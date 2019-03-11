using System.Data.SqlClient;

namespace HotelRestService.DBUtil
{
    public static class Connection
    {
        private const string ConnectionString = @"Data Source=mort237d.database.windows.net;Initial Catalog=MortensDatabase;User ID=KaffeOgKage;Password=Mort237d;Connect Timeout=30;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static readonly SqlConnection MyConnection = new SqlConnection(ConnectionString);
    }
}