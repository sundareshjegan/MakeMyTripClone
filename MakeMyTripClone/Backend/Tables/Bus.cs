using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTrip
{
    class Bus
    {
        public static String TableName { get; } = "bus";

        public static String Id { get; } = "bus_id";

        public static String Name { get; } = "bus_name";

        public static String Number { get; } = "bus_no";

        public static String Owner { get; } = "bus_owner";

        public static String Type { get; } = "bus_type";

        public static String StartTime { get; } = "start_time";

        public static String EndTime { get; } = "end_time";

        public static String Duration { get; } = "duration";

        public static String Prices { get; } = "prices";

        public static String NoOfSeats { get; } = "no_of_seats";

        public static String DriverId { get; } = "d_id";

        public static String ServiceDate { get; } = "service_date";

        public static String Image { get; } = "image";

    }
}
