using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTrip
{
    class Route
    {

        public static String TableName { get; } = "route";

        public static String Id { get; } = "r_id";

        public static String BusId { get; } = "bus_id";

        public static String StartDate { get; } = "start_date";

        public static String EndDate { get; } = "end_date";

        public static String Boarding { get; } = "source";

        public static String Destination { get; } = "destination";

        public static String StartTime { get; } = "start_time";

        public static String EndTime { get; } = "end_time";

        public static String BoardingPoints { get; } = "boarding_points";

        public static String DropPoints { get; } = "drop_points";


    }
}
