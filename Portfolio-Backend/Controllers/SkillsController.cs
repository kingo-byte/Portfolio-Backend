using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.Models;
using Portfolio_Backend.Repository;

namespace Portfolio_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly ISkillsServices _skillsServices;
        private readonly IUserService _userService;

        public SkillsController(ISkillsServices skillsServices, IUserService userService)
        {
            _skillsServices = skillsServices;
            _userService = userService; 
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddSkill(Skill skill)
        {
            try
            {
                User user = _userService.GetUser(skill.UserId);
      
                if (user == null) 
                {
                    return BadRequest("User was not found");  
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Skill");
                }

                Skill addedSkill = _skillsServices.AddSkill(skill);

                return Ok(addedSkill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetSkills")]
        public IActionResult GetSkills()
        {
            try
            {
                List<Skill> skills = _skillsServices.GetSkills();

                return Ok(skills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
