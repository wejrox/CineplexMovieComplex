using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CineplexMovieComplex.Models
{
    public class CreditCardModel
    {
        [CreditCard, Required]
        public string CreditCardNumber { get; set; }
        [StringLength(3), Required]
        public string CVV { get; set;} 
        [Required]
        public int ExpiryMonth { get; set; }
        [Required]
        public int ExpiryYear { get; set; }
        [StringLength(40), Required]
        public string NameOnCard { get; set; }
        
        public IEnumerable<string> PosExpiryMonth { get; private set; }
        public List<string> PosExpiryYear { get; private set; }

        public CreditCardModel()
        {
            PosExpiryYear = new List<string>();
            for (int i = 0; i < 10; i++)
                PosExpiryYear.Add("0" + i.ToString());
            for (int i = 10; i < 100; i++)
                PosExpiryYear.Add(i.ToString());

            PosExpiryMonth = new List <string> { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11" };
        }
    }
}
