using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Features.Motorbikes
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorbikesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Motorbike>> GetMotorbikes()
        {
            var motos = new List<Motorbike>();
            var moto1 = new Motorbike()
            {
                TeamName = "Team A",
                Speed = 100,
                MalfunctionChance = 0.2
            };

            var moto2 = new Motorbike()
            {
                TeamName = "Team B",
                Speed = 90,
                MalfunctionChance = 0.15
            };

            motos.Add(moto1);
            motos.Add(moto2);

            return Ok(motos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Motorbike> GetMotorbike(int id)
        {
            var moto1 = new Motorbike()
            {
                TeamName = "Team A",
                Speed = 100,
                MalfunctionChance = 0.2
            };

            return Ok(moto1);
        }

        [HttpPost]
        public ActionResult<Motorbike> CreateMotorbike(Motorbike motorbike)
        {
            var newMoto = new Motorbike()
            {
                Id = motorbike.Id,
                TeamName = motorbike.TeamName,
                Speed = motorbike.Speed,
                MalfunctionChance = motorbike.MalfunctionChance
            };

            return Ok(newMoto);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Motorbike> UpdateMotorbike(Motorbike motorbike)
        {
            var updateMoto = new Motorbike()
            {
                Id = motorbike.Id,
                TeamName = motorbike.TeamName,
                Speed = motorbike.Speed,
                MalfunctionChance = motorbike.MalfunctionChance
            };

            return Ok(updateMoto);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteMotorbike(int id)
        {
            return Ok($"Motorbike with ID: {id} was succesfully deleted.");
        }
    }
}
