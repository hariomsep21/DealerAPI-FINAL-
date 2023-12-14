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
    public class StockAudit_PurposeAPIController : ControllerBase
    {


        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public StockAudit_PurposeAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<StockAudit_PurposeDTO>>> GetStockPurposeDetails()
        {
            try
            {
                var stockAPdetails = await _db.StockAudit_Purposestbl.ToListAsync();

                if (stockAPdetails == null || stockAPdetails.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var stockAPDto = _mapper.Map<IEnumerable<StockAudit_PurposeDTO>>(stockAPdetails);

                return Ok(stockAPDto);
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
