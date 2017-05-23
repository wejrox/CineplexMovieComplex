using System;
using System.Collections.Generic;

namespace CineplexMovieComplex.Models
{
    public partial class Seat
    {
        public Seat()
        {
            SeatReserved = new HashSet<SeatReserved>();
        }

        public int SeatId { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public bool? Reserved { get; set; }
        public int CineplexId { get; set; }

        public virtual ICollection<SeatReserved> SeatReserved { get; set; }
        public virtual Cineplex Cineplex { get; set; }
    }
}
