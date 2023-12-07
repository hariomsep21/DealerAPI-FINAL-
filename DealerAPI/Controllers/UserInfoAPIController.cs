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
    public class UserInfoAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserInfoAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }


        [HttpGet("Getdetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserInfoDTO>>> GetUserInfoSupport()
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
                var userInfoDto = _mapper.Map<IEnumerable<UserInfoDTO>>(userInfoDetails);

                return Ok(userInfoDto);
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

        public ActionResult<UserInfoDTO> PostUserInfoSupport(UserInfoDTO userInfoDTO)
        {
            try
            {
                int lastUserId = _db.LastUsetbl.OrderByDescending(u => u.ActiveId).FirstOrDefault()?.UserValue ?? 0;

                if (userInfoDTO == null)
                {
                    return BadRequest("userInfoDTO is null");
                }

                var UserInfoModel = _mapper.Map<UserInfo>(userInfoDTO);

                // Check if a user with the same id already exists
                var existingUser = _db.Userstbl.FirstOrDefault(u => u.Id == lastUserId);

                if (existingUser != null)
                {
                    // Update existing user values
                    existingUser.UserName = UserInfoModel.UserName; // Replace with actual property names
                    existingUser.UserEmail = UserInfoModel.UserEmail; // Replace with actual property names

                    existingUser.StatusId = 1;// Update other properties as needed

                    _db.SaveChanges();

                    var updatedUserInfoDtoModel = _mapper.Map<UserInfoDTO>(existingUser);
                    return Ok(updatedUserInfoDtoModel);
                }
                else
                {
                    // If no user with the specified id exists, add a new user
                    _db.Userstbl.Add(UserInfoModel);
                    _db.SaveChanges();

                    var newUserInfoDtoModel = _mapper.Map<UserInfoDTO>(UserInfoModel);
                    return Ok(newUserInfoDtoModel);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserInfoDTO>> GetUserInfoById(int id)
        {
            try
            {
                var userInfo = await _db.Userstbl.FindAsync(id);

                if (userInfo == null)
                {
                    // Return 404 Not Found if the user with the specified ID is not found
                    return NotFound($"User with ID {id} not found");
                }

                // Use AutoMapper to map the entity to DTO
                var userInfoDto = _mapper.Map<UserInfoDTO>(userInfo);

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