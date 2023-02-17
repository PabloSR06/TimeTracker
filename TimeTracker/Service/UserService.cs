using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Service
{
    public class UserService
    {
        private readonly MySqlService _sqlService;
        public UserModel UserInfo { get; set; }

        public UserService(MySqlService sqlService)
        {
            _sqlService = sqlService;
        }

        private UserModel GetUser(string email)
        {
            try
            {
                MySqlCommand cmd = _sqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name, email, password FROM user WHERE email = @email;";

                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                _sqlService.TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();

                UserModel user = new UserModel();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.Id = reader.GetInt32("id");
                        user.Name = reader.GetString("name");
                        user.Email = reader.GetString("email");
                        user.Password = reader.GetString("password");
                    }
                }

                _sqlService.CloseConnection();

                return user;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in UserService GetUser " + ex.Message);
            }
        }


        public bool CheckPassword(UserModelInput input)
        {
            string hashedPassword = HashPassword(input.Password);
            UserInfo = GetUser(input.Email);

            if (UserInfo != null && UserInfo.Password == hashedPassword)
            {
                return true;
            }
            return false;
        }


        private static string HashPassword(string password)
        {
            using (SHA256 sha256 = new SHA256Managed())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("x2"));
                }
                return result.ToString();
            }
        }
    }
}
