using CrossCutting.Enums;

namespace CrossCutting
{
    public class Song
    {
        public Song(int id, string name, Category category, Author author, string album, string totalDuration)
        {
            Id = id;
            Name = name;
            Category = category;
            Author = author;
            Album = album;
            TotalDuration = totalDuration;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Author Author { get; set; }
        public string Album { get; set; }
        public string TotalDuration { get; set; }
    }
}
