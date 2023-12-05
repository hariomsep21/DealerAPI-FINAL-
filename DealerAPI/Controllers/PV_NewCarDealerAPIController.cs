using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Controllers
{
    //public class PV_OpenMarketAPIController
    //{
    //}

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
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public ActionResult<PV_NewCarDealerDTO> PostNewCarDealerSupport(PV_NewCarDealerDTO pv_cardealerDto)
        {
            try
            {
                int lastUserId = _db.LastUsetbl.OrderByDescending(u => u.ActiveId).FirstOrDefault()?.UserValue ?? 0;
                if (pv_cardealerDto == null)
                {
                    return BadRequest("PV_AggregatorDTO is null");
                }

                // Optional: Validation if an item with the same Id already exists
                // if (_db.PV_Aggregatorstbl.Any(v => v.Id == pv_aggregatorDto.Id))
                // {
                //     return StatusCode(500, "Item with the same Id already exists");
                // }

                pv_cardealerDto.UserInfoId = lastUserId;
                var pv_cardealerModel = _mapper.Map<PV_NewCarDealer>(pv_cardealerDto);

                _db.PV_NewCarDealerstbl.Add(pv_cardealerModel);
                _db.SaveChanges();

                var pv_carDtoModel = _mapper.Map<PV_NewCarDealerDTO>(pv_cardealerModel);

                return Ok(pv_carDtoModel);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(500, "Internal Server Error");
    }
}


       
    }
}
