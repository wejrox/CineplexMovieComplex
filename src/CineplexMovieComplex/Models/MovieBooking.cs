using System;
using System.Collections.Generic;

namespace CineplexMovieComplex.Models
{
    public partial class MovieBooking
    {
        public int ReservationId { get; set; }
        public int CartId { get; set; }
        public int SeatId { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Seat Seat { get; set; }
    }
}
