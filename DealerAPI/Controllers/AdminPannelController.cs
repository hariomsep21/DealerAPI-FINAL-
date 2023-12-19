using Dealer.Model;
using Dealer.Model.DTO;
using DealerAPI.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAppAPI.Controllers;

namespace DealerAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class AdminPannelController:ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public AdminPannelController( ApplicationDbContext db)
        {

            _db = db;
        }

        [HttpGet("Cars")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<CarsDto>>> GetAllCarsBasicInfo()
        {
            try
            {
                

                var cars = await _db.Cars
                    .Select(car => new CarsDto
                    {
                        CarId = car.CarId,
                        CarName = car.CarName,
                        Variant = car.Variant,
                        UserId = car.UserId,
                    })
                    .ToListAsync();

                return Ok(cars);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPost("cars")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddCar([FromBody] CarsDto newCarDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCar = new Car
                    {
                        CarName = newCarDto.CarName,
                        Variant = newCarDto.Variant,
                        CarId = newCarDto.CarId,
                        UserId = newCarDto.UserId,
                 
                    };

                    _db.Cars.Add(newCar);
                    await _db.SaveChangesAsync();

                    return Ok("Successfully added to the database"); // Returns a 200 OK with a success message
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("cars/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] CarsDto updatedCarDto)
        {
            try
            {
                var existingCar = await _db.Cars.FindAsync(id);

                if (existingCar == null)
                {
                    return NotFound(); // Return 404 Not Found if the car with the provided ID doesn't exist
                }

                if (ModelState.IsValid)
                {
                    existingCar.CarName = updatedCarDto.CarName;
                    existingCar.Variant = updatedCarDto.Variant;

                    existingCar.UserId = updatedCarDto.UserId;

                    _db.Cars.Update(existingCar);
                    await _db.SaveChangesAsync();

                    return Ok("Successfully updated the car"); // Returns a 200 OK with a success message
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("cars/{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                var existingCar = await _db.Cars.FindAsync(id);

                if (existingCar == null)
                {
                    return NotFound(); // Return 404 Not Found if the car with the provided ID doesn't exist
                }

                _db.Cars.Remove(existingCar);
                await _db.SaveChangesAsync();

                return Ok("Successfully deleted the car"); // Returns a 200 OK with a success message
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, ex);
            }
        }


    }
}
