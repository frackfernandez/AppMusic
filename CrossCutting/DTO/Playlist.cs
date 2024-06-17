using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting
{
    public class Playlist
    {
        public Playlist(int id, string name, Weather weather, List<Song> songs)
        {
            Id = id;
            Name = name;
            Weather = weather;
            Songs = songs;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Weather Weather { get; set; }
        public List<Song> Songs { get;}
    }
}
