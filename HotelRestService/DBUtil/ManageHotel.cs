using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageHotel : IManageHotel
    {
        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hotels = new List<Hotel>();
            string queryString = "SELECT * FROM Hotel";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Hotel hotel = new Hotel();
                        hotel.Hotel_No = reader.GetInt32(0);
                        hotel.Name = reader.GetString(1);
                        hotel.Address = reader.GetString(2);

                        hotels.Add(hotel);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }

            return hotels;
        }

        public Hotel GetHotelFromID(int hotelNo)
        {
            string queryString = $"SELECT * FROM Hotel WHERE Hotel_No = {hotelNo}";
            Hotel hotel = new Hotel();

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        hotel.Hotel_No = reader.GetInt32(0);
                        hotel.Name = reader.GetString(1);
                        hotel.Address = reader.GetString(2);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }

            return hotel;
        }

        public bool CreateHotel(Hotel hotel)
        {
            string queryString = "INSERT INTO Hotel (Hotel_No, Name, Address) VALUES ( @Number , @Name, @Address)";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);

                command.Parameters.AddWithValue("@Number", hotel.Hotel_No);
                command.Parameters.AddWithValue("@Name", hotel.Name);
                command.Parameters.AddWithValue("@Address", hotel.Address);

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

        public bool UpdateHotel(Hotel hotel, int hotelNo)
        {
            string queryString = $"UPDATE Hotel SET Hotel_No = @Number, Name = @Name, Address = @Address WHERE Hotel_No = {hotelNo}";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                
                command.Parameters.AddWithValue("@Number", hotel.Hotel_No);
                command.Parameters.AddWithValue("@Name", hotel.Name);
                command.Parameters.AddWithValue("@Address", hotel.Address);

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

        public void DeleteHotel(int hotelNo)
        {
            string queryString = $"DELETE FROM Hotel WHERE Hotel_No = {hotelNo}";

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