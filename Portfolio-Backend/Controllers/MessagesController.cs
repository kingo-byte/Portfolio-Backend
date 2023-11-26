using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BAL.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio_Backend.Models;
using Portfolio_Backend.Repository;

namespace Portfolio_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IMessageServices _messageServices; 

        public MessagesController(ILogger<MessagesController> logger, IMessageServices messageServices)
        {
            _logger = logger;
            _messageServices = messageServices;
        }

        [HttpGet("GetMessages")]
        public IActionResult GetMessages()
        {
            try
            {
                List<Message> messages = _messageServices.GetMessages();

                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("AddMessage")]
        public IActionResult AddMessage(Message message)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid Message");
                }

                Message addedMessage = _messageServices.AddMessage(message);

                return Ok(addedMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
