using CrossCutting;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IRepositoryPlaylist
    {
        List<Playlist> ReadPlaylist();
        List<string> ReadNamePlaylists();
        void CreatePlaylist(string name, Weather weather, string totalDuration, List<Song> songs);
        void UpdatePlaylist(int id, string name, Weather weather, string totalDuration, List<Song> songs);
        void DeletePlaylist(int id);
        Playlist GetPlaylist(int id);
    }
}
