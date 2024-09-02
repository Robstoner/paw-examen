using ExamPrep.Server.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paw_examen.Server.Models;

namespace paw_examen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocatieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LocatieController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locatie>>> GetLocatii()
        {
            return await _context.Locatii.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Locatie>> GetLocatie(int id)
        {
            var locatie = await _context.Locatii.FindAsync(id);

            if (locatie == null)
            {
                return NotFound();
            }

            return locatie;
        }
    }
}
