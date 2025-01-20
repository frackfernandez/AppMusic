using CrossCutting;
using CrossCutting.Enums;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IRepositorySong
    {
        List<Song> ReadSong();
        void CreateSong(string name, Category category, Author author, string album, string totalDuration);
        void UpdateSong(int id, string name, Category category, Author author, string album, string totalDuration);
        void DeleteSong(int id);
        Song GetSong(int id);
        List<Song> GetSongsPlayList(int id);
    }
}
