using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet("{id}")]
        public IActionResult Get(int id) 
        {
            var user = _userService.GetUser(id);

            return user != null ? Ok(user): NotFound("User was not found");
        }
    }
}
