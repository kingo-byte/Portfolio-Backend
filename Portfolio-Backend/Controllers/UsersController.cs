using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BAL.IServices;
using DAL.Models;
using DAL.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Portfolio_Backend.Models;
using Portfolio_Backend.Repository;

namespace Portfolio_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRefreshTokenServices _refreshTokenServices;
        private readonly IConfiguration _configuration;
        public UsersController(IUserService userService, IRefreshTokenServices refreshTokenServices, IConfiguration configuration) 
        {
            _userService = userService;
            _refreshTokenServices = refreshTokenServices;
            _configuration = configuration; 
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(SignUpDto request) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest("Invalid User");
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                UserName = request.UserName,    
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = request.RoleId
            };

            return Ok(_userService.AddUser(user));
        }

        [HttpPost("BulkRegister")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> BulkRegister([FromBody] List<SignUpDto> requests)
        {
            if (!ModelState.IsValid || requests == null || !requests.Any())
            {
                return BadRequest("Invalid User(s)");
            }

            try
            {
                List<User> users = new List<User>();

                foreach (var request in requests)
                {
                    CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    users.Add(new User
                    {
                        UserName = request.UserName,
                        Email = request.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt,
                        RoleId = request.RoleId
                    });
                }

                var result = _userService.BulkInsert(users);

                if (result == 1)
                {
                    return Ok("Users registered successfully");
                }
                else
                {
                    return StatusCode(500, "Failed to register users");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(SignInDTO request) 
        {
            User user = new User() 
            {
                UserName = request.UserName,
                Email = request.Email
            };

            User checkUser = _userService.CheckUser(user);

            if (checkUser == null) 
            {
                return BadRequest("Invalid User");
            }

            if (!VerifyPasswordHash(request.Password, checkUser.PasswordHash, checkUser.PasswordSalt)) 
            {
                return BadRequest("Invalid Password");
            }

            string token = CreateToken(checkUser);

            SetRefreshToken(checkUser.Id);

            return Ok(token);
        }

        [HttpGet("GetUser/{id}")]
        //[Authorize(Roles = "Admin")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);

            return user != null ? Ok(user) : BadRequest("User was not found");
        }

        [HttpPost("RefreshToken")]
        //[Authorize]
        public IActionResult RefreshToken(int userId) 
        {
            // Get last generated token
            var refreshToken = _refreshTokenServices.GetUserToken(userId);
            var loggedInUser = _userService.GetUser(userId);

            if(loggedInUser == null) 
            {
                return BadRequest("Invalid User");
            }

            if(refreshToken == null)
            {
                return Unauthorized("User is not signed In");
            }

            if(refreshToken.ExpiresAt < DateTime.UtcNow) 
            {
                return Unauthorized("Token Expired");
            }

            string token = CreateToken(loggedInUser);

            SetRefreshToken(loggedInUser.Id);

            return Ok(token);   
        }

        private string CreateToken(User user) 
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.role.Name) 
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new HMACSHA512()) 
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[]passwordSalt) 
        {
            using (var hmac = new HMACSHA512(passwordSalt)) 
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private RefreshToken SetRefreshToken(int userId)
        {
            var token = new RefreshToken
            {
                Token = Guid.NewGuid(),
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                CreatedAt = DateTime.UtcNow,
                UserId = userId
            };

            //add new refresh token in the database
            RefreshToken refreshToken =  _refreshTokenServices.AddToken(token);

            return refreshToken;
        }
    } 
}
