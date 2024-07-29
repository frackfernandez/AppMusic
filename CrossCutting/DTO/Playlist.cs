using System.Collections.Generic;

namespace CrossCutting
{
    public class Playlist
    {
        public Playlist(int id, string name, Weather weather, string totalDuration,List<Song> songs)
        {
            Id = id;
            Name = name;
            Weather = weather;
            TotalDuration = totalDuration;
            Songs = songs;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Weather Weather { get; set; }
        public string TotalDuration { get; set; }
        public List<Song> Songs { get;}
    }
}
