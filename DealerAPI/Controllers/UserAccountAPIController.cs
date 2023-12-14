using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserAccountDTO>>> GetUserAccountSupport()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    // Fetch user details where UserId is equal to the ID from claims
                    var userInfoDetails = await _db.Userstbl
                        .Where(user => user.Id == userId)
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
                else
                {
                    // Handle the case where the user ID from the claim cannot be parsed as an integer
                    return BadRequest("Invalid user ID");
                }
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