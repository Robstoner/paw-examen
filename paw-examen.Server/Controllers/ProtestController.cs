using ExamPrep.Server.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using paw_examen.Server.Models;

namespace paw_examen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProtestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProtestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protest>>> GetProteste()
        {
            return await _context.Proteste.Include(c => c.Locatie).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Protest>> GetProtest(int id)
        {
            var protest = await _context.Proteste.Include(c => c.Locatie).FirstOrDefaultAsync(c => c.Id == id);

            if (protest == null)
            {
                return NotFound();
            }

            return protest;
        }

        [HttpPost]
        public async Task<ActionResult<Protest>> CreateProtest(CreateProtestRequest protestReq)
        {
            var locatie = await _context.Locatii.FindAsync(protestReq.LocatieId);
            if (locatie == null)
            {
                return BadRequest("Locatia nu exista.");
            }

            if (protestReq.Numar_participanti > locatie.Numar_locuitori)
            {
                return BadRequest("Numarul de participanti nu poate fi mai mare decat numarul de locuitori ai locatiei.");
            }

            var protest = new Protest
            {
                Denumire = protestReq.Denumire,
                Descriere = protestReq.Descriere,
                LocatieId = locatie.Id,
                Numar_participanti = protestReq.Numar_participanti,
                Data = protestReq.Data,
            };

            _context.Proteste.Add(protest);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProtest), new { id = protest.Id }, protest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProtest(int id, Protest protest)
        {
            if (id != protest.Id)
            {
                return BadRequest();
            }
            if (protest.Locatie.Numar_locuitori < protest.Numar_participanti)
            {
                return BadRequest("Numarul de participanti nu poate fi mai mare decat numarul de locuitori ai locatiei.");
            }

            _context.Entry(protest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            } 
            catch (DbUpdateConcurrencyException)
            {
                if (!ProtestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProtest(int id)
        {
            var protest = await _context.Proteste.FindAsync(id);
            if (protest == null)
            {
                return NotFound();
            }

            _context.Proteste.Remove(protest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        public class CreateProtestRequest
        {
            public string Denumire { get; set; }
            public string Descriere { get; set; }
            public DateTime Data { get; set; }
            public int Numar_participanti { get; set; }

            public int LocatieId { get; set; }
        }

        private bool ProtestExists(int id)
        {
            return _context.Proteste.Any(e => e.Id == id);
        }
    }
}
