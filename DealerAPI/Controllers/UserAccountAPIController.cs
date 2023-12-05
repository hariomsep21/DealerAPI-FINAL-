using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DealerAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserAccountAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserAccountAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserAccountDTO>>> GetUserAccountSupport()
        {
            try
            {
                // Assuming LastUsetbl is a table and Id is the property to get the last user's Id
                int lastUserId = _db.LastUsetbl.OrderByDescending(u => u.ActiveId).FirstOrDefault()?.UserValue ?? 0;

                if (lastUserId == 0)
                {
                    // Handle the case where no user is found in LastUsetbl
                    return NotFound("No User details found");
                }

                // Fetch user details where UserId is equal to lastUserId
                var userInfoDetails = await _db.Userstbl
                    .Where(user => user.Id == lastUserId)
                    .ToListAsync();

                if (userInfoDetails == null || userInfoDetails.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No User details found");
                }

                // Use AutoMapper to map the entities to DTO
                var userInfoDto = _mapper.Map<IEnumerable<UserAccountDTO>>(userInfoDetails);

                return Ok(userInfoDto);
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