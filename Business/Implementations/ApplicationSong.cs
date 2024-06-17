using Business.Interfaces;
using CrossCutting;
using CrossCutting.Enums;
using Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class ApplicationSong : IApplicationSong
    {
        RepositorySong repSong = new RepositorySong();

        public void CreateSong(string name, Category category, Author author, string totalDuration)
        {
            repSong.CreateSong(name, category, author, totalDuration);
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
        public void UpdateSong(int id, string name, Category category, Author author, string totalDuration)
        {
            repSong.UpdateSong(id, name, category, author, totalDuration);
        }

        public List<Song> GetListSongs(int id) 
        {
            var list = repSong.GetListSongs(id);
            return list;
        }
    }
}
