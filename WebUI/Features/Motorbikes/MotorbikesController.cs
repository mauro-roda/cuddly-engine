using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Features.Motorbikes
{
    [Route("api/motorbikes")]
    [ApiController]
    public class MotorbikesController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public MotorbikesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Motorbike>> GetMotorbikes()
        {
            var motos = _context.Motorbikes.ToList();

            return Ok(motos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Motorbike> GetMotorbike(int id)
        {
            var moto = _context.Motorbikes.FirstOrDefault(m => m.Id == id);

            if (moto == null)
            {
                return NotFound($"Motorbike with ID: {id} has not been found.");
            }

            return Ok(moto);
        }

        [HttpPost]
        public ActionResult<Motorbike> CreateMotorbike(Motorbike motorbike)
        {
            _context.Motorbikes.Add(motorbike);
            _context.SaveChanges();

            return Ok(motorbike);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<Motorbike> UpdateMotorbike(Motorbike motorbike)
        {
            var dbMoto = _context.Motorbikes.FirstOrDefault(c => c.Id == motorbike.Id);

            if (dbMoto == null)
            {
                return NotFound($"Motorbike with ID: {motorbike.Id} has not been found.");
            }

            dbMoto.TeamName = motorbike.TeamName;
            dbMoto.Speed = motorbike.Speed;
            dbMoto.MalfunctionChance = motorbike.MalfunctionChance;

            _context.SaveChanges();

            return Ok(dbMoto);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteMotorbike(int id)
        {
            var dbMoto = _context.Motorbikes.FirstOrDefault(m => m.Id == id);

            if (dbMoto == null)
            {
                return NotFound($"Motorbike with ID: {id} has not been found");
            }

            _context.Motorbikes.Remove(dbMoto);
            _context.SaveChanges();

            return Ok($"Motorbike with ID: {id} was succesfully deleted.");
        }
    }
}
