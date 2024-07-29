using CrossCutting;
using CrossCutting.Enums;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Implementations
{
    public class RepositorySong : IRepositorySong
    {
        private readonly SqlConnection connection;
        private readonly RepositoryAuthor repAuthor;

        public RepositorySong()
        {
            connection = ConnectionDB.GetInstance().GetConnection();

            repAuthor = new RepositoryAuthor();
        }

        public void CreateSong(string name, Category category, Author author, string album, string totalDuration)
        {
            string query = "INSERT INTO Canciones (Nombre, Categoria, Autor, Album, DuracionTotal) VALUES (@nombre, @categoria, @autor, @album, @duracionTotal)";

            string categoryStr = category.ToString();
            int authorInt = author.Id;

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@categoria", categoryStr);
                command.Parameters.AddWithValue("@autor", authorInt);
                command.Parameters.AddWithValue("@album", album);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public List<Song> ReadSong()
        {
            List<Song> listSongs = new List<Song>();

            var query = "SELECT * FROM Canciones";

            if (connection.State == 0)
                connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);                    

                    foreach (DataRow fila in dt.Rows)
                    {
                        string auxCategory = fila["Categoria"].ToString();                                                
                        Enum.TryParse(auxCategory, out Category categoryRes);

                        int idAuthor = Convert.ToInt32(fila["Autor"]);
                        var author = repAuthor.GetAuthor(idAuthor);

                        listSongs.Add(new Song(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), categoryRes, author, fila["Album"].ToString(), fila["DuracionTotal"].ToString()));
                    }
                }
            }
            connection.Close();

            return listSongs;
        }
        public void UpdateSong(int id, string name, Category category, Author author, string album, string totalDuration)
        {
            string query = "UPDATE Canciones SET Nombre = @nombre, Categoria = @categoria, Autor = @autor, Album = @album, DuracionTotal = @duracionTotal WHERE Id = @id";

            string categoryStr = category.ToString();
            int authorInt = author.Id;

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {                
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@categoria", categoryStr);
                command.Parameters.AddWithValue("@autor", authorInt);
                command.Parameters.AddWithValue("@album", album);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void DeleteSong(int id)
        {
            string query = "DELETE FROM Canciones WHERE Id = @id";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        public Song GetSong(int id)
        {
            Song song = null;

            string query = "SELECT * FROM Canciones WHERE Id=@id";

            string idStr = id.ToString();

            if (connection.State == 0)
                connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idStr);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        string auxCategory = fila["Categoria"].ToString();
                        Enum.TryParse(auxCategory, out Category categoryRes);

                        int idAuthor = Convert.ToInt32(fila["Autor"]);
                        var author = repAuthor.GetAuthor(idAuthor);

                        song = new Song(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), categoryRes, author, fila["Album"].ToString(), fila["DuracionTotal"].ToString());
                    }
                }
            }
            connection.Close();

            return song;
        }
        public List<Song> GetListSongs(int id)
        {
            var listSongs = new List<Song>();

            var query = "SELECT * FROM PlaylistCanciones WHERE idPlaylist=@id";

            string idStr = id.ToString();

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idStr);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        int aux = Convert.ToInt32(fila["IdCancion"]);

                        var auxSong = GetSong(aux);

                        listSongs.Add(auxSong);
                    }
                }
            }
            connection.Close();

            return listSongs;
        }
    }
}
