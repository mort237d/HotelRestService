using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageRoom
    {
        List<Room> GetAllRooms();

        Room GetRoomFromID(int roomNo, int hotelNo);

        bool CreateRoom(Room room);

        bool UpdateRoom(Room room, int roomNo, int hotelNo);

        void DeleteRoom(int roomNo, int hotelNo);
    }
}
