using CrossCutting;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementations
{
    public class RepositoryAuthor : IRepositoryAuthor
    {
        ConnectionDB connection = new ConnectionDB();

        public void CreateAuthor(string name)
        {
            string query = "INSERT INTO Autores (Nombre) VALUES (@nombre)";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@nombre", name);

                command.ExecuteNonQuery();
            }
        }
        public List<Author> ReadAuthor()
        {
            List<Author> listAuthors = new List<Author>();

            var query = "SELECT * FROM Autores";

            using (SqlConnection connec = connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connec))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow fila in dt.Rows)
                        {
                            listAuthors.Add(new Author(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString()));
                        }
                        return listAuthors;
                    }
                }
            }
        }
        public void UpdateAuthor(int id, string name)
        {
            string query = "UPDATE Autores SET Nombre = @nombre WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);

                command.ExecuteNonQuery();
            }
        }
        public void DeleteAuthor(int id)
        {
            string query = "DELETE FROM Autores WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }
        public Author GetAuthor(int id)
        {
            var allAuthor = ReadAuthor();
            var author = allAuthor.Where(x => x.Id == id).First();

            return author;
        }
    }
}
