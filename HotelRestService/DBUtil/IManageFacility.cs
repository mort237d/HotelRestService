using System.Collections.Generic;
using HotelModel;

namespace HotelRestService.DBUtil
{
    interface IManageFacility
    {
        List<Facility> GetAllFacilities();


        Facility GetFacilityFromID(int hotelNo);


        bool CreateFacility(Facility facility);


        bool UpdateFacility(Facility facility, int hotelNo);


        void DeleteFacility(int hotelNo);
    }
}
