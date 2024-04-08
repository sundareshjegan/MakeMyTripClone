using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTripClone
{
    public class BusDetails
    {
        public int BusId { get; set; }

        public string BusName { get; set; }

        public int RouteID { get; set; }

        public bool IsAC { get; set; }

        public SeatType SeatType { get; set; }

        public TimePeriod TimePeriod { get; set; }
    }
}
