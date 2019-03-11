using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using HotelModel;

namespace HotelRestService.DBUtil
{
    public class ManageRoom : IManageRoom
    {
        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();
            string queryString = "SELECT * FROM Room";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        Room room = new Room();
                        room.Room_No = reader.GetInt32(0);
                        room.Hotel_No = reader.GetInt32(1);
                        room.Type = reader.GetString(2).First();
                        room.Price = reader.GetDouble(3);

                        rooms.Add(room);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }
            }
            return rooms;
        }

        public Room GetRoomFromID(int roomNo, int hotelNo)
        {
            string queryString = $"SELECT * FROM Room WHERE Room_No = {roomNo} AND Hotel_No = {hotelNo}";
            Room room = new Room();

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        room.Room_No = reader.GetInt32(0);
                        room.Hotel_No = reader.GetInt32(1);
                        room.Type = reader.GetString(2).First();
                        room.Price = reader.GetDouble(3);
                    }
                }
                finally
                {
                    reader.Close();
                    command.Connection.Close();
                }

                return room;
            }
        }

        public bool CreateRoom(Room room)
        {
            string queryString = "INSERT INTO Room (Room_No, Hotel_No, Types, Price) VALUES (@RoomNumber, @HotelNumber, @Type, @Price)";
            
            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);

                command.Parameters.AddWithValue("@RoomNumber", room.Room_No);
                command.Parameters.AddWithValue("@HotelNumber", room.Hotel_No);
                command.Parameters.AddWithValue("@Type", room.Type);
                command.Parameters.AddWithValue("@Price", room.Price);

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

        public bool UpdateRoom(Room room, int roomNo, int hotelNo)
        {
            string queryString = $"UPDATE Room SET Room_No = @RoomNumber, Hotel_No = @HotelNumber, Types = @Types, Price = @Price WHERE Room_No = {roomNo} AND hotel_No = {hotelNo}";

            using (Connection.MyConnection)
            {
                SqlCommand command = new SqlCommand(queryString, Connection.MyConnection);
                
                command.Parameters.AddWithValue("@RoomNumber", room.Room_No);
                command.Parameters.AddWithValue("@HotelNumber", room.Hotel_No);
                command.Parameters.AddWithValue("@Types", room.Type);
                command.Parameters.AddWithValue("@Price", room.Price);

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

        public void DeleteRoom(int roomNo, int hotelNo)
        {
            string queryString = $"DELETE FROM Room WHERE Room_No = {roomNo} AND Hotel_No = {hotelNo}";

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