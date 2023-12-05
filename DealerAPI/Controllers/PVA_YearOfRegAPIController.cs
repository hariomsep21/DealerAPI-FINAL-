using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PVA_YearOfRegAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PVA_YearOfRegAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PVA_YearOfRegDTO>>> GetYearOfRegDetails()
        {
            try
            {
                var YearOfRegdetails = await _db.PVA_YearOfRegtbl.ToListAsync();

                if (YearOfRegdetails == null || YearOfRegdetails.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var YearOfRegdetailssDto = _mapper.Map<IEnumerable<PVA_YearOfRegDTO>>(YearOfRegdetails);

                return Ok(YearOfRegdetailssDto);
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
