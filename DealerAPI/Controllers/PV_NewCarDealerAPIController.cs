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
    public class PV_NewCarDealerAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PV_NewCarDealerAPIController(ApplicationDbContext db, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<PV_NewCarDealerDTO>>> GetNewCarDealerSupport()
        {
            try
            {
                var carDealer = await _db.PV_NewCarDealerstbl.ToListAsync();

                if (carDealer == null || carDealer.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var carDealerDto = _mapper.Map<IEnumerable<PV_NewCarDealerDTO>>(carDealer);

                return Ok(carDealerDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return StatusCode(500, "Internal Server Error");
            }
        }



        [HttpPost("Post")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]


        public ActionResult<PV_NewCarDealerDTO> PostNewCarDealer(PV_NewCarDealerDTO pv_cardealerDto)
        {
            try
            {
                if (pv_cardealerDto == null)
                {
                    return BadRequest("PV_NewCarDealerDTO is null");
                }

                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    // Set UserId in the DTO before mapping to the entity
                    pv_cardealerDto.UserInfoId = userId;

                    var pv_cardealerModel = _mapper.Map<PV_NewCarDealer>(pv_cardealerDto);

                    _db.PV_NewCarDealerstbl.Add(pv_cardealerModel);
                    _db.SaveChanges();

                    var pv_carDtoModel = _mapper.Map<PV_NewCarDealerDTO>(pv_cardealerModel);

                    return Ok(pv_carDtoModel);
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

