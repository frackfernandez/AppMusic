using CrossCutting.DTO;
using CrossCutting.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    internal interface IApplicationUser
    {
        void CreateUser(string name, UserType type, string password);
        List<User> ReadUser();
        void UpdateUser(int id, string name, UserType type, string password);
        void DeleteUser(int id);
        User GetUser(int id);
    }
}
