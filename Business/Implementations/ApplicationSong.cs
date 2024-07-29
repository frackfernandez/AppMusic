using Business.Interfaces;
using CrossCutting;
using CrossCutting.Enums;
using Infrastructure.Implementations;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Business.Implementations
{
    public class ApplicationSong : IApplicationSong
    {
        private readonly IRepositorySong repSong;

        public ApplicationSong()
        {
            repSong = new RepositorySong();
        }

        public void CreateSong(string name, Category category, Author author, string album, string totalDuration)
        {
            repSong.CreateSong(name, category, author, album, totalDuration);
        }
        public void DeleteSong(int id)
        {
            repSong.DeleteSong(id);
        }
        public Song GetSong(int id)
        {
            var song = repSong.GetSong(id);

            return song;
        }
        public List<Song> ReadSong()
        {
            var list = repSong.ReadSong();

            return list;
        }
        public void UpdateSong(int id, string name, Category category, Author author, string album, string totalDuration)
        {
            repSong.UpdateSong(id, name, category, author, album, totalDuration);
        }

        public List<Song> GetListSongs(int id) 
        {
            var list = repSong.GetListSongs(id);
            return list;
        }
    }
}
