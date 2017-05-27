using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineplexMovieComplex.Models
{
    public class BookSeatFormModel
    {
        public int SelectedSeatId { get; set; }
        public bool Concession { get; set; }
        public CineplexMovie CineplexMovie { get; set; }
    }
}
