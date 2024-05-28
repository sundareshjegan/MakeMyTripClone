﻿
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GoLibrary;
using DatabaseLibrary;
using Newtonsoft.Json;     


namespace MakeMyTripClone
{
    static class DBManager
    {
        private static readonly string server = "192.168.3.63";
        private static readonly string database = "makemytrip";
        private static readonly string user = "team7";
        private static readonly string password = "team7team7";

        private static readonly string connectionString = $"server={server};user={user};password={password};database={database}";

        public static bool IsUserLoggedIn = false;
        public static CustomerDetails CurrentUser;
        public static event EventHandler<bool> OnUserLoggedIn;

        public static List<RouteDetails> list;

        public static MySqlConnection Connection = null;
        public static DatabaseManager manager = new MySqlHandler(server, user, password, database);

        public static void GetConnection()
        {
            manager.Connect();
        }

        #region SignUpUser

        public static Boolean IsUserExisted(String email)
        {
            // Customer -  class contains table name and column names
            var res = manager.FetchData(Customer.TableName, $"{Customer.Email}= '{email}' ", -1,-1,"",Customer.Email);
            if(res) return res.Value.Count > 0;
            return false;

        }
        public static BooleanMsg AddUser(CustomerDetails cd)
        {
            var res = manager.InsertData(Customer.TableName, 0, cd.Name, cd.Email, cd.Phone, cd.Password, cd.Gender);
            return res;
        }

        #endregion

        #region Signin

        public static String Verify(String email, String password)
        {
            if (!IsUserExisted(email))
                return "You dont have an account";

            var res = manager.FetchColumn(Customer.TableName, Customer.Password, $"{Customer.Email}= '{email}' ").Value;

            string dbPassword = res[0].ToString();

            if (dbPassword.Equals(password))
                return "";
            else
                return "Password does not match";

        }

        public static void SetCurrentUser(string email)
        {
            var res = manager.FetchData(Customer.TableName, $"{Customer.Email}= '{email}' ");
            if (res.Value != null && res.Value.Count > 0)
            {
                CurrentUser = new CustomerDetails()
                {
                    Id = int.Parse(res.Value[Customer.Id][0].ToString()),
                    Name = res.Value[Customer.Name][0].ToString(),
                    Email = res.Value[Customer.Email][0].ToString(),
                    Phone = long.Parse(res.Value[Customer.Phone][0].ToString()),
                    Password = res.Value[Customer.Password][0].ToString(),
                    Gender = res.Value[Customer.Gender][0].ToString()[0],
                };
                IsUserLoggedIn = true;
                OnUserLoggedIn ?.Invoke(CurrentUser,true);
            }
        }

        #endregion

        public static void UpdatePassword(string email, string newPassword)
        {
            ParameterData[] data = new ParameterData[]
            {
                new ParameterData(Customer.Password,newPassword),
            };
            var res = manager.UpdateData(Customer.TableName, $"{Customer.Email} = '{email}'", data);
        }
        

