using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
        [Authorize]
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


        [Authorize]
        [HttpPost("Post")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public ActionResult<PV_AggregatorDTO> PostAggregatorSupport(PV_AggregatorDTO pv_aggregatorDto)
        {
            try
            {
                if (pv_aggregatorDto == null)
                {
                    return BadRequest("PV_AggregatorDTO is null");
                }

                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    // Set UserId in the DTO before mapping to the entity
                    pv_aggregatorDto.UserInfoId = userId;

                    var pvAggregatorModel = _mapper.Map<PV_Aggregator>(pv_aggregatorDto);

                    _db.PV_Aggregatorstbl.Add(pvAggregatorModel);
                    _db.SaveChanges();

                    var createdDto = _mapper.Map<PV_AggregatorDTO>(pvAggregatorModel);

                    return Ok(createdDto);
                }
                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}
