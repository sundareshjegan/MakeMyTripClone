
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GoLibrary;
using DatabaseLibrary;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MakeMyTrip
{
    static class DBManager
    {
        private static readonly string server = "192.168.3.";
        private static readonly string database = "makemytrip";
        private static readonly string user = "root";
        private static readonly string password = "databaseB7&";

        private static readonly string connectionString = $"server={server};user={user};password={password};database={database}";

        public static MySqlConnection Connection = null;
        public static DatabaseManager manager = new MySqlHandler(server, user, password, database);


        private static string insertquery;
        private static string updatequery;
        private static string deletequery;
        private static string query;

        public static void GetConnection()
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
            manager.Connect();
            
        }


        #region User

        public static Boolean IsUserExisted()
        {

            List<Object> list = manager.FetchColumn("customer", "c_email", "c_email = 'leos@gmail.com'" );

           

            return false;
        }


        public static void AddUser(CustomerDetails cd)
        {
         ////   insertQuery = $"insert into customer values ( ${cd.Name} , ${cd.Email} , ${cd.Phone} , ${cd.Password}, ${cd.Gender} )";
         //   MySqlCommand cmd = new MySqlCommand(query, Connection);
         //   cmd.ExecuteNonQuery();
        }



        #endregion

        public static void Insert()
        {
            if (manager.IsConnected) ;
             //manager.InsertData("customer", 1, "Leo", "leodas@gmail.com", 945657, "bloodysweet", "M");
          IsUserExisted();
        }




    }
}
