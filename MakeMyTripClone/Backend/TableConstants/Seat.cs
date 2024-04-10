using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTripClone
{
    class Seat
    {
        public static string TableName = "seat";

        public static String Id { get; } = "seat_id";

        public static String RouteId { get; } = "r_id";

        public static String SeatType { get; } = "seat_type";

        public static String IsBooked { get; } = "is_booked";

        public static String IsWindow { get; } = "is_window";

        public static String Price { get; } = "price";

        public static String CustomerId { get; } = "c_id";
    }
}
