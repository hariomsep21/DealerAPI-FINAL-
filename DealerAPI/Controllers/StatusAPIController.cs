using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StatusAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public StatusAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<StatusDTO>>> GetStatusSupport()
        {
            try
            {
                var statuses = await _db.Statustbl.ToListAsync();

                if (statuses == null || statuses.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var statusesDto = _mapper.Map<IEnumerable<StatusDTO>>(statuses);

                return Ok(statusesDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return StatusCode(500, "Internal Server Error");
            }


        }
    }
}

