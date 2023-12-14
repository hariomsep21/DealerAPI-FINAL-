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
    public class Order_StockAuditAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public Order_StockAuditAPIController(ApplicationDbContext db, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<Order_StockAuditDTO>>> GetStockSupport()
        {
            try
            {
                var stockAudit = await _db.Order_StockAuditstbl.ToListAsync();

                if (stockAudit == null || stockAudit.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var stockAuditDto = _mapper.Map<IEnumerable<Order_StockAuditDTO>>(stockAudit);

                return Ok(stockAuditDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return StatusCode(500, "Internal Server Error");
            }


        }



        [HttpPost]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public ActionResult<Order_StockAuditDTO> PostStockSupport(Order_StockAuditDTO stockAuditDTO)
        {
            try
            {
                if (stockAuditDTO == null)
                {
                    return BadRequest("stockAuditDTO is null");
                }

                // Optional: Validation if an item with the same Id already exists
                // if (_db.PV_Aggregatorstbl.Any(v => v.Id == pv_aggregatorDto.Id))
                // {
                //     return StatusCode(500, "Item with the same Id already exists");
                // }

                var stockAuditModel = _mapper.Map<Order_StockAudit>(stockAuditDTO);

                _db.Order_StockAuditstbl.Add(stockAuditModel);
                _db.SaveChanges();

                var stockAuditDTOModel = _mapper.Map<Order_StockAuditDTO>(stockAuditModel);

                return Ok(stockAuditDTOModel);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}