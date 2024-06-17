using CrossCutting;
using CrossCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    internal interface IRepositorySong
    {
        List<Song> ReadSong();
        void CreateSong(string name, Category category, Author author, string totalDuration);
        void UpdateSong(int id, string name, Category category, Author author, string totalDuration);
        void DeleteSong(int id);
        Song GetSong(int id);
    }
}
