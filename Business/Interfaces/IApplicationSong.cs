using CrossCutting.Enums;
using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    internal interface IApplicationSong
    {
        List<Song> ReadSong();
        void CreateSong(string name, Category category, Author author, string totalDuration);
        void UpdateSong(int id, string name, Category category, Author author, string totalDuration);
        void DeleteSong(int id);
        Song GetSong(int id);
    }
}
