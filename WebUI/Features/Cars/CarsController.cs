using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Features.Cars
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context) {  _context = context; }

        [HttpGet]
        public ActionResult<List<Car>> GetCars()
        {
            var cars = _context.Cars.ToList();

            return Ok(cars);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Car> GetCar(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);

            if (car == null)
            {
                return NotFound($"Car with ID: {id} has not been found.");
            }

            return Ok(car);
        }

        [HttpPost]
        public ActionResult<Car> CreateCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();

            return Ok(car);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Car> UpdateCar(Car car)
        {
            var dbCar = _context.Cars.FirstOrDefault(c => c.Id == car.Id);

            if (dbCar == null)
            {
                return NotFound($"Car with ID: {car.Id} has not been found");
            }

            dbCar.TeamName = car.TeamName;
            dbCar.Speed = car.Speed;
            dbCar.MalfunctionChance = car.MalfunctionChance;

            _context.SaveChanges();

            return Ok(dbCar);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCar(int id)
        {
            var dbCar = _context.Cars.FirstOrDefault(c => c.Id == id);
            
            if (dbCar == null)
            {
                return NotFound($"Car with ID: {id} has not been found");
            }

            _context.Cars.Remove(dbCar);
            _context.SaveChanges();

            return Ok($"Car with ID: {id} was succesfully deleted.");
        }
    }
}
