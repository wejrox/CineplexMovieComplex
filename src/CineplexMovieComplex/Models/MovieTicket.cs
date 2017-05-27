using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineplexMovieComplex.Models
{
    public partial class MovieTicket
    {
        [Key]
        public int MovieTicketId { get; set; }
        [Display(Name = "Cart ID")]
        public int CartId { get; set; }
        [Display(Name = "Seat ID")]
        public int SeatId { get; set; }
        public bool Concession { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Seat Seat { get; set; }
    }
}
