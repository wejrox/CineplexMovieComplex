using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CineplexMovieComplex.Models
{
    public partial class Cart
    {
        public Cart()
        {
            MovieTicket = new HashSet<MovieTicket>();
        }
        [Key]
        public int CartId { get; set; }
        public string CustomerName { get; set; }
        public DateTime? LastChange { get; set; }
        public bool? Finalised { get; set; }

        public virtual ICollection<MovieTicket> MovieTicket { get; set; }
    }
}
