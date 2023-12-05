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
    public class UserPhoneAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserPhoneAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }


        [HttpGet("SignGet")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserPhoneDTO>>> GetUserPhoneSupport()
        {
            try
            {
                var Phone = await _db.userPhonestbl.ToListAsync();

                if (Phone == null || Phone.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No account details found");
                }

                // Use AutoMapper to map the entities to DTO
                var PhoneDto = _mapper.Map<IEnumerable<UserPhoneDTO>>(Phone);

                return Ok(PhoneDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application

                // Return 500 Internal Server Error
                return StatusCode(500, "Internal Server Error");
            }


        }





        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public ActionResult<UserPhoneDTO> PostUserPhoneSupport(UserPhoneDTO userphoneDTO)
        {
            try
            {
                if (userphoneDTO == null)
                {
                    return BadRequest("UserPhoneDTO is null");
                }

                // Check if the user phone already exists
                if (_db.userPhonestbl.Any(u => u.PhoneNumber == userphoneDTO.PhoneNumber))
                {
                    return Conflict("User phone already exists");
                }

                var UserPhoneModel = _mapper.Map<UserPhone>(userphoneDTO);

                _db.userPhonestbl.Add(UserPhoneModel);
                _db.SaveChanges();

                var UserPhoneDto = _mapper.Map<UserPhoneDTO>(UserPhoneModel);

                return Ok(UserPhoneDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(500, "Internal Server Error");
            }
        }


    }
}