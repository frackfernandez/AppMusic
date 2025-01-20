using CrossCutting;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Implementations
{
    public class RepositoryAuthor : IRepositoryAuthor
    {
        private readonly SqlConnection _connection;

        public RepositoryAuthor()
        {
            _connection = ConnectionDB.GetInstance().GetConnection();
        }

        public void CreateAuthor(string name)
        {
            string query = "INSERT INTO Autores (Nombre) VALUES (@nombre)";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {                
                command.Parameters.AddWithValue("@nombre", name);                
                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
        public List<Author> ReadAuthor()
        {
            List<Author> listAuthors = new List<Author>();

            var query = "SELECT * FROM Autores";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {                
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        listAuthors.Add(new Author(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString()));
                    }
                }                
            }
            _connection.Close();
            return listAuthors;
        }        
        public void UpdateAuthor(int id, string name)
        {
            string query = "UPDATE Autores SET Nombre = @nombre WHERE Id = @id";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);

                command.ExecuteNonQuery();                
            }
            _connection.Close();
        }
        public void DeleteAuthor(int id)
        {
            string query = "DELETE FROM Autores WHERE Id = @id";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();                
            }
            _connection.Close();
        }

        public Author GetAuthor(int id)
        {
            Author author = null;
            string query = "SELECT * FROM Autores WHERE Id=@id";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        author = new Author(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString());
                    }
                }
            }
            _connection.Close();

            return author;
        }
        public Author GetAuthor(string name)
        {
            Author author = null;
            string query = "SELECT * FROM Autores WHERE Nombre=@nombre";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@nombre", name);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        author = new Author(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString());
                    }
                }
            }
            _connection.Close();

            return author;
        }
    }
}
