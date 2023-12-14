using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DealerAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CustomerSupportAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
       private readonly IMapper _mapper;

        public CustomerSupportAPIController(ApplicationDbContext db,IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<CustomerSupportDTO>>> GetCustomerSupport()
        {
            try
            {
                var Customer = await _db.CustomerSupporttbl.ToListAsync();

                if (Customer == null || Customer.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var CustomerDto = _mapper.Map<IEnumerable<CustomerSupportDTO>>(Customer);

                return Ok(CustomerDto);
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
