using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BAL.IServices;
using DAL.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.Models;
using Portfolio_Backend.Repository;

namespace Portfolio_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService) 
        {
            _userService = userService;
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
            };

            return Ok(_userService.AddUser(user));
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

            return Ok(checkUser.Id);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            var user = _userService.GetUser(id);

            return user != null ? Ok(user): BadRequest("User was not found");
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
    } 
}
