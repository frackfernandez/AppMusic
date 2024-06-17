using Business.Interfaces;
using CrossCutting;
using Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class ApplicationPlaylist : IApplicationPlaylist
    {
        RepositoryPlaylist repPlaylist = new RepositoryPlaylist();

        public void CreatePlaylist(string name, Weather weather, List<Song> songs)
        {
            repPlaylist.CreatePlaylist(name, weather, songs);
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
        public void UpdatePlaylist(int id, string name, Weather weather, List<Song> songs)
        {
            repPlaylist.UpdatePlaylist(id, name, weather, songs);
        }
    }
}
