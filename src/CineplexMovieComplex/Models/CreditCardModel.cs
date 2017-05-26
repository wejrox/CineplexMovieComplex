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
        
        public List<string> PosExpiryMonth
        {
            get
            {
                return new List<string> { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11" };
            }
        } 
        public List<string> PosExpiryYear
        {
            get
            {
                PosExpiryYear = new List<string>();
                for (int i = 0; i < 100; i++)
                    PosExpiryYear.Add(i.ToString());
                return PosExpiryYear;
            }
            set
            {
                PosExpiryYear = value;
            }
        } 
    }
}
