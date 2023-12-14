using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DealerAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class NotificationAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public NotificationAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<NotificationDTO>>> GetNotificationSupport()
        {
            try
            {
                var notifications = await _db.Notificationstbl.ToListAsync();

                if (notifications == null || notifications.Count == 0)
                {
                    // Return 204 No Content if no data is found
                    return NoContent();
                }

                // Use AutoMapper to map the entities to DTO
                var notificationDto = _mapper.Map<IEnumerable<NotificationDTO>>(notifications);

                return Ok(notificationDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
