using CrossCutting;
using CrossCutting.Enums;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Implementations
{
    public class RepositoryWeather : IRepositoryWeather
    {
        private readonly SqlConnection connection;

        public RepositoryWeather()
        {
            connection = ConnectionDB.GetInstance().GetConnection();
        }

        public void CreateWeather(Code code, string description)
        {
            string query = "INSERT INTO Climas (Codigo, Descripcion) VALUES (@codigo, @descripcion)";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codigo", code.ToString());
                command.Parameters.AddWithValue("@descripcion", description);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public List<Weather> ReadWeather()
        {
            List<Weather> listWeather = new List<Weather>();

            var query = "SELECT * FROM Climas";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        Enum.TryParse(fila["Codigo"].ToString(), out Code codeEnum);

                        listWeather.Add(new Weather(Convert.ToInt32(fila["Id"]), codeEnum, fila["Descripcion"].ToString()));
                    }
                }
            }
            connection.Close();

            return listWeather;
        }
        public void UpdateWeather(int id, Code code, string description)
        {
            string query = "UPDATE Climas SET Codigo = @codigo, Descripcion = @descripcion WHERE Id = @id";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@codigo", code.ToString());
                command.Parameters.AddWithValue("@descripcion", description);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void DeleteWeather(int id)
        {
            string query = "DELETE FROM Climas WHERE Id = @id";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public Weather GetWeather(int id)
        {
            Weather weather = null;
            string query = "SELECT * FROM Climas WHERE Id=@id";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        Enum.TryParse(fila["Codigo"].ToString(), out Code codeEnum);

                        weather = new Weather(Convert.ToInt32(fila["Id"]), codeEnum, fila["Descripcion"].ToString());
                    }
                }
            }
            connection.Close();

            return weather;
        }
        public Weather GetWeather(string code)
        {
            Weather weather = null;
            string query = "SELECT * FROM Climas WHERE Codigo=@codigo";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@codigo", code);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        Enum.TryParse(fila["Codigo"].ToString(), out Code codeEnum);

                        weather = new Weather(Convert.ToInt32(fila["Id"]), codeEnum, fila["Descripcion"].ToString());
                    }
                }
            }
            connection.Close();

            return weather;
        }
    }
}
