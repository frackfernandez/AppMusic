using CrossCutting;
using System.Collections.Generic;

namespace Infrastructure.Interfaces
{
    public interface IRepositoryAuthor
    {
        List<Author> ReadAuthor();
        void CreateAuthor(string name);
        void UpdateAuthor(int id, string name);
        void DeleteAuthor(int id);
        Author GetAuthor(int id);
        Author GetAuthor(string name);
    }
}
