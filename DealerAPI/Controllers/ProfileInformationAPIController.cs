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
    public class ProfileInformationAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public ProfileInformationAPIController(ApplicationDbContext db, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<ProfileInformationDTO>>> GetProfileSupport()
        {
            try
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    var profiles = await _db.ProfileInformationtbl
                        .Where(p => p.UserInfoId == userId)
                        .ToListAsync();

                    if (profiles == null || profiles.Count == 0)
                    {
                        // Return 404 Not Found if no data is found
                        return NotFound("No account details found");
                    }

                    // Use AutoMapper to map the entities to DTO
                    var profileDto = _mapper.Map<IEnumerable<ProfileInformationDTO>>(profiles);

                    return Ok(profileDto);
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

        [HttpPost("Post")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ProfileInformationDTO>> CreateProfile([FromBody] ProfileInformationDTO profileCreateDto)
        {
            try
            {
                // Validate the incoming data
                if (profileCreateDto == null)
                {
                    return BadRequest("Invalid data");
                }

                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (int.TryParse(userIdString, out int userId))
                {
                    // Set UserId in the DTO before mapping to the entity
                    profileCreateDto.UserInfoId = userId;

                    // You may want to perform additional validation on profileCreateDto properties

                    // Create a new profile entity
                    var newProfile = _mapper.Map<ProfileInformation>(profileCreateDto);

                    // Add the new profile to the database
                    _db.ProfileInformationtbl.Add(newProfile);
                    await _db.SaveChangesAsync();

                    // Use AutoMapper to map the created entity to DTO
                    var createdProfileDto = _mapper.Map<ProfileInformationDTO>(newProfile);

                    // Return 201 Created with the created profile DTO
                    return CreatedAtAction(nameof(GetProfileSupport), new { id = createdProfileDto.UserInfoId }, createdProfileDto);
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
