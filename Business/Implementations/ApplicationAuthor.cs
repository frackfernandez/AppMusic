using Business.Interfaces;
using CrossCutting;
using Infrastructure.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations
{
    public class ApplicationAuthor : IApplicationAuthor
    {
        RepositoryAuthor repAuthor = new RepositoryAuthor();

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
