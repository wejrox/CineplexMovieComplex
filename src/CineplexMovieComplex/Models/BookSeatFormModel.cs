using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineplexMovieComplex.Models
{
    public class BookSeatFormModel
    {
        public Seat SelectedSeat { get; set; }
        public CineplexMovie CineplexMovieId { get; set; }
    }
}
