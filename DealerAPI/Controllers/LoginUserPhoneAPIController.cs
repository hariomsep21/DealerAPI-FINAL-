using AutoMapper;
using DealerAPI.Data;
using DealerAPI.Models;
using DealerAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DealerAPI.Migrations;
using System.Security.Cryptography;

namespace DealerAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]

    public class LoginUserPhoneAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public LoginUserPhoneAPIController(ApplicationDbContext db, IMapper mapper, IConfiguration configuration)
        {
            _db = db;
            _mapper = mapper;
            _config = configuration;
        }


        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserPhoneDTO>>> GetUserPhoneSupport()
        {
            try
            {
                var Phone = await _db.Userstbl.ToListAsync();

                if (Phone == null || Phone.Count == 0)
                {
                    return NotFound("No account details found");
                }

                var PhoneDto = _mapper.Map<IEnumerable<UserPhoneDTO>>(Phone);

                return Ok(PhoneDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }



      
        [HttpPost("generateotp")]
        public IActionResult GenerateOTP(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone number is required to generate OTP.");
            }

            // Check if the user with the given phone number exists in the database
            var user = _db.Userstbl.FirstOrDefault(u => u.Phone == phoneNumber);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Generate a 4-digit OTP
            int otp = GenerateOTP();

            // Set the OTP and its expiry in the UserInfo table
            user.OTP = otp;
            // Expiry set to 1 minute from current time
            user.OTPExpiry = DateTime.UtcNow.AddMinutes(1);
            _db.SaveChanges();

            // Return the OTP for the user to enter on the next page
            return Ok(otp); // Modify this as needed
        }
        private bool IsOTPOutdated(DateTime? otpExpiration)
        {
            if (otpExpiration.HasValue && otpExpiration.Value < DateTime.UtcNow)
            {
                return true; // OTP has expired
            }
            return false; // OTP is still valid
        }

        //resend

        [HttpPost("resend-otp")]
        public IActionResult ResendOTP(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return BadRequest("Phone number is required to generate OTP.");
            }

            // Check if the user with the given phone number exists in the database
            var user = _db.Userstbl.FirstOrDefault(u => u.Phone == phoneNumber);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the previous OTP expired
            if (IsOTPOutdated(user.OTPExpiry))
            {
                // Generate a new OTP and update user information
                int newOTP = GenerateOTP();
                user.OTP = newOTP;
                user.OTPExpiry = DateTime.UtcNow.AddMinutes(1); // Set a new expiration time

                // Save changes to the database
                _db.SaveChanges();

                // Send the new OTP via SMS or any other method (not shown here)

                return Ok("New OTP sent successfully.");
            }



            return BadRequest("Previous OTP is still valid.");
        }

        //Verify

        [HttpPost("verifyotp")]
        public IActionResult VerifyOTP(string phoneNumber, int enteredOTP)
        {
            if (string.IsNullOrEmpty(phoneNumber) || enteredOTP == null)
            {
                return BadRequest("Phone number and OTP are required for verification.");
            }

            // Check if the user with the given phone number exists in the database
            var user = _db.Userstbl.FirstOrDefault(u => u.Phone == phoneNumber);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the entered OTP matches the stored OTP and is not expired
            if (user.OTP == enteredOTP && user.OTPExpiry > DateTime.UtcNow)
            {
                // Reset the OTP and its expiry as it's no longer needed
                user.OTP = null;
                user.OTPExpiry = DateTime.MinValue;

                _db.SaveChanges();

                // Generate a token for successful login
                string token = CreateToken(user);

                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken, user);
                // Perform actions for successful login
                // You can return user information, generate a token, or set session information here

                return Ok(token); // Modify this as needed
            }

            user.OTP = null;
            user.OTPExpiry = DateTime.MinValue;

            _db.SaveChanges();
            return BadRequest("Invalid OTP or OTP has expired");

        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(30),
                Created = DateTime.UtcNow
            };
            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken refreshToken, UserInfo user)
        {
            var cookiOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
            };
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookiOptions);
            user.RefreshToken = refreshToken.Token;
            user.TokenCreated = refreshToken.Created;
            user.TokenExpires = refreshToken.Expires;
            _db.SaveChanges();


        }
        private int GenerateOTP()
        {
            Random random = new Random();
            return random.Next(1000, 9999);
        }

        private string CreateToken(UserInfo userInfo)
        {
            List<Claim> claims = new List<Claim>()
          {
              new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
              new Claim (ClaimTypes.Name, userInfo.UserName)

          };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Appsettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,

                expires: DateTime.UtcNow.AddDays(30),
                audience: "http://localhost:5137",
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;

        }



        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                // Retrieve the user from the authenticated context
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int userIdInt))
                {
                    // Find the user from the database using the converted ID
                    var user = await _db.Userstbl.FirstOrDefaultAsync(u => u.Id == userIdInt);

                    if (user != null)
                    {
                        // Invalidate the refresh token by setting it to null or an empty string
                        user.RefreshToken = null;
                        user.TokenCreated = DateTime.MinValue;
                        user.TokenExpires = DateTime.MinValue;
                        // You might also want to update TokenCreated and TokenExpires here if applicable

                        await _db.SaveChangesAsync();

                        // Clear the token cookie on the client-side
                        Response.Cookies.Delete("refreshToken");


                        return Ok("Logged out successfully");
                    }

                    return NotFound("User not found");
                }

                return BadRequest("Invalid or missing user ID");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("refresh-token")]

        public async Task<ActionResult<string>> RefreshToken()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];

                // Retrieve the user from the database based on the refresh token
                var user = await _db.Userstbl.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

                if (user == null)
                {
                    return Unauthorized("Invalid Refresh Token.");
                }
                else if (user.TokenExpires < DateTime.Now)
                {
                    return Unauthorized("Token expired.");
                }

                // Refresh the token and generate a new refresh token
                string token = CreateToken(user);
                var newRefreshToken = GenerateRefreshToken();
                SetRefreshToken(newRefreshToken, user);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost("Register")]
        public async Task<ActionResult<UserPhoneDTO>> RegisterUser(string phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the phone number already exists
            var existingUser = await _db.Userstbl.FirstOrDefaultAsync(u => u.Phone == phone);
            if (existingUser != null)
            {
                return Conflict("Phone number already exists");
            }

            // Create a new user based on the DTO
            var newUser = new UserInfo
            {
                Phone = phone,
                // Set other properties as needed
            };

            // Add the user to the database
            _db.Userstbl.Add(newUser);
            await _db.SaveChangesAsync();
            int otp = GenerateOTP();

            // Set the OTP and its expiry in the UserInfo table
            newUser.OTP = otp;
            // Expiry set to 1 minute from current time
            newUser.OTPExpiry = DateTime.UtcNow.AddMinutes(1);
            _db.SaveChanges();

            // Return the OTP for the user to enter on the next page
            return Ok(otp); // Mo


        }
        [HttpPost("AdditionalDetails")]
        public async Task<IActionResult> AddAdditionalUserDetails([FromBody] UserAdditionalDetailsDto additionalDetails, string phone)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Find the user by phone number
                var existingUser = await _db.Userstbl.FirstOrDefaultAsync(u => u.Phone == phone);
                if (existingUser == null)
                {
                    return NotFound("Phone number not found");
                }

                // Update the user with additional details
                existingUser.UserName = additionalDetails.UserName;
                existingUser.UserEmail = additionalDetails.UserEmail;
                existingUser.SId = additionalDetails.SId;

                // Save changes to the database
                await _db.SaveChangesAsync();
                string token = CreateToken(existingUser);

                var refreshToken = GenerateRefreshToken();
                SetRefreshToken(refreshToken, existingUser);
                // Perform actions for successful login
                // You can return user information, generate a token, or set session information here

                return Ok(token); // Modify this as
               
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"Exception: {ex.Message}");
                return StatusCode(500, "An error occurred while saving data");
            }
        }


        [HttpPost("verifyotpSignup")]
        public IActionResult VerifyOTPSignup(string phoneNumber, int enteredOTP)
        {
            if (string.IsNullOrEmpty(phoneNumber) || enteredOTP == null)
            {
                return BadRequest("Phone number and OTP are required for verification.");
            }

            // Check if the user with the given phone number exists in the database
            var user = _db.Userstbl.FirstOrDefault(u => u.Phone == phoneNumber);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the entered OTP matches the stored OTP and is not expired
            if (user.OTP == enteredOTP && user.OTPExpiry > DateTime.UtcNow)
            {
                // Reset the OTP and its expiry as it's no longer needed
                user.OTP = null;
                user.OTPExpiry = DateTime.MinValue;

                _db.SaveChanges();


                return Ok(); // Modify this as needed
            }

            user.OTP = null;
            user.OTPExpiry = DateTime.MinValue;

            _db.SaveChanges();
            return BadRequest("Invalid OTP or OTP has expired");

        }
    }
}
