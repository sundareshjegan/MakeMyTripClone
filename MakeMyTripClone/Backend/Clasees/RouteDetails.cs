using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTripClone
{
    public class RouteDetails
    {

      
        public int RouteId { get; set; }
        public int BusId { get; set; }
        public String BusName { get; set; }
        public String BusType { get; set; }
        public String StartDate  { get; set; }
        public String EndDate { get; set; }
        public String Source { get; set; }
        public String Destination { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public String Price { get; set; }
        public String NoOfSeats { get; set; }
        public string Duration { get; set; }
        public List<String> BoardingPoints { get; set; }
        public List<String> DropintPoints { get; set; }
    }
}
