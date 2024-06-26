﻿
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GoLibrary;
using DatabaseLibrary;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MakeMyTrip
{
    static class DBManager
    {


        private static readonly string server = "localhost";
        private static readonly string database = "makemytrip";
        private static readonly string user = "root";
        private static readonly string password = "databaseB7&";

        private static readonly string connectionString = $"server={server};user={user};password={password};database={database}";

        public static MySqlConnection Connection = null;
        public static DatabaseManager manager = new MySqlHandler(server, user, password, database);

        private static int busId ;

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

            if (manager.IsConnected) ;
               manager.InsertData(Customer.TableName, cd.Name, cd.Email, cd.Phone, cd.Password, cd.Gender);

        }

        #endregion

        #region Signin

        public static String Verify(String email, String password)
        {


            GetBoardingPoints("", "", "");

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



        public static List<RouteDetails> GetBuses(String boarding , String destination , DateTime date1)
        {
            boarding = "Coimbatore";
            destination = "Chennai";

            String date  = new DateTime(2024, 3, 27).ToString("yyyy-MM-dd");

            var res = manager.FetchData(Route.TableName, $"{Route.Boarding} = '{boarding}' and {Route.Destination} = '{destination}' and {Route.StartDate} = '{date}'").Value;

            List<RouteDetails> list = new List<RouteDetails>();
            int size = res["r_id"].Count;

        
            for(int i = 0; i < size; i++)
            {
                RouteDetails rd = new RouteDetails();

                rd.BusName = manager.FetchColumn(Bus.TableName, Bus.Name, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString();
                rd.BusType = manager.FetchColumn(Bus.TableName, Bus.Type, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString();
                rd.StartTime = res[Route.StartTime][i].ToString();
                rd.StartDate = res[Route.StartDate][i].ToString();
                rd.EndTime = res[Route.EndTime][i].ToString();
                rd.EndDate = res[Route.EndDate][i].ToString();
                rd.Boarding = res[Route.Boarding][i].ToString();
                rd.Destination = res[Route.Destination][i].ToString();
                rd.Price = manager.FetchColumn(Bus.TableName, Bus.Prices, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString();
                rd.NoOfSeats = manager.FetchColumn(Bus.TableName, Bus.NoOfSeats, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString();
        
                list.Add(rd);
                
            }

            GetTravel(boarding, destination, date);
            return list;

        }
          
        public static List<object> GetBoardingPoints(String boarding, String destination, String date)
        {
            busId = 1;
            boarding = "Chennai";
            destination = "Coimbatore";
            date = "28-03-2024";
            var res = manager.FetchColumn(Route.TableName ,Route.BoardingPoints, $"{Route.BusId} = {busId} and {Route.Boarding} = '{boarding}'").Value;


            dynamic jsonObject = JsonConvert.DeserializeObject(res[0].ToString());

            // Extracting only values
            List<object> boardingPoints = new List<object>();
            ExtractValues(jsonObject, boardingPoints);


            return boardingPoints;


        }

        public static List<object> GetDropPoints(String boarding, String destination, String date)
        {
            boarding = "Chennai";
            destination = "Coimbatore";
            date = "28-03-2024";

            busId = 1;
            var res = manager.FetchColumn(Route.TableName, Route.DropPoints, $"{Route.BusId} = {busId} and {Route.Boarding} = '{boarding}'").Value;


            dynamic jsonObject = JsonConvert.DeserializeObject(res[0].ToString());

            // Extracting only values
            List<object> dropPoints = new List<object>();
            ExtractValues(jsonObject, dropPoints);

            return dropPoints;

        }

        public static List<String> GetTravel(String boarding, String destination, String date)
        {
            var res = manager.FetchColumn(Route.TableName,Route.BusId ,$"{Route.Boarding} = '{boarding}' and {Route.Destination} = '{destination}' and {Route.StartDate} = '{date}'").Value;

            List<String> travels = new List<String>();

            for(int i = 0; i < res.Count; i++)
            {
                travels.Add(manager.FetchColumn(Bus.TableName, Bus.Name, $"{Bus.Id} = {res[i]} ").Value[0].ToString());
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
