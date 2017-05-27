using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CineplexMovieComplex.Models
{
    public class PurchaseDetails
    {
        public List<MovieTicket> MovieTickets { get; set; }
        public int NumConcession { get; set; }
        public int NumAdult { get; set; }
        public double ConcessionTotalCost { get; set; }
        public double AdultTotalCost { get; set; }
        public double ConcessionPrice { get; } = 20.0f;
        public double AdultPrice { get; } = 45.0f;

        public double TotalCost {
            get {
                return ConcessionTotalCost + AdultTotalCost;
            }
        }

        // Sets the cost and amount of tickets
        public void CalculateDetails()
        {
            foreach (MovieTicket m in MovieTickets)
            {
                if (m.Concession)
                {
                    ConcessionTotalCost += ConcessionPrice;
                    NumConcession++;
                }
                else if (m.Concession == false)
                {
                    AdultTotalCost += AdultPrice;
                    NumAdult++;
                }
            }
        }
    }
}
