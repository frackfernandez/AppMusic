using CrossCutting;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Implementations
{
    public class RepositoryWeather : IRepositoryWeather
    {
        ConnectionDB connection = new ConnectionDB();

        public void CreateWeather(string code, string description)
        {
            string query = "INSERT INTO Climas (Codigo, Descripcion) VALUES (@codigo, @descripcion)";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@codigo", code);
                command.Parameters.AddWithValue("@descripcion", description);

                command.ExecuteNonQuery();
            }
        }
        public List<Weather> ReadWeather()
        {
            List<Weather> listWeather = new List<Weather>();

            var query = "SELECT * FROM Climas";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        listWeather.Add(new Weather(Convert.ToInt32(fila["Id"]), fila["Codigo"].ToString(), fila["Descripcion"].ToString()));
                    }
                    return listWeather;
                }
            }
        }
        public void UpdateWeather(int id, string code, string description)
        {
            string query = "UPDATE Climas SET Codigo = @codigo, Descripcion = @descripcion WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@codigo", code);
                command.Parameters.AddWithValue("@descripcion", description);

                command.ExecuteNonQuery();
            }
        }
        public void DeleteWeather(int id)
        {
            string query = "DELETE FROM Climas WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }
        public Weather GetWeather(int id)
        {
            var allWeather = ReadWeather();
            var weather = allWeather.Where(x => x.Id == id).First();

            return weather;
        }
    }
}
