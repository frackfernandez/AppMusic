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
        private readonly SqlConnection _connection;
        private readonly RepositoryAuthor _repAuthor;

        public RepositorySong()
        {
            _connection = ConnectionDB.GetInstance().GetConnection();
            _repAuthor = new RepositoryAuthor();
        }

        public void CreateSong(string name, Category category, Author author, string album, string totalDuration)
        {
            string query = "INSERT INTO Canciones (Nombre, Categoria, Autor, Album, DuracionTotal) VALUES (@nombre, @categoria, @autor, @album, @duracionTotal)";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@categoria", category.ToString());
                command.Parameters.AddWithValue("@autor", author.Id);
                command.Parameters.AddWithValue("@album", album);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);

                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
        public List<Song> ReadSong()
        {
            List<Song> listSongs = new List<Song>();

            var query = "SELECT * FROM Canciones";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);                    

                    foreach (DataRow fila in dt.Rows)
                    {                                             
                        Enum.TryParse(fila["Categoria"].ToString(), out Category categoryEnum);
                        var authorEntity = _repAuthor.GetAuthor(Convert.ToInt32(fila["Autor"]));

                        listSongs.Add(new Song(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), categoryEnum, authorEntity, fila["Album"].ToString(), fila["DuracionTotal"].ToString()));
                    }
                }
            }
            _connection.Close();

            return listSongs;
        }
        public void UpdateSong(int id, string name, Category category, Author author, string album, string totalDuration)
        {
            string query = "UPDATE Canciones SET Nombre = @nombre, Categoria = @categoria, Autor = @autor, Album = @album, DuracionTotal = @duracionTotal WHERE Id = @id";

            int authorInt = author.Id;

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {                
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@categoria", category.ToString());
                command.Parameters.AddWithValue("@autor", authorInt);
                command.Parameters.AddWithValue("@album", album);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);

                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
        public void DeleteSong(int id)
        {
            string query = "DELETE FROM Canciones WHERE Id = @id";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public Song GetSong(int id)
        {
            Song song = null;

            string query = "SELECT * FROM Canciones WHERE Id=@id";

            if (_connection.State == ConnectionState.Closed)
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
                        Enum.TryParse(fila["Categoria"].ToString(), out Category categoryEnum);
                        var authorEntity = _repAuthor.GetAuthor(Convert.ToInt32(fila["Autor"]));

                        song = new Song(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), categoryEnum, authorEntity, fila["Album"].ToString(), fila["DuracionTotal"].ToString());
                    }
                }
            }
            _connection.Close();

            return song;
        }
        public List<Song> GetSongsPlayList(int id)
        {
            var listSongs = new List<Song>();

            var query = "SELECT * FROM PlaylistCanciones WHERE idPlaylist=@id";

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
                        var auxSong = GetSong(Convert.ToInt32(fila["IdCancion"]));

                        listSongs.Add(auxSong);
                    }
                }
            }
            _connection.Close();

            return listSongs;
        }
    }
}
