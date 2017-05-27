using System;
using System.Collections.Generic;

namespace CineplexMovieComplex.Models
{
    public partial class CineplexMovie
    {
        public CineplexMovie()
        {
            Seat = new HashSet<Seat>();
        }

        public int CineplexMovieId { get; set; }
        public int CineplexId { get; set; }
        public int MovieId { get; set; }
        public string Day { get; set; }
        public TimeSpan ViewingTime { get; set; }
        public int SeatsAvailable { get; set; }

        public virtual ICollection<Seat> Seat { get; set; }
        public virtual Cineplex Cineplex { get; set; }
        public virtual Movie Movie { get; set; }

        
    }
}
