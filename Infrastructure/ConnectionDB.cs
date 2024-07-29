using System;
using System.Data.SqlClient;

namespace Infrastructure
{
    public class ConnectionDB
    {
        private static readonly Lazy<ConnectionDB> instance = new Lazy<ConnectionDB>(() => new ConnectionDB());

        private ConnectionDB() { }

        public SqlConnection GetConnection()
        {
            
            string connectionString = "Data Source=.;Database=MusicPlayer;Integrated Security=True;TrustServerCertificate=True";
            
            return new SqlConnection(connectionString);
        }

        public static ConnectionDB GetInstance()
        {
            return instance.Value;
        }
    }
}
