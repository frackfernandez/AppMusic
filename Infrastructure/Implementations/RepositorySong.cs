using CrossCutting;
using CrossCutting.Enums;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Implementations
{
    public class RepositorySong : IRepositorySong
    {
        ConnectionDB connection = new ConnectionDB();
        RepositoryAuthor repAuthor = new RepositoryAuthor();

        public void CreateSong(string name, Category category, Author author, string totalDuration)
        {
            string query = "INSERT INTO Canciones (Nombre, Categoria, Autor, DuracionTotal) VALUES (@nombre, @categoria, @autor, @duracionTotal)";

            string categoryStr = category.ToString();
            int authorInt = author.Id;

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@categoria", categoryStr);
                command.Parameters.AddWithValue("@autor", authorInt);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);

                command.ExecuteNonQuery();
            }
        }
        public List<Song> ReadSong()
        {
            List<Song> listSongs = new List<Song>();

            var listAuthors = repAuthor.ReadAuthor();

            var query = "SELECT * FROM Canciones";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);                    

                    foreach (DataRow fila in dt.Rows)
                    {
                        string auxCategory = fila["Categoria"].ToString();
                        int auxIdAuthor = Convert.ToInt32(fila["Autor"]);
                        
                        Enum.TryParse(auxCategory, out Category categoryRes);
                        
                        var selectAuthor = listAuthors.Where(x => x.Id == auxIdAuthor).First();

                        listSongs.Add(new Song(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), categoryRes, selectAuthor, fila["DuracionTotal"].ToString()));
                    }
                    return listSongs;
                }
            }
        }
        public void UpdateSong(int id, string name, Category category, Author author, string totalDuration)
        {
            string query = "UPDATE Canciones SET Nombre = @nombre, Categoria = @categoria, Autor = @autor, DuracionTotal = @duracionTotal WHERE Id = @id";
            string categoryStr = category.ToString();
            int authorInt = author.Id;

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@categoria", categoryStr);
                command.Parameters.AddWithValue("@autor", authorInt);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);

                command.ExecuteNonQuery();
            }
        }
        public void DeleteSong(int id)
        {
            string query = "DELETE FROM Canciones WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }
        public Song GetSong(int id)
        {
            var allSong = ReadSong();
            var song = allSong.Where(x => x.Id == id).First();

            return song;
        }

        public List<Song> GetListSongs(int id)
        {
            var listSongs = new List<Song>();

            var query = $"SELECT * FROM PlaylistCanciones WHERE idPlaylist = {id}";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
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
                    return listSongs;
                }
            }
        }
    }
}
