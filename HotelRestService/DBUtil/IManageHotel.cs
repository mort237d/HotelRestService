using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageHotel
    {
        List<Hotel> GetAllHotel();

        Hotel GetHotelFromID(int hotelNo);
        
        bool CreateHotel(Hotel hotel);
        
        bool UpdateHotel(Hotel hotel, int hotelNo);

        void DeleteHotel(int hotelNo);
    }
}
