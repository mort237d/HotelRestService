using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageGuest : IManageGuest
    {
        public List<Guest> GetAllGuest()
        {
            List<Guest> guests = new List<Guest>();
            string queryString = "SELECT * FROM Guest";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        Guest guest = new Guest();
                        guest.Guest_No = reader.GetInt32(0); 
                        guest.Name = reader.GetString(1); 
                        guest.Address = reader.GetString(2);

                        guests.Add(guest);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }

            return guests;
        }

        public Guest GetGuestFromID(int guestNo)
        {
            string queryString = $"SELECT * FROM Guest WHERE Guest_No = {guestNo}";
            Guest guest = new Guest();

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        guest.Guest_No = reader.GetInt32(0);
                        guest.Name = reader.GetString(1);
                        guest.Address = reader.GetString(2);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }

            return guest;
        }

        public bool CreateGuest(Guest guest)
        {
            string queryString = "INSERT INTO Guest (Guest_No, Name, Address) VALUES ( @Number , @Name, @Address)";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);

                command.Parameters.AddWithValue("@Number", guest.Guest_No);
                command.Parameters.AddWithValue("@Name", guest.Name);
                command.Parameters.AddWithValue("@Address", guest.Address);

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

        public bool UpdateGuest(Guest guest, int guestNo)
        {
            string queryString = $"UPDATE Guest SET Guest_No = @Number, Name = @Name, Address = @Address WHERE Guest_No = {guestNo}";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                
                command.Parameters.AddWithValue("@Number", guest.Guest_No);
                command.Parameters.AddWithValue("@Name", guest.Name);
                command.Parameters.AddWithValue("@Address", guest.Address);

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

        public void DeleteGuest(int guestNo)
        {
            string queryString = $"DELETE FROM Guest WHERE Guest_No = {guestNo}";

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