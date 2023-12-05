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
    public class PV_AggregatorAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PV_AggregatorAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PV_AggregatorDTO>>> GetAggregatorSupport()
        {
            try
            {
                var aggreator = await _db.PV_Aggregatorstbl.ToListAsync();

                if (aggreator == null || aggreator.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var aggreatorDto = _mapper.Map<IEnumerable<PV_AggregatorDTO>>(aggreator);

                return Ok(aggreatorDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return StatusCode(500, "Internal Server Error");
            }


        }



        [HttpPost("Post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public ActionResult<PV_AggregatorDTO> PostAggregatorSupport( PV_AggregatorDTO pv_aggregatorDto)
        {
            try
            {
                if (pv_aggregatorDto == null)
                {
                    return BadRequest("PV_AggregatorDTO is null");
                }

                // Optional: Validation if an item with the same Id already exists
                // if (_db.PV_Aggregatorstbl.Any(v => v.Id == pv_aggregatorDto.Id))
                // {
                //     return StatusCode(500, "Item with the same Id already exists");
                // }

                var pvAggregatorModel = _mapper.Map<PV_Aggregator>(pv_aggregatorDto);

                _db.PV_Aggregatorstbl.Add(pvAggregatorModel);
                _db.SaveChanges();

                var createdDto = _mapper.Map<PV_AggregatorDTO>(pvAggregatorModel);

                return Ok(createdDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
