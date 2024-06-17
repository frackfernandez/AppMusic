using CrossCutting.Enums;

namespace CrossCutting.DTO
{
    public class User
    {
        public User(int id, string name, UserType userType, string password)
        {
            Id = id;
            Name = name;
            UserType = userType;
            Password = password;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public UserType UserType { get; set; }
        public string Password { get; set; }
    }
}
