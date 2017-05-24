using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CineplexMovieComplex.Models
{
    public class Enquiry
    {
        [Required]
        public int EnquiryId { get; set; }
        [Required, Display(Name ="Your Name")]
        public string FromName { get; set; }
        [Required, Display(Name ="Your Email"), EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
