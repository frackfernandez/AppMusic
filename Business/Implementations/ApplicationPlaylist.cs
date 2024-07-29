using Business.Interfaces;
using CrossCutting;
using Infrastructure.Implementations;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Business.Implementations
{
    public class ApplicationPlaylist : IApplicationPlaylist
    {
        private readonly IRepositoryPlaylist repPlaylist;

        public ApplicationPlaylist()
        {
            repPlaylist = new RepositoryPlaylist();
        }
        public void CreatePlaylist(string name, Weather weather, string totalDuration, List<Song> songs)
        {
            repPlaylist.CreatePlaylist(name, weather, totalDuration, songs);
        }
        public void DeletePlaylist(int id)
        {
            repPlaylist.DeletePlaylist(id);
        }
        public Playlist GetPlaylist(int id)
        {
            var playlist = repPlaylist.GetPlaylist(id);

            return playlist;
        }
        public List<Playlist> ReadPlaylist()
        {
            var list = repPlaylist.ReadPlaylist();

            return list;
        }
        public void UpdatePlaylist(int id, string name, Weather weather, string totalDuration, List<Song> songs)
        {
            repPlaylist.UpdatePlaylist(id, name, weather, totalDuration, songs);
        }
    }
}
