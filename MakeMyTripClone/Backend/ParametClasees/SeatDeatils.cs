using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTrip
{
    class SeatDeatils
    {

        public int SeatId { get; set; }
        public int RouteId { get; set; }
        public string SeatType { get; set; }
        public Boolean IsBooked { get; set; }
        public Boolean IsWindow { get; set; }
        public int Price { get; set; }
        public int CId { get; set; }

    }
}
