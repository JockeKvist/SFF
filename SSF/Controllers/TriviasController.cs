using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSF.Models;

namespace SSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviasController : ControllerBase
    {
        private readonly Context _context;

        public TriviasController(Context context)
        {
            _context = context;
        }

        // GET: api/Trivias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trivia>>> GetTrivia()
        {
            return await _context.Trivia.ToListAsync();
        }

        // GET: api/Trivias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Trivia>> GetTrivia(int id)
        {
            var trivia = await _context.Trivia.FindAsync(id);

            if (trivia == null)
            {
                return NotFound();
            }

            return trivia;
        }

        // PUT: api/Trivias/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrivia(int id, Trivia trivia)
        {
            if (id != trivia.Id)
            {
                return BadRequest();
            }

            _context.Entry(trivia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TriviaExists(id))
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

        // POST: api/Trivias
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Trivia>> PostTrivia(Trivia trivia)
        {
            _context.Trivia.Add(trivia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrivia", new { id = trivia.Id }, trivia);
        }

        // DELETE: api/Trivias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Trivia>> DeleteTrivia(int id)
        {
            var trivia = await _context.Trivia.FindAsync(id);
            if (trivia == null)
            {
                return NotFound();
            }

            _context.Trivia.Remove(trivia);
            await _context.SaveChangesAsync();

            return trivia;
        }

        private bool TriviaExists(int id)
        {
            return _context.Trivia.Any(e => e.Id == id);
        }
    }
}
