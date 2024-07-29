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
        private readonly SqlConnection connection;

        public RepositoryUser()
        {
            connection = ConnectionDB.GetInstance().GetConnection();
        }

        public void CreateUser(string name, UserType type, string password)
        {
            string query = "INSERT INTO Usuarios (Nombre, Tipo, Contraseña) VALUES (@nombre,@tipo,@contraseña)";

            string typeStr = type.ToString();

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@tipo", typeStr);
                command.Parameters.AddWithValue("@contraseña", password);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public List<User> ReadUser()
        {
            List<User> listUser = new List<User>();

            var query = "SELECT * FROM Usuarios";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        string auxType = fila["Tipo"].ToString();
                        Enum.TryParse(auxType, out UserType typeRes);

                        listUser.Add(new User(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), typeRes, fila["Contraseña"].ToString()));
                    }
                }
            }
            connection.Close();
            return listUser;
        }
        public void UpdateUser(int id, string name, UserType type, string password)
        {
            string query = "UPDATE Usuarios SET Nombre = @nombre, Tipo = @tipo, Contraseña = @contraseña WHERE Id = @id";
            string typeStr = type.ToString();

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@tipo", typeStr);
                command.Parameters.AddWithValue("@contraseña", password);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }
        public void DeleteUser(int id)
        {
            string query = "DELETE FROM Usuarios WHERE Id = @id";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
            connection.Close();
        }      
        
        public User GetUser(int id)
        {         
            User user = null;
            string query = "SELECT * FROM Usuarios WHERE Id=@id";

            string idStr = id.ToString();

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idStr);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        string auxType = fila["Tipo"].ToString();
                        Enum.TryParse(auxType, out UserType typeRes);

                        user = new User(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), typeRes, fila["Contraseña"].ToString());
                    }
                }
            }
            connection.Close();

            return user;
        }
        public User GetUser(string name, string password)
        {
            User user = null;
            string query = "SELECT * FROM Usuarios WHERE Nombre=@name AND Contraseña=@password";

            connection.Open();
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@password", password);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    var dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow fila in dt.Rows)
                    {
                        string auxType = fila["Tipo"].ToString();
                        Enum.TryParse(auxType, out UserType typeRes);

                        user = new User(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(), typeRes, fila["Contraseña"].ToString());
                    }
                }
            }
            connection.Close();

            return user;
        }
    }
}
