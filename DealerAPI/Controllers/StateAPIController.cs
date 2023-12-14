using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class StateAPIController : ControllerBase
    {


        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public StateAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
       
        public async Task<ActionResult<IEnumerable<StateDTO>>> GetStateDetails()
        {
            try
            {
                var statedetails = await _db.Statetbl.ToListAsync();

                if (statedetails == null || statedetails.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var StatesDto = _mapper.Map<IEnumerable<StateDTO>>(statedetails);

                return Ok(StatesDto);
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
