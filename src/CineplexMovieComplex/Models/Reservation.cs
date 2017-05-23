using System;
using System.Collections.Generic;

namespace CineplexMovieComplex.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            SeatReservedCineplexMovie = new HashSet<SeatReserved>();
            SeatReservedReservation = new HashSet<SeatReserved>();
        }

        public int ReservationId { get; set; }
        public int CineplexMovieId { get; set; }
        public bool? Reserved { get; set; }
        public bool? Paid { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<SeatReserved> SeatReservedCineplexMovie { get; set; }
        public virtual ICollection<SeatReserved> SeatReservedReservation { get; set; }
        public virtual CineplexMovie CineplexMovie { get; set; }
    }
}
