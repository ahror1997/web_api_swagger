using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Records;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ApplicationContext context;

        public CarController(ApplicationContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Returns list of cars
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetCars([FromQuery] int count)
        {
            //string[] cars = { "BMW", "Range Rover", "Honda", "Audi" };

            if (count <= 0)
            {
                throw new ArgumentException("Invalid count", nameof(count));
            }
                
            var cars = await context.Cars.ToListAsync();
            return Ok(cars.Take(count));
        }

        [HttpPost]
        public async Task<ActionResult> CreateCar([FromBody] CarRecord carRecord)
        {
            Car car = new Car
            {
                Name = carRecord.Name,
                Description = carRecord.Description,
                Year = carRecord.Year
            };

            context.Cars.Add(car);
            await context.SaveChangesAsync();
            return Created("wtf", car);
        }

    }
}
