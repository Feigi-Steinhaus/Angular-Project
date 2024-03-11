using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Classes
{
    public class TripsDTO
    {
        public int TripCode { get; set; }
        public string TripDestination { get; set; }
        public int TypeCode { get; set; }
        public DateTime TripDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public int TripDurationHours { get; set; }
        public int AvailablePlaces { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }

        //--
        public string TypeName { get; set; }
        public bool IsMedicNeeded { get; set; }
    }
}
