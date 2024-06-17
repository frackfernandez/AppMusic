using CrossCutting;
using CrossCutting.DTO;
using CrossCutting.Enums;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Implementations
{
    public class RepositoryUser : IRepositoryUser
    {
        ConnectionDB connection = new ConnectionDB();

        public void CreateUser(string name, UserType type, string password)
        {
            string query = "INSERT INTO Usuarios (Nombre, Tipo, Contraseña) VALUES (@nombre,@tipo,@contraseña)";

            string typeStr = type.ToString();

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@tipo", typeStr);
                command.Parameters.AddWithValue("@contraseña", password);

                command.ExecuteNonQuery();
            }
        }
        public void DeleteUser(int id)
        {
            string query = "DELETE FROM Usuarios WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@id", id);

                command.ExecuteNonQuery();
            }
        }
        public User GetUser(int id)
        {
            var list = ReadUser();
            var user = list.Where(x => x.Id == id).First();

            return user;
        }
        public List<User> ReadUser()
        {
            List<User> listUser = new List<User>();

            var query = "SELECT * FROM Usuarios";

            using (SqlConnection connec = connection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connec))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        var dt = new DataTable();
                        adapter.Fill(dt);

                        foreach (DataRow fila in dt.Rows)
                        {
                            string auxType = fila["Tipo"].ToString();
                            Enum.TryParse(auxType, out UserType typeRes);

                            listUser.Add(new User(Convert.ToInt32(fila["Id"]), fila["Nombre"].ToString(),typeRes, fila["Contraseña"].ToString()));
                        }
                        return listUser;
                    }
                }
            }
        }
        public void UpdateUser(int id, string name, UserType type, string password)
        {
            string query = "UPDATE Usuarios SET Nombre = @nombre, Tipo = @tipo, Contraseña = @contraseña WHERE Id = @id";
            string typeStr = type.ToString();

            using (SqlCommand command = new SqlCommand(query, connection.GetConnection()))
            {
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@nombre", name);
                command.Parameters.AddWithValue("@tipo", typeStr);
                command.Parameters.AddWithValue("@contraseña", password);

                command.ExecuteNonQuery();
            }
        }
    }
}
