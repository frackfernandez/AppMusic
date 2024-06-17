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
        ConnectionDB connection = new ConnectionDB();
        RepositoryWeather repWeather = new RepositoryWeather();
        RepositorySong repSong = new RepositorySong();

        public void CreatePlaylist(string name, Weather weather, List<Song> songs)
        {
            using (SqlConnection connec = connection.GetConnection())
            {
                string query = "INSERT INTO Playlist (Nombre, Clima) VALUES (@nombre, @clima); SELECT SCOPE_IDENTITY();";
                int weatherInt = weather.Id;

                int id = 0;

                using (SqlCommand command = new SqlCommand(query, connec))
                {
                    command.Parameters.AddWithValue("@nombre", name);
                    command.Parameters.AddWithValue("@clima", weatherInt);

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        id = Convert.ToInt32(result);
                    }
                }

                string querysongs = "INSERT INTO PlaylistCanciones (IdPlaylist, IdCancion) VALUES (@idPlaylist, @idCancion);";

                for (int i = 0; i < songs.Count(); i++)
                {
                    using (SqlCommand command = new SqlCommand(querysongs, connec))
                    {
                        command.Parameters.AddWithValue("@idPlaylist", id);
                        command.Parameters.AddWithValue("@idCancion", songs[i].Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public List<Playlist> ReadPlaylist()
        {
            List<Playlist> listPlaylists = new List<Playlist>();

            var query = "SELECT * FROM Playlist";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
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
                        var listWeather = repWeather.ReadWeather();
                        var selectWeather = listWeather.Where(x => x.Id == auxIdWeather).First();

                        listPlaylists.Add(new Playlist(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), selectWeather, listSongs));
                    }
                    return listPlaylists;
                }
            }
        }
        public void UpdatePlaylist(int id, string name, Weather weather, List<Song> songs)
        {
            using (SqlConnection connec = connection.GetConnection())
            {
                string querypre = "DELETE FROM PlaylistCanciones WHERE idPlaylist = @idPlaylist";

                using (SqlCommand command = new SqlCommand(querypre, connec))
                {
                    command.Parameters.AddWithValue("@idPlaylist", id);

                    command.ExecuteNonQuery();
                }

                string querysongs = "INSERT INTO PlaylistCanciones (IdPlaylist, IdCancion) VALUES (@idPlaylist, @idCancion);";

                for (int i = 0; i < songs.Count(); i++)
                {
                    int idSong = songs[i].Id;
                    using (SqlCommand command = new SqlCommand(querysongs, connec))
                    {
                        command.Parameters.AddWithValue("@idPlaylist", id);
                        command.Parameters.AddWithValue("@idCancion", idSong);

                        command.ExecuteNonQuery();
                    }
                }

                string query = "UPDATE Playlist SET Nombre = @nombre, Clima = @clima WHERE Id = @id";

                using (SqlCommand command = new SqlCommand(query, connec))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@nombre", name);
                    command.Parameters.AddWithValue("@clima", weather.Id);

                    command.ExecuteNonQuery();
                }
            }
        }
        public void DeletePlaylist(int id)
        {
            using (SqlConnection connec = connection.GetConnection())
            {
                string query = "DELETE FROM PlaylistCanciones WHERE idPlaylist = @idPlaylist";

                using (SqlCommand command = new SqlCommand(query, connec))
                {
                    command.Parameters.AddWithValue("@idPlaylist", id);

                    command.ExecuteNonQuery();
                }

                string query2 = "DELETE FROM Playlist WHERE Id = @id";

                using (SqlCommand command = new SqlCommand(query2, connec))
                {
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }                             
        public Playlist GetPlaylist(int id)
        {
            var allPlaylist = ReadPlaylist();
            var playlist = allPlaylist.Where(x => x.Id == id).First();

            return playlist;
        }
    }
}
