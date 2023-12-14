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
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<UserInfoDTO>>> GetUserInfoSupport()
        {
            try
            {
                var userInfoDetails = await _db.Userstbl
                    .ToListAsync();

                if (userInfoDetails == null || userInfoDetails.Count == 0)
                {
                    // Return 404 Not Found if no data is found
                    return NotFound("No user details found");
                }

                // Use AutoMapper to map the entities to DTO
                var userInfoDTO = _mapper.Map<IEnumerable<UserInfoDTO>>(userInfoDetails);

                return Ok(userInfoDTO);
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

        public ActionResult<UserInfoDTO> PostUserInfoSupport(UserInfoDTO userInfoDTO)
        {
            try
            {
               // int lastUserId = _db.LastUsetbl.OrderByDescending(u => u.ActiveId).FirstOrDefault()?.UserValue ?? 0;

                if (userInfoDTO == null)
                {
                    return BadRequest("userInfoDTO is null");
                }

                var UserInfoModel = _mapper.Map<UserInfo>(userInfoDTO);

                // Check if a user with the same id already exists
                var existingUser = _db.Userstbl.FirstOrDefault(u => u.Id == userInfoDTO.Id);

                if (existingUser != null)
                {
                    // Update existing user values
                    existingUser.UserName = UserInfoModel.UserName; // Replace with actual property names
                    existingUser.UserEmail = UserInfoModel.UserEmail; // Replace with actual property names
                    existingUser.State = UserInfoModel.State;
                   

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
        [Authorize]
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

        [HttpGet("State")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<StateDTO>>> GetState()
        {
            try
            {


                var filter = await _db.Statetbl.ToListAsync();
                return Ok(filter);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}