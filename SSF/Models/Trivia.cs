using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSF.Models
{
    public class Trivia
    {
        public int Id { get; set; }
        public int FilmstudioId { get; set; }
        public int MovieId { get; set; }
        public string TriviaInfo { get; set; }
        public int Rating { get; set; }
    }
}
