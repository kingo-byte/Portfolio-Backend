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
    public class ExperiencesController : ControllerBase
    {
        private readonly IExperienceService _experienceService; 
        private readonly IUserService _userService; 

        public ExperiencesController(IExperienceService experienceService, IUserService userService)
        {
            _experienceService = experienceService;
            _userService = userService;
        }

        [HttpGet("GetExperiences")]
        public async Task<ActionResult<List<Experience>>> GetExperiences()
        {
            return Ok(await _experienceService.GetExperiences());
        }

        [HttpPost("AddExperience")]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<Experience>> AddExperience(Experience experience)
        {
            try 
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Experience is not valid");
                }

                User user = _userService.GetUser(experience.UserId);

                if (user == null)
                {
                    return BadRequest("User does not exist");
                }

                Experience addedExperience = await _experienceService.AddExperience(experience);

                Console.WriteLine("teadaskdjadskadkapkdasp");

                return Ok(addedExperience);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
