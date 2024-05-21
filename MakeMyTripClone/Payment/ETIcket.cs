using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTripClone
{ 
    public class ETicket
    {
        public string BookingID { get; set; }
        public string JourneyDate { get; set; }
        public string DepartureTime { get; set; }
        public string SourceCity { get; set; }
        public string DestinationCity { get; set; }
        public string BusName { get; set; }
        public string Contact { get; set; }
        public string[,] PassengerDetails { get; set; }
        public string TotalFare { get; set; }
        public string PaymentMethod { get; set; }
        public string EmailToSendTicket { get; set; }
    }
}
