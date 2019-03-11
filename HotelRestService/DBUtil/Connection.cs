using System.Data.SqlClient;

namespace HotelRestService.DBUtil
{
    public static class Connection
    {
        private const string ConnectionString = @"";
        public static readonly SqlConnection MyConnection = new SqlConnection(ConnectionString);
    }
}