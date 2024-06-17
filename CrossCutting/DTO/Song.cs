using CrossCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting
{
    public class Song
    {
        public Song(int id, string name, Category category, Author author, string totalDuration)
        {
            Id = id;
            Name = name;
            Category = category;
            Author = author;
            TotalDuration = totalDuration;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public Author Author { get; set; }
        public string TotalDuration { get; set; }
    }
}
