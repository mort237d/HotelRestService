﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageFacility : IManageFacility
    {
        public List<Facility> GetAllFacilities()
        {
            List<Facility> facilities = new List<Facility>();
            string queryString = "SELECT * FROM Facility";
            
            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Facility facility = new Facility();
                        facility.Hotel_No = reader.GetInt32(0);
                        facility.Bar = reader.GetString(1).First();
                        facility.TableTennis = reader.GetString(2).First();
                        facility.PoolTable = reader.GetString(3).First();
                        facility.Restaurant = reader.GetString(4).First();
                        facility.SwimmingPool = reader.GetString(5).First();

                        facilities.Add(facility);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }

            return facilities;
        }
        public Facility GetFacilityFromID(int hotelNo)
        {
            string queryString = $"SELECT * FROM Facility WHERE Hotel_No = {hotelNo}";
            Facility facility = new Facility();

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        facility.Hotel_No = reader.GetInt32(0);
                        facility.Bar = reader.GetString(1).First();
                        facility.TableTennis = reader.GetString(2).First();
                        facility.PoolTable = reader.GetString(3).First();
                        facility.Restaurant = reader.GetString(4).First();
                        facility.SwimmingPool = reader.GetString(5).First();
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }

                return facility;
            }
        }


        public bool CreateFacility(Facility facility)
        {
            string queryString = "INSERT INTO Facility (Hotel_No, Swimming_Pool, Bar, Table_Tennis, Pool_Table, Restaurant) VALUES (@Number, @SwimmingPool , @Bar, @TableTennis, @PoolTable, @Restaurant)";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);

                command.Parameters.AddWithValue("@Number", facility.Hotel_No);
                command.Parameters.AddWithValue("@Bar", facility.Bar);
                command.Parameters.AddWithValue("@TableTennis", facility.TableTennis);
                command.Parameters.AddWithValue("@PoolTable", facility.PoolTable);
                command.Parameters.AddWithValue("@Restaurant", facility.Restaurant);
                command.Parameters.AddWithValue("@SwimmingPool", facility.SwimmingPool);

                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public bool UpdateFacility(Facility facility, int hotelNo)
        {
            string queryString = $"UPDATE Facility SET Hotel_No = @Number, Swimming_Pool = @SwimmingPool, Bar = @Bar, Table_Tennis = @TableTennis, Pool_Table = @PoolTable, Restaurant = @Restaurant  WHERE Hotel_No = {hotelNo}";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                
                command.Parameters.AddWithValue("@Number", facility.Hotel_No);
                command.Parameters.AddWithValue("@Bar", facility.Bar);
                command.Parameters.AddWithValue("@TableTennis", facility.TableTennis);
                command.Parameters.AddWithValue("@PoolTable", facility.PoolTable);
                command.Parameters.AddWithValue("@Restaurant", facility.Restaurant);
                command.Parameters.AddWithValue("@SwimmingPool", facility.SwimmingPool);


                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        public void DeleteFacility(int hotelNo)
        {
            string queryString = $"DELETE FROM Facility WHERE Hotel_No = {hotelNo}";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                try
                {
                    command.ExecuteNonQuery();
                }
                finally
                {
                    command.Connection.Close();
                }

            }
        }
    }
}