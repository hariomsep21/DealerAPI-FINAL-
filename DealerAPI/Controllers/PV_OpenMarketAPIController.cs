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
    public class PV_OpenMarketAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public PV_OpenMarketAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PV_OpenMarketDTO>>> GetOpenMarketSupport()
        {
            try
            {
                var openMarket = await _db.PV_OpenMarketstbl.ToListAsync();

                if (openMarket == null || openMarket.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var openMarketDto = _mapper.Map<IEnumerable<PV_OpenMarketDTO>>(openMarket);

                return Ok(openMarketDto);
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

        public ActionResult<PV_OpenMarketDTO> PostOpenMarketSupport(PV_OpenMarketDTO pV_openmarketDTO)
        {

            try
            {
                int lastUserId = _db.LastUsetbl.OrderByDescending(u => u.ActiveId).FirstOrDefault()?.UserValue ?? 0;
                if (pV_openmarketDTO == null)
                {
                    return BadRequest("pv_carDtoDTO is null");
                }

                // Optional: Validation if an item with the same Id already exists
                //if (_db.PV_Aggregatorstbl.Any(v => v.Id == pv_aggregatorDto.Id))
                //{
                //    return StatusCode(500, "Item with the same Id already exists");
                //}
                pV_openmarketDTO.UserInfoId = lastUserId;
                var pV_openmarketModel = _mapper.Map<PV_OpenMarket>(pV_openmarketDTO);

                _db.PV_OpenMarketstbl.Add(pV_openmarketModel);
                _db.SaveChanges();

                var pV_openmarketDtoModel = _mapper.Map<PV_OpenMarketDTO>(pV_openmarketModel);

                return Ok(pV_openmarketDtoModel);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

