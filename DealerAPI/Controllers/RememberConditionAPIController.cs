using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class RememberConditionAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public RememberConditionAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LastUserDTO>>> GetRememberIdAsync()
        {
            try
            {
                var lastUsers = await _db.LastUsetbl.ToListAsync();

                if (lastUsers == null || !lastUsers.Any())
                {
                    return NoContent();
                }

                var lastUserDtos = _mapper.Map<IEnumerable<LastUserDTO>>(lastUsers);

                return Ok(lastUserDtos);
            }
            catch (Exception ex)
            {
                // Log the exception using a logging framework like Serilog or ILogger
                // For simplicity, logging to console is shown here.
                Console.Error.WriteLine($"Error: {ex.Message}");

                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
