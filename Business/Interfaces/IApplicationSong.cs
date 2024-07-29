using CrossCutting.Enums;
using CrossCutting;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IApplicationSong
    {
        List<Song> ReadSong();
        void CreateSong(string name, Category category, Author author, string album, string totalDuration);
        void UpdateSong(int id, string name, Category category, Author author, string album, string totalDuration);
        void DeleteSong(int id);
        Song GetSong(int id);
        List<Song> GetListSongs(int id);
    }
}
