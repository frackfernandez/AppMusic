using Business.Interfaces;
using CrossCutting;
using Infrastructure.Implementations;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Business.Implementations
{
    public class ApplicationAuthor : IApplicationAuthor
    {
        private readonly IRepositoryAuthor repAuthor;

        public ApplicationAuthor()
        {
            repAuthor = new RepositoryAuthor();
        }

        public void CreateAuthor(string name)
        {
            repAuthor.CreateAuthor(name);
        }
        public void DeleteAuthor(int id)
        {
            repAuthor.DeleteAuthor(id);
        }
        public Author GetAuthor(int id)
        {
            var author = repAuthor.GetAuthor(id);

            return author;
        }
        public Author GetAuthor(string name)
        {
            var author = repAuthor.GetAuthor(name);

            return author;
        }
        public List<Author> ReadAuthor()
        {
            var list = repAuthor.ReadAuthor();

            return list;
        }
        public void UpdateAuthor(int id, string name)
        {
            repAuthor.UpdateAuthor(id, name);
        }
    }
}
