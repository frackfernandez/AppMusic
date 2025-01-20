using CrossCutting;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Infrastructure.Implementations
{
    public class RepositoryPlaylist : IRepositoryPlaylist
    {        
        private readonly SqlConnection _connection;
        private readonly RepositoryWeather _repWeather;
        private readonly RepositorySong _repSong;

        public RepositoryPlaylist()
        {
            _connection = ConnectionDB.GetInstance().GetConnection();
            _repWeather = new RepositoryWeather();
            _repSong = new RepositorySong();
        }

        public void CreatePlaylist(string name, Weather weather, string totalDuration, List<Song> songs)
        {
            int id = 0;

            string query = "INSERT INTO Playlist (Nombre, Clima, DuracionTotal) VALUES (@nombre, @clima, @duracionTotal); SELECT SCOPE_IDENTITY();";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@clima", weather.Id);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);

                object result = command.ExecuteScalar();
                id = Convert.ToInt32(result);
            }

            string querysongs = "INSERT INTO PlaylistCanciones (IdPlaylist, IdCancion) VALUES (@idPlaylist, @idCancion);";

            foreach (Song song in songs)
            {
                using (SqlCommand command = new SqlCommand(querysongs, _connection))
                {
                    command.Parameters.AddWithValue("@idPlaylist", id);
                    command.Parameters.AddWithValue("@idCancion", song.Id);
                    command.ExecuteNonQuery();
                }
            }
            _connection.Close();
        }
        public List<Playlist> ReadPlaylist() // mucho consumo
        {
            List<Playlist> listPlaylists = new List<Playlist>();

            var query = "SELECT * FROM Playlist";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        List<Song> listSongsEntity = new List<Song>();

                        listSongsEntity = _repSong.GetSongsPlayList(Convert.ToInt32(fila["Id"]));
                        var weatherEntity = _repWeather.GetWeather(Convert.ToInt32(fila["Clima"]));

                        listPlaylists.Add(new Playlist(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), weatherEntity, fila["DuracionTotal"].ToString(), listSongsEntity));
                    }
                }
            }
            _connection.Close();

            return listPlaylists;
        }

        public List<string> ReadNamePlaylists()
        {
            List<string> listPlaylists = new List<string>();

            var query = "SELECT Nombre FROM Playlist";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {   
                        listPlaylists.Add(fila["Nombre"].ToString());
                    }
                }
            }
            _connection.Close();

            return listPlaylists;
        }

        public void UpdatePlaylist(int id, string name, Weather weather, string totalDuration, List<Song> songs)
        {
            _connection.Open();
            string querypre = "DELETE FROM PlaylistCanciones WHERE idPlaylist = @idPlaylist";

            using (SqlCommand command = new SqlCommand(querypre, _connection))
            {
                command.Parameters.AddWithValue("@idPlaylist", id);

                command.ExecuteNonQuery();
            }

            string querysongs = "INSERT INTO PlaylistCanciones (IdPlaylist, IdCancion) VALUES (@idPlaylist, @idCancion);";

            foreach (Song song in songs)
            {
                using (SqlCommand command = new SqlCommand(querysongs, _connection))
                {
                    command.Parameters.AddWithValue("@idPlaylist", id);
                    command.Parameters.AddWithValue("@idCancion", song.Id);

                    command.ExecuteNonQuery();
                }
            }

            string query = "UPDATE Playlist SET Nombre = @nombre, Clima = @clima, DuracionTotal = @duracionTotal WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);
                command.Parameters.AddWithValue("@clima", weather.Id);

                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
        public void DeletePlaylist(int id)
        {
            _connection.Open();
            string query = "DELETE FROM PlaylistCanciones WHERE idPlaylist = @idPlaylist";

            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@idPlaylist", id);

                command.ExecuteNonQuery();
            }

            string query2 = "DELETE FROM Playlist WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query2, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            _connection.Close();
        }                             

        public Playlist GetPlaylist(int id)
        {
            Playlist playlist = null;
            string query = "SELECT * FROM Playlist WHERE Id=@id";

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
                        List<Song> listSongs = new List<Song>();

                        listSongs = _repSong.GetSongsPlayList(Convert.ToInt32(fila["Id"]));
                        var weatherEntity = _repWeather.GetWeather(Convert.ToInt32(fila["Clima"]));

                        playlist = new Playlist(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(),weatherEntity, fila["DuracionTotal"].ToString(), listSongs);
                    }
                }
            }
            _connection.Close();

            return playlist;
        }
    }
}
