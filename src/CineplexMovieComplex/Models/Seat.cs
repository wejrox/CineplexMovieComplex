using System;
using System.Collections.Generic;

namespace CineplexMovieComplex.Models
{
    public partial class Seat
    {
        public Seat()
        {
            MovieTicket = new HashSet<MovieTicket>();
        }

        public int SeatId { get; set; }
        
        public int Row { get; set; }
        public int Number { get; set; }
        public string CombinedSeat
        {
            get
            {
                return string.Format("Row: {0}, Seat: {1}", Row, Number);
            }
        }
        public bool? Reserved { get; set; }
        public int CineplexMovieId { get; set; }

        public virtual ICollection<MovieTicket> MovieTicket { get; set; }
        public virtual CineplexMovie CineplexMovie { get; set; }
    }
}
