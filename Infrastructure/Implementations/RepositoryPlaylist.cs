using CrossCutting;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Implementations
{
    public class RepositoryPlaylist : IRepositoryPlaylist
    {        
        private readonly SqlConnection connection;

        private readonly RepositoryWeather repWeather;
        private readonly RepositorySong repSong;

        public RepositoryPlaylist()
        {
            connection = ConnectionDB.GetInstance().GetConnection();

            repWeather = new RepositoryWeather();
            repSong = new RepositorySong();
        }

        public void CreatePlaylist(string name, Weather weather, string totalDuration, List<Song> songs)
        {
            string query = "INSERT INTO Playlist (Nombre, Clima, DuracionTotal) VALUES (@nombre, @clima, @duracionTotal); SELECT SCOPE_IDENTITY();";
            int weatherInt = weather.Id;

            int id = 0;

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@clima", weatherInt);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);

                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    id = Convert.ToInt32(result);
                }
            }

            string querysongs = "INSERT INTO PlaylistCanciones (IdPlaylist, IdCancion) VALUES (@idPlaylist, @idCancion);";

            for (int i = 0; i < songs.Count(); i++)
            {
                using (SqlCommand command = new SqlCommand(querysongs, connection))
                {
                    command.Parameters.AddWithValue("@idPlaylist", id);
                    command.Parameters.AddWithValue("@idCancion", songs[i].Id);

                    command.ExecuteNonQuery();
                }
            }
            connection.Close();
        }
        public List<Playlist> ReadPlaylist()
        {
            List<Playlist> listPlaylists = new List<Playlist>();

            var query = "SELECT * FROM Playlist";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        List<Song> listSongs = new List<Song>();

                        int auxId = Convert.ToInt32(fila["Id"]);
                        listSongs = repSong.GetListSongs(auxId);

                        int auxIdWeather = Convert.ToInt32(fila["Clima"]);
                        var weather = repWeather.GetWeather(auxIdWeather);

                        listPlaylists.Add(new Playlist(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), weather, fila["DuracionTotal"].ToString(), listSongs));
                    }
                }
            }
            connection.Close();

            return listPlaylists;
        }
        public void UpdatePlaylist(int id, string name, Weather weather, string totalDuration, List<Song> songs)
        {
            connection.Open();
            string querypre = "DELETE FROM PlaylistCanciones WHERE idPlaylist = @idPlaylist";

            using (SqlCommand command = new SqlCommand(querypre, connection))
            {
                command.Parameters.AddWithValue("@idPlaylist", id);

                command.ExecuteNonQuery();
            }

            string querysongs = "INSERT INTO PlaylistCanciones (IdPlaylist, IdCancion) VALUES (@idPlaylist, @idCancion);";

            for (int i = 0; i < songs.Count(); i++)
            {
                int idSong = songs[i].Id;
                using (SqlCommand command = new SqlCommand(querysongs, connection))
                {
                    command.Parameters.AddWithValue("@idPlaylist", id);
                    command.Parameters.AddWithValue("@idCancion", idSong);

                    command.ExecuteNonQuery();
                }
            }

            string query = "UPDATE Playlist SET Nombre = @nombre, Clima = @clima, DuracionTotal = @duracionTotal WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@duracionTotal", totalDuration);
                command.Parameters.AddWithValue("@clima", weather.Id);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void DeletePlaylist(int id)
        {
            connection.Open();
            string query = "DELETE FROM PlaylistCanciones WHERE idPlaylist = @idPlaylist";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idPlaylist", id);

                command.ExecuteNonQuery();
            }

            string query2 = "DELETE FROM Playlist WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query2, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }                             

        public Playlist GetPlaylist(int id)
        {
            Playlist playlist = null;
            string query = "SELECT * FROM Playlist WHERE Id=@id";

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
                        List<Song> listSongs = new List<Song>();

                        int auxId = Convert.ToInt32(fila["Id"]);
                        listSongs = repSong.GetListSongs(auxId);

                        int auxIdWeather = Convert.ToInt32(fila["Clima"]);
                        var weather = repWeather.GetWeather(auxIdWeather);

                        playlist = new Playlist(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(),weather, fila["DuracionTotal"].ToString(), listSongs);
                    }
                }
            }
            connection.Close();

            return playlist;
        }
    }
}
