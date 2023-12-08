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
                using (var transaction = _db.Database.BeginTransaction())
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

                        // Assuming _db is your DbContext instance

                        var existingUser = _db.Userstbl.FirstOrDefault(e => e.PhnId == UserPhoneModel.PhoneId);

                        _db.userPhonestbl.Add(UserPhoneModel);
                        _db.SaveChanges();

                        // this condition for save phnid or otp in userInfo tbl.
                        if (UserPhoneModel != null)
                        {
                            // these two lines for cleaning LastUsertbl and restarting it with 1 id.
                            _db.Database.ExecuteSqlRaw("DELETE FROM dbo.LastUsetbl");
                            _db.Database.ExecuteSqlRaw("DBCC CHECKIDENT('dbo.LastUsetbl', RESEED, 0)");

                            var otpUpdate = GenerateOTPForSignUp();

                            // Create a new user record and add it to the DbSet
                            var newUser = new UserInfo
                            {
                                // Set other properties as needed
                                PhnId = UserPhoneModel.PhoneId,
                                OTP = otpUpdate,
                                // Set other properties as needed
                            };

                            newUser.StateId = 1;
                            _db.Userstbl.Add(newUser);
                            _db.SaveChanges();

                            var activeUserId = new LastUser { UserValue = newUser.Id };
                            _db.LastUsetbl.Add(activeUserId);
                            _db.SaveChanges();
                        }

                        // These codes for calling the generate method and saving it to the database.
                        var UserPhoneDto = _mapper.Map<UserPhoneDTO>(UserPhoneModel);

                        transaction.Commit(); // Commit the transaction if everything is successful.
                        return Ok(UserPhoneDto);
                    }
                    catch (Exception)
                    {
                        // Log the exception or handle it as appropriate for your application
                        transaction.Rollback(); // Rollback the transaction if an exception occurs.
                        throw; // Re-throw the exception to maintain the original exception stack trace.
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as appropriate for your application
                return StatusCode(500, "Internal Server Error");
            }

        }

        private string GenerateOTPForSignUp()
        {
            // Generate a random 4-digit OTP
            Random random = new Random();
            int otp = random.Next(1000, 10000);

            return otp.ToString("D4"); // Format as a 4-digit string
        }
    }
}