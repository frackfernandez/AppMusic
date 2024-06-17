using CrossCutting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    internal interface IApplicationAuthor
    {
        List<Author> ReadAuthor();
        void CreateAuthor(string name);
        void UpdateAuthor(int id, string name);
        void DeleteAuthor(int id);
        Author GetAuthor(int id);
    }
}
