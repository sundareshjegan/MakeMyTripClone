using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeMyTripClone
{
    class Customer
    {
        public static String TableName { get; } = "customer";

        public static String Id { get; } = "c_id";

        public static String Name { get; } = "c_name";

        public static String Email { get; } = "c_email";

        public static String Phone { get; } = "c_phone_no";

        public static String Password { get; } = "c_password";

        public static String Gender { get; } = "c_gender";
    }
}
