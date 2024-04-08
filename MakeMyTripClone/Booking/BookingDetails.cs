using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTripClone
{
    public class BookingDetails
    {
        public string BusName { get; set; }
        public string Bustype { get; set; }
        public string Bookedseatnumber { get; set; }
        public int Nooftravellers { get; set; }
        public string Pickuptime { get; set; }
        public string Droptime { get; set; }
        public string Pickuplocation { get; set; }
        public string Droplocation { get; set; }
        public string[] Boardingpoint { get; set; }
        public string[] Droppoint { get; set; }
        public string Durations { get; set; }
        public int Totalamount { get; set; }
    }
}
