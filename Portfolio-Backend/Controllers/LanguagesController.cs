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
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;
        private readonly IUserService _userService;

        public LanguagesController(ILanguageService languageService, IUserService userService)
        {
            _languageService = languageService;
            _userService = userService;
        }

        [HttpPost("AddLanguage")]
        //[Authorize(Roles = "Admin")]
        public IActionResult AddLanguage(Language language)
        {
            try
            {
                User user = _userService.GetUser(language.UserId);

                if (user == null)
                {
                    return BadRequest("User was not found");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Skill");
                }

                Language addedLanguage = _languageService.AddLanguage(language);

                return Ok(addedLanguage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetLanguages")]
        public IActionResult GetLanguages()
        {
            try
            {
                List<Language> languages = _languageService.GetLanguages();

                return Ok(languages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
