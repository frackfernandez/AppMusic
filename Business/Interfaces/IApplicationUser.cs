using CrossCutting.DTO;
using CrossCutting.Enums;
using System.Collections.Generic;

namespace Business.Interfaces
{
    public interface IApplicationUser
    {
        void CreateUser(string name, UserType type, string password);
        List<User> ReadUser();
        void UpdateUser(int id, string name, UserType type, string password);
        void DeleteUser(int id);
        User GetUser(int id);
        User GetUser(string name, string password);
    }
}
