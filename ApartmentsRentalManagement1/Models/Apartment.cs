using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApartmentsRentalManagement1.Models
{
    public class Apartment
    {
        [Display(Name = "Apartment ID")]
        public int ApartmentID { get; set; }
        [Display(Name = "Unit #")]
        public string UnitNumber { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        [Display(Name = "Monthly Rate")]
        public int MonthlyRate { get; set; }
        [Display(Name = "Deposit")]
        public int SecurityDeposit { get; set; }
        [Display(Name = "Occupancy Status")]
        public string OccupancyStatus { get; set; }

        public string Residence
        {
            get
            {
                string beds = Bedrooms + " bedrooms";
                string baths = Bathrooms + " bathrooms";

                if (Bedrooms == 1)
                    beds = Bedrooms + " bedroom";
                if (Bathrooms == 1)
                    baths = Bedrooms + " bathroom";

                return UnitNumber + " - " + beds + ", " + baths + ", rent = " +
                       MonthlyRate.ToString() + "/month, deposit = " +
                       SecurityDeposit.ToString() + ", " + OccupancyStatus;
            }
        }
    }
}