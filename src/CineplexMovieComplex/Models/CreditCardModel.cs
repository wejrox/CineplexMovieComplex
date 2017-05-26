using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using CineplexMovieComplex.Models;

namespace CineplexMovieComplex.Models
{
    public class CreditCardModel
    {
        [CreditCard]
        public string CreditCardNumber { get; set; }
        public string CVV { get; set; }
        public string Expiry { get; set; }
        public string NameOnCard { get; set; }
    }
}
