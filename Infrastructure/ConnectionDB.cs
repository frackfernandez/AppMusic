using System;
using System.Data.SqlClient;

namespace Infrastructure
{
    internal class ConnectionDB
    {
        string connectionString = "Data Source=.;Database=MusicPlayer;Integrated Security=True;TrustServerCertificate=True";

        public SqlConnection GetConnection()
        {
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
    }
}
