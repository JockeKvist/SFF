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
    public class MoviesRentedsController : ControllerBase
    {
        private readonly Context _context;
        Movie m = new Movie();
        public MoviesRentedsController(Context context)
        {
            _context = context;
        }

        // GET: api/MoviesRenteds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoviesRented>>> GetRentedMovies()
        {
            return await _context.RentedMovies.ToListAsync();
        }

        // GET: api/MoviesRenteds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoviesRented>> GetMoviesRented(int id)
        {
            var moviesRented = await _context.RentedMovies.FindAsync(id);

            if (moviesRented == null)
            {
                return NotFound();
            }

            return moviesRented;
        }

        [HttpGet("MoviesRented/{id}")]

        public async Task<ActionResult<IEnumerable<MoviesRented>>> GetRentalsForStudio(int id)

        {

            return await _context.RentedMovies.Where(r => r.MoviestuidoId == id).ToListAsync();

        }

        // PUT: api/MoviesRenteds/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoviesRented(int id, MoviesRented moviesRented)
        {
            if (id != moviesRented.Id)
            {
                return BadRequest();
            }

            _context.Entry(moviesRented).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesRentedExists(id))
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

        // POST: api/MoviesRenteds
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MoviesRented>> PostMoviesRented(MoviesRented moviesRented)
        {
            var m = await _context.Movies.FindAsync(moviesRented.MovieId);

            m.Amount--;
            _context.Entry(moviesRented).State = EntityState.Modified;


            if (m.Amount >= 0)
            {

                _context.RentedMovies.Add(moviesRented);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMoviesRented", new { id = moviesRented.Id }, moviesRented);


            }
            else
            {
                m.Amount = 0;
                return null;
            }
        }

        // DELETE: api/MoviesRenteds/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MoviesRented>> DeleteMoviesRented(int id)
        {
            var moviesRented = await _context.RentedMovies.FindAsync(id);
            if (moviesRented == null)
            {
                return NotFound();
            }

            var m = await _context.Movies.FindAsync(moviesRented.MovieId);

            m.Amount++;
            _context.Entry(moviesRented).State = EntityState.Modified;

            _context.RentedMovies.Remove(moviesRented);
            await _context.SaveChangesAsync();

            return moviesRented;
        }

        private bool MoviesRentedExists(int id)
        {
            return _context.RentedMovies.Any(e => e.Id == id);
        }
    }
}
