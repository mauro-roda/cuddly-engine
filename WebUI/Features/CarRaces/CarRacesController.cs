using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using WebUI.Features.CarRaces.Models;

namespace WebUI.Features.CarRaces
{
    [Route("api/carraces")]
    [ApiController]
    public class CarRacesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CarRaceService service { get; set; } = new CarRaceService();

        public CarRacesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [SwaggerOperation(Summary = "Get all races")]
        [HttpGet]
        public ActionResult<List<CarRace>> GetCarRaces()
        {
            var carRaces = _context.CarRaces.Include(x => x.Cars).ToList();

            return Ok(carRaces);
        }

        [SwaggerOperation(Summary = "Get a specific race")]
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetCarRace(int id)
        {
            var carRace = _context.CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(x => x.Id == id);

            if (carRace == null)
            {
                return NotFound();
            }

            return Ok(carRace);
        }

        [SwaggerOperation(Summary = "Create a car race")]
        [HttpPost]
        public ActionResult CreateCarRaces(CarRaceCreateModel carRace)
        {
            var newCarRace = new CarRace
            {
                Name = carRace.Name,
                Location = carRace.Location,
                Distance = carRace.Distance,
                TimeLimit = carRace.TimeLimit,
                Status = ""
            };

            _context.CarRaces.Add(newCarRace);
            _context.SaveChanges();

            return Ok(newCarRace);
        }

        [SwaggerOperation(Summary = "Update a race")]
        [HttpPut]
        public ActionResult UpdateCarRaces(CarRaceUpdateModel carRace)
        {
            var dbCarRace = _context.CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(x => x.Id == carRace.Id);

            if (dbCarRace == null)
            {
                return NotFound($"Car Race with ID: {carRace.Id} was not found.");
            }

            dbCarRace.Name = carRace.Name;
            dbCarRace.Location = carRace.Location;
            dbCarRace.Distance = carRace.Distance;
            dbCarRace.TimeLimit = carRace.TimeLimit;

            _context.CarRaces.Update(dbCarRace);
            _context.SaveChanges();

            return Ok(dbCarRace);
        }

        [SwaggerOperation(Summary = "Add car to race")]
        [HttpPut]
        [Route("{carRaceId}/addcar/{carID}")]
        public ActionResult AddCarToCarRace(int carRaceId, int carID)
        {
            var dbCarRace = _context.CarRaces
                .Include(x => x.Cars)
                .SingleOrDefault(x => x.Id == carRaceId);

            if (dbCarRace == null)
            {
                return NotFound($"Car Race with ID: {carRaceId} was not found.");
            }

            var dbCar = _context.Cars
                .SingleOrDefault(x => x.Id == carID);

            if (dbCar == null)
            {
                return NotFound($"Car with ID: {carID} was not found.");
            }

            dbCarRace.Cars.Add(dbCar);
            _context.SaveChanges();

            return Ok(dbCarRace);
        }

        [SwaggerOperation(Summary = "Start a race")]
        [HttpPut]
        [Route("{id}/start")]
        public ActionResult StartCarRaces(int id)
        {
            var carRace = _context.CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(x => x.Id == id);

            if (carRace == null)
            {
                return NotFound($"Car Race with ID: {id} was not found.");
            }

            carRace.Status = "Started";
            var result = service.RunRace(carRace);
            _context.SaveChanges();

            return Ok(result);
        }

        [SwaggerOperation(Summary = "Delete a race")]
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCarRace(int id)
        {
            var dbCarRace = _context.CarRaces
                .Include(x => x.Cars)
                .FirstOrDefault(x => x.Id == id);

            if (dbCarRace == null)
            {
                return NotFound($"Car Race with ID: {id} was not found.");
            }

            _context.Remove(dbCarRace);
            _context.SaveChanges();

            return Ok($"Car Race with ID: {id} was succesfully deleted.");
        }
    }
}
