using System;
using System.Collections.Generic;

namespace CineplexMovieComplex.Models
{
    public partial class Cart
    {
        public Cart()
        {
            MovieBooking = new HashSet<MovieBooking>();
        }

        public int CartId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? LastChange { get; set; }
        public bool? Finalised { get; set; }

        public virtual ICollection<MovieBooking> MovieBooking { get; set; }
    }
}
