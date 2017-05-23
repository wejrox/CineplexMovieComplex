using System;
using System.Collections.Generic;

namespace CineplexMovieComplex.Models
{
    public partial class Cineplex
    {
        public Cineplex()
        {
            CineplexMovie = new HashSet<CineplexMovie>();
            Seat = new HashSet<Seat>();
        }

        public int CineplexId { get; set; }
        public string Location { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }

        public virtual ICollection<CineplexMovie> CineplexMovie { get; set; }
        public virtual ICollection<Seat> Seat { get; set; }
    }
}
