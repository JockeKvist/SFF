using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace SSF.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {}
        public DbSet<Filmstudio> Filmstudios { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MoviesRented> RentedMovies { get; set; }
        public DbSet<Trivia> Trivia { get; set; }
        public DbSet<Delivery> LeveransXML { get; set; }
    }
}