        public static List<RouteDetails> GetBuses(String boarding , String destination , String date)
        {
            var res = manager.FetchData(Route.TableName, $"{Route.Boarding} = '{boarding}' and {Route.Destination} = '{destination}' and {Route.StartDate} = '{date}'").Value;

            list= new List<RouteDetails>();

            // to avoid null access and to check server is reachable or not
            if (res == null)
                return null;
           
            if (res.Count > 0 )
            {
                int size = res[Route.Id].Count;
                for (int i = 0; i < size; i++)
                {
                    RouteDetails rd = new RouteDetails
                    {
                        BusId = Convert.ToInt32(manager.FetchColumn(Bus.TableName, Bus.Id, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0]),
                        RouteId = int.Parse(res[Route.Id][i].ToString()),
                        BusName = manager.FetchColumn(Bus.TableName, Bus.Name, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString(),
                        BusType = manager.FetchColumn(Bus.TableName, Bus.Type, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString(),
                        StartTime = res[Route.StartTime][i].ToString(),
                        StartDate = res[Route.StartDate][i].ToString(),
                        EndTime = res[Route.EndTime][i].ToString(),
                        EndDate = res[Route.EndDate][i].ToString(),
                        Source = res[Route.Boarding][i].ToString(),
                        Destination = res[Route.Destination][i].ToString(),
                        Price = manager.FetchColumn(Bus.TableName, Bus.Prices, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString(),
                        NoOfSeats = manager.FetchColumn(Bus.TableName, Bus.NoOfSeats, $"{Bus.Id} = '{res[Route.BusId][i].ToString()}'").Value[0].ToString(),
                        Duration = res[Route.Duration][i].ToString()
                    };
                    DateTime now = DateTime.Now;
                    DateTime bustime = Convert.ToDateTime(rd.StartTime);
                    list.Add(rd);
                }
            }
            return list;
            
        }

        public static List<object> GetBoardingPoints(String boarding, String destination, String date)
        {
           
            var res = manager.FetchColumn(Route.TableName, Route.BoardingPoints, $"{Route.Destination} = '{destination}' and {Route.Boarding} = '{boarding}' and {Route.StartDate} = '{date}'").Value;
            List<object> boardingPoints = new List<object>();
            HashSet<object> board = new HashSet<object>();

            
            if (res.Count > 0)
            {
                for (int i = 0; i < res.Count; i++)
                {
                    dynamic jsonObject = JsonConvert.DeserializeObject(res[i].ToString());
                    // Extracting only values
                    ExtractValues(jsonObject, boardingPoints);
                    foreach(var item in boardingPoints)
                    {
                        string s = item.ToString() ;
                        string[] ss = s.Split('&');
                        board.Add(ss[1]);
                    }
                    boardingPoints.Clear();
                }
            }

            foreach(var item in board)
            {
                boardingPoints.Add(item);
            }
            return boardingPoints;
        }

        public static List<object> GetBoarding(String boarding, String destination, String date,int rootid)
        {
            var res = manager.FetchColumn(Route.TableName, Route.BoardingPoints, $"{Route.Destination} = '{destination}' and {Route.Boarding} = '{boarding}' and {Route.StartDate} = '{date}' and {Route.Id}='{rootid}'").Value;
            List<object> boardingPoints = new List<object>();
            if (res.Count > 0)
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(res[0].ToString());
                // Extracting only values
                ExtractValues(jsonObject, boardingPoints);
            }
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
            List<object> dropPoints = new List<object>();
            HashSet<object> drop = new HashSet<object>();
            if(res.Count > 0)
            {
                for (int i = 0; i < res.Count; i++)
                {
                    dynamic jsonObject = JsonConvert.DeserializeObject(res[i].ToString());
                    ExtractValues(jsonObject, dropPoints);
                    foreach (var item in dropPoints)
                    {
                        string s = item.ToString();
                        string[] ss = s.Split('&');
                        drop.Add(ss[1]);
                    }
                    dropPoints.Clear();
                }
                foreach (var item in drop)
                {
                    dropPoints.Add(item);
                }
            }
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

        public static BooleanMsg ChangeSeatBookingState(SeatDetails seat)
        {
            int isBooked = seat.IsBooked ? 1 : 0;
            var res = manager.InsertData(Seat.TableName, 0, seat.RouteId, seat.SeatType, isBooked, seat.Price, seat.CId,seat.SeatNumber.Trim(),seat.IsBookedByfemale);
            return res;
        }

        public static bool IsSeatBooked(int routeId, string seatNumber)
        {
            var res = manager.FetchData(Seat.TableName, $"{Route.Id}= '{routeId}' and {Seat.SeatNumber}= '{seatNumber}' ", -1, -1, "", Seat.IsBooked).Value;
            int seatStatus = 0;
            if (res.Count > 0 && res.ContainsKey(Seat.IsBooked))
            {
                seatStatus = (Convert.ToInt32(res[Seat.IsBooked][0]));
            }
            return seatStatus == 1;

        }

        public static bool IsSeatBookedByFemale(int routeId, string seatNumber)
        {
            var res = manager.FetchData(Seat.TableName, $"{Route.Id}= '{routeId}' and {Seat.SeatNumber}= '{seatNumber}' ", -1, -1, "", Seat.IsBookedByFemale).Value;
            int seatStatus = 0;
            if (res.Count > 0 && res.ContainsKey(Seat.IsBookedByFemale))
            {
                seatStatus = (Convert.ToInt32(res[Seat.IsBookedByFemale][0]));
            }
            return seatStatus == 1;
        }

        public static string DriverPhno(int busId)
        {
            var res = manager.FetchData(Bus.TableName, $"{Bus.Id}='{busId}'").Value;
            if (res.Count > 0)
            {
                int n = (Convert.ToInt32(res["d_id"][0]));
                var res2 = manager.FetchData(Driver.TableName, $"{Driver.DriverId}='{n}'").Value;
                if (res2.Count != 0)
                {
                    string phno = res2[Driver.DriverPhonenumber][0] + "";
                    return phno;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public static void DeleteDatasfromDatabase()
        {
            for (int i = -10; i < 0; i++)
            {
                DateTime date = DateTime.Today;
                DateTime previousDate = date.AddDays(i);
                string previous = previousDate.ToString("yyyy-MM-dd");
                var res = manager.FetchData(Route.TableName, $"{Route.StartDate} = '{previous}'").Value;
                List<int> li = new List<int>();
                if (res != null)
                {
                    for (int j = 0; j < res.Count; j++)
                    {
                        li.Add(int.Parse(res[Route.Id][j].ToString()));
                    }
                    if (li.Count != 0)
                    {
                        for (int k = 0; k < li.Count; k++)
                        {
                            manager.DeleteData(Seat.TableName, $"{Seat.RouteId}='{li[k]}'");
                        }
                    }
                    manager.DeleteData(Route.TableName, $"{Route.StartDate} = '{previousDate}'");
                }
            }
        }
    }
}
