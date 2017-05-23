using System;
using System.Collections.Generic;

namespace CineplexMovieComplex.Models
{
    public partial class SeatReserved
    {
        public int SeatReservedId { get; set; }
        public int SeatId { get; set; }
        public int ReservationId { get; set; }
        public int CineplexMovieId { get; set; }

        public virtual Reservation CineplexMovie { get; set; }
        public virtual Reservation Reservation { get; set; }
        public virtual Seat Seat { get; set; }
    }
}
