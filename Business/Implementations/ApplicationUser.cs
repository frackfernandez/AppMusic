using Business.Interfaces;
using CrossCutting.DTO;
using CrossCutting.Enums;
using Infrastructure.Implementations;
using Infrastructure.Interfaces;
using System.Collections.Generic;

namespace Business.Implementations
{
    public class ApplicationUser : IApplicationUser
    {
        private readonly IRepositoryUser repUser;

        public ApplicationUser()
        {
            repUser = new RepositoryUser();
        }

        public void CreateUser(string name, UserType type, string password)
        {
            repUser.CreateUser(name, type, password);
        }
        public void DeleteUser(int id)
        {
            repUser.DeleteUser(id);
        }
        public User GetUser(int id)
        {
            var user = repUser.GetUser(id);
            return user;
        }

        public User GetUser(string name, string password)
        {
            return repUser.GetUser(name, password);
        }

        public List<User> ReadUser()
        {
            var list = repUser.ReadUser();
            return list;
        }
        public void UpdateUser(int id, string name, UserType type, string password)
        {
            repUser.UpdateUser(id, name, type, password);
        }
    }
}
