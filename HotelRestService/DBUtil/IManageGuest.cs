using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageGuest
    {
        List<Guest> GetAllGuest();
        
        Guest GetGuestFromID(int guestNo);

        bool CreateGuest(Guest guest);
        
        bool UpdateGuest(Guest guest, int guestNo);
        
        void DeleteGuest(int guestNo);
    }
}
