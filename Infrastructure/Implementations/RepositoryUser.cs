using CrossCutting.DTO;
using CrossCutting.Enums;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Implementations
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly SqlConnection _connection;

        public RepositoryUser()
        {
            _connection = ConnectionDB.GetInstance().GetConnection();
        }

        public void CreateUser(string name, UserType type, string password)
        {
            string query = "INSERT INTO Usuarios (Nombre, Tipo, Contraseña) VALUES (@nombre,@tipo,@contraseña)";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@tipo", type.ToString());
                command.Parameters.AddWithValue("@contraseña", password);

                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
        public List<User> ReadUser()
        {
            List<User> listUser = new List<User>();

            var query = "SELECT * FROM Usuarios";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        Enum.TryParse(fila["Tipo"].ToString(), out UserType typeRes);

                        listUser.Add(new User(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), typeRes, fila["Contraseña"].ToString()));
                    }
                }
            }
            _connection.Close();
            return listUser;
        }
        public void UpdateUser(int id, string name, UserType type, string password)
        {
            string query = "UPDATE Usuarios SET Nombre = @nombre, Tipo = @tipo, Contraseña = @contraseña WHERE Id = @id";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@tipo", type.ToString());
                command.Parameters.AddWithValue("@contraseña", password);

                command.ExecuteNonQuery();
            }
            _connection.Close();
        }
        public void DeleteUser(int id)
        {
            string query = "DELETE FROM Usuarios WHERE Id = @id";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            _connection.Close();
        }      
        
        public User GetUser(int id)
        {         
            User user = null;
            string query = "SELECT * FROM Usuarios WHERE Id=@id";

            string idStr = id.ToString();

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@id", idStr);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        Enum.TryParse(fila["Tipo"].ToString(), out UserType typeEnum);

                        user = new User(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), typeEnum, fila["Contraseña"].ToString());
                    }
                }
            }
            _connection.Close();

            return user;
        }
        public User GetUser(string name, string password)
        {
            User user = null;
            string query = "SELECT * FROM Usuarios WHERE Nombre=@name AND Contraseña=@password";

            _connection.Open();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@password", password);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        Enum.TryParse(fila["Tipo"].ToString(), out UserType typeEnum);

                        user = new User(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), typeEnum, fila["Contraseña"].ToString());
                    }
                }
            }
            _connection.Close();

            return user;
        }
    }
}
