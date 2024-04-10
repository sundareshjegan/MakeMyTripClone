
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GoLibrary;
using DatabaseLibrary;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MakeMyTripClone
{
    static class DBManager
    {
        private static readonly string server = "192.168.3.63";
        private static readonly string database = "makemytrip";
        private static readonly string user = "team7";
        private static readonly string password = "team7team7";

        private static readonly string connectionString = $"server={server};user={user};password={password};database={database}";

        public static MySqlConnection Connection = null;
        public static DatabaseManager manager = new MySqlHandler(server, user, password, database);


        public static void GetConnection()
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
            manager.Connect();

        }


        #region SignUpUser

        public static Boolean IsUserExisted(String email)
        {
            // Customer -  class contains table name and column names
            var res = manager.FetchData(Customer.TableName, $"{Customer.Email}= '{email}' ", -1,-1,"",Customer.Email).Value;
            return res.Count == 0;

        }
        public static void AddUser(CustomerDetails cd)
        {
            if (manager.IsConnected) 
               manager.InsertData(Customer.TableName, cd.Name, cd.Email, cd.Phone, cd.Password, cd.Gender);
        }

        #endregion

        #region Signin

        public static String Verify(String email, String password)
        {
           // GetBuses("", "", new DateTime());

            if (IsUserExisted(email))
                return "You dont have an account";

            var res = manager.FetchColumn(Customer.TableName, Customer.Password, $"{Customer.Email}= '{email}' ").Value;

            string dbPassword = res[0].ToString();


            if (dbPassword.Equals(password))
                return "";
            else
                return "Password does not match";

        }

        #endregion



        public static List<RouteDetails> GetBuses(String boarding , String destination , String date)
        {
           
            var res = manager.FetchData(Route.TableName, $"{Route.Boarding} = '{boarding}' and {Route.Destination} = '{destination}' and {Route.StartDate} = '{date}'").Value;

            List<RouteDetails> list = new List<RouteDetails>();
            int size = res[Route.Id].Count;

        
            for(int i = 0; i < size; i++)
            {
                RouteDetails rd = new RouteDetails();
                rd.BusId = Convert.ToInt32(manager.FetchColumn(Bus.TableName, Bus.Id, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0]);
                rd.RouteId = int.Parse(res[Route.Id][i].ToString());
                rd.BusName = manager.FetchColumn(Bus.TableName, Bus.Name, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString();
                rd.BusType = manager.FetchColumn(Bus.TableName, Bus.Type, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString();
                rd.StartTime = res[Route.StartTime][i].ToString();
                rd.StartDate = res[Route.StartDate][i].ToString();
                rd.EndTime = res[Route.EndTime][i].ToString();
                rd.EndDate = res[Route.EndDate][i].ToString();
                rd.Source = res[Route.Boarding][i].ToString();
                rd.Destination = res[Route.Destination][i].ToString();
                rd.Price = manager.FetchColumn(Bus.TableName, Bus.Prices, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString();
                rd.NoOfSeats = manager.FetchColumn(Bus.TableName, Bus.NoOfSeats, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString();
                rd.Duration = res[Route.Duration][i].ToString();

                DateTime now = DateTime.Now;
                DateTime bustime = Convert.ToDateTime(rd.StartTime);
                //if (bustime > now)
                //{
                    list.Add(rd);
                //}
                
            }
            return list;
        }

        public static List<object> GetBoardingPoints(String boarding, String destination, String date)
        {
           
            var res = manager.FetchColumn(Route.TableName, Route.BoardingPoints, $"{Route.Destination} = '{destination}' and {Route.Boarding} = '{boarding}' and {Route.StartDate} = '{date}'").Value;

            dynamic jsonObject = JsonConvert.DeserializeObject(res[0].ToString());

           // Extracting only values
            List<object> boardingPoints = new List<object>();
            ExtractValues(jsonObject, boardingPoints);


            return boardingPoints;


        }

        public static List<object> GetBoarding(String boarding, String destination, String date,int rootid)
        {

            var res = manager.FetchColumn(Route.TableName, Route.BoardingPoints, $"{Route.Destination} = '{destination}' and {Route.Boarding} = '{boarding}' and {Route.StartDate} = '{date}' and {Route.Id}='{rootid}'").Value;

            dynamic jsonObject = JsonConvert.DeserializeObject(res[0].ToString());

            // Extracting only values
            List<object> boardingPoints = new List<object>();
            ExtractValues(jsonObject, boardingPoints);


            return boardingPoints;
        }

        public static List<object> GetDrop(String boarding, String destination, String date,int rootid)
        {
            var res = manager.FetchColumn(Route.TableName, Route.DropPoints, $"{Route.Destination} = '{destination}' and {Route.Boarding} = '{boarding}' and {Route.StartDate} = '{date}' and {Route.Id}='{rootid}'").Value;


            dynamic jsonObject = JsonConvert.DeserializeObject(res[0].ToString());
            List<object> dropPoints = new List<object>();
            ExtractValues(jsonObject, dropPoints);

            return dropPoints;
        }



        public static List<object> GetDropPoints(String boarding, String destination, String date)
        {
            var res = manager.FetchColumn(Route.TableName, Route.DropPoints, $"{Route.Destination} = '{destination}' and {Route.Boarding} = '{boarding}' and {Route.StartDate} = '{date}'").Value;


            dynamic jsonObject = JsonConvert.DeserializeObject(res[0].ToString());
            List<object> dropPoints = new List<object>();
            ExtractValues(jsonObject, dropPoints);

            return dropPoints;

        }

        public static BooleanMsg<List<String>> GetTravel(String boarding, String destination, String date)
        {
            var res = manager.FetchColumn(Route.TableName, Route.BusId, $"{Route.Boarding} = '{boarding}' and {Route.Destination} = '{destination}' and {Route.StartDate} = '{date}'");
            List<String> travels = new List<String>();
            if (res && res.Value.Count != 0)
            {
                for (int i = 0; i < res.Value.Count; i++)
                {
                    travels.Add(manager.FetchColumn(Bus.TableName, Bus.Name, $"{Bus.Id} = {res.Value[i]} ").Value[0].ToString());
                }
            }
            return travels;
        }

        public static void ExtractValues(dynamic obj, List<object> valuesList)
        {
            foreach (var property in obj)
            {
                if (property.Value is Newtonsoft.Json.Linq.JObject)
                {
                    ExtractValues(property.Value, valuesList);
                }
                else
                {
                    valuesList.Add(property.Value);
                }
            }
        }


    }
}
