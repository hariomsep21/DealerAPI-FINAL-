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
                var Phone = await _db.userPhonestbl.ToListAsync();

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







        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]

        public IActionResult LoginUserPhone([FromBody] UserPhoneDTO userPhoneDTO)
        {
            try
            {

                //these two ine for clean LastUsertbl and it is restart with 1 id.
                _db.Database.ExecuteSqlRaw("DELETE FROM dbo.LastUsetbl");
                _db.Database.ExecuteSqlRaw("DBCC CHECKIDENT('dbo.LastUsetbl', RESEED, 0)");

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingUserPhone = _db.userPhonestbl.FirstOrDefault(u => u.PhoneNumber == userPhoneDTO.PhoneNumber);

                if (existingUserPhone == null)
                {
                    return NotFound($"Phone number '{userPhoneDTO.PhoneNumber}' not found");
                }

                var userInfo = _db.Userstbl.FirstOrDefault(u => u.PhnId == existingUserPhone.PhoneId);

                if (userInfo == null )
                {

                    return NotFound($"User information not found for phone number '{userPhoneDTO.PhoneNumber}'");
                }

                //these code for call generate method and save in database.
                var otpUpdate = GenerateOTP();
                userInfo.OTP = otpUpdate;
                _db.SaveChanges();


                //these code for  save UderId  in database.
                var activeUserId = new LastUser { UserValue = userInfo.Id };
                _db.LastUsetbl.Add(activeUserId);
                _db.SaveChanges();

                var token = GenerateJwtToken(existingUserPhone, userInfo);

                return Ok(token);
            }
            catch (Exception ex)
            {
                // Log the exception details
               
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        private string GenerateOTP()
        {
            // Generate a random 4-digit OTP
            Random random = new Random();
            int otp = random.Next(1000, 10000);

            return otp.ToString("D4"); // Format as a 4-digit string
        }


        private string GenerateJwtToken(UserPhone userPhone, UserInfo userInfo )
        {
            var userIdClaim = userInfo?.PhnId == userPhone.PhoneId
                ? new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString())
                : null;

            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(userPhone.PhoneNumber))
            {
                claims.Add(new Claim(ClaimTypes.Name, userPhone.PhoneNumber));
            }

            if (userIdClaim != null)
            {
                claims.Add(userIdClaim);
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Authentication:Schemes:Bearer:SigningKeys:0:Value").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(

                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }






        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserPhoneDTO> GetUserPhoneById(int id)
        {
            try
            {
                var userPhone = _db.userPhonestbl.FirstOrDefault(u => u.PhoneId == id);

                if (userPhone == null)
                {
                    return NotFound($"User phone with ID '{id}' not found");
                }

                var userPhoneDto = _mapper.Map<UserPhoneDTO>(userPhone);

                return Ok(userPhoneDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
