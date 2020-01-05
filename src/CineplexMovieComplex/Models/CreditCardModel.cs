using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CineplexMovieComplex.Models
{
    public class CreditCardModel
    {
        [CreditCard(ErrorMessage = "The Credit Card number you have entered is invalid"), Required]
        public string CreditCardNumber { get; set; }
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CCV must be 3 or 4 numbers"), Required]
        public string CVV { get; set;} 
        [Required, StringLength(2)]
        public int ExpiryMonth { get; set; }
        [Required, StringLength(2)]
        public int ExpiryYear { get; set; }
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Please ensure you have entered all of your card name"), Required]
        public string NameOnCard { get; set; }
        
        public IEnumerable<string> PosExpiryMonth { get; private set; }
        public IEnumerable<string> PosExpiryYear { get; private set; }

        public CreditCardModel()
        {
            List<string> posyrtmp = new List<string>();
            for (int i = 0; i < 10; i++)
                posyrtmp.Add("0" + i.ToString());
            for (int i = 10; i < 100; i++)
                posyrtmp.Add(i.ToString());

            // We want it read only
            PosExpiryYear = posyrtmp as IEnumerable<string>;

            PosExpiryMonth = new List <string> { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
        }
    }
}
