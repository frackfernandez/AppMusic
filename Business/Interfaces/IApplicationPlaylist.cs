using CrossCutting;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IApplicationPlaylist
    {
        List<Playlist> ReadPlaylist();
        void CreatePlaylist(string name, Weather weather, string totalDuration,List<Song> songs);
        void UpdatePlaylist(int id, string name, Weather weather, string totalDuration, List<Song> songs);
        void DeletePlaylist(int id);
        Playlist GetPlaylist(int id);
    }
}
