﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSF.Models;

namespace SSF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmstudiosController : ControllerBase
    {
        private readonly Context _context;
        MoviesRented checkmovies = new MoviesRented();

        public FilmstudiosController(Context context)
        {
            _context = context;
        }

        // GET: api/Filmstudios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filmstudio>>> GetFilmstudios()
        {
            return await _context.Filmstudios.ToListAsync();
        }

        // GET: api/Filmstudios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Filmstudio>> GetFilmstudio(int id)
        {
            var filmstudio = await _context.Filmstudios.FindAsync(id);


            if (filmstudio == null)
            {
                return NotFound();
            }
            
            return filmstudio;
        }

        // PUT: api/Filmstudios/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFilmstudio(int id, Filmstudio filmstudio)
        {
            if (id != filmstudio.Id)
            {
                return BadRequest();
            }

            _context.Entry(filmstudio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmstudioExists(id))
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

        // POST: api/Filmstudios
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Filmstudio>> PostFilmstudio(Filmstudio filmstudio)
        {
            _context.Filmstudios.Add(filmstudio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFilmstudio", new { id = filmstudio.Id }, filmstudio);
        }

        // DELETE: api/Filmstudios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Filmstudio>> DeleteFilmstudio(int id)
        {
            var filmstudio = await _context.Filmstudios.FindAsync(id);
            if (filmstudio == null)
            {
                return NotFound();
            }

            _context.Filmstudios.Remove(filmstudio);
            await _context.SaveChangesAsync();

            return filmstudio;
        }

        private bool FilmstudioExists(int id)
        {
            return _context.Filmstudios.Any(e => e.Id == id);
        }
    }
}
