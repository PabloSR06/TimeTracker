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
            string hash = String.Empty;

            // Initialize a SHA256 hash object
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the given string
                byte[] hashValue = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert the byte array to string format
                foreach (byte b in hashValue)
                {
                    hash += $"{b:x2}";
                }
            }

            return hash;
        }

        public Dictionary<int, UserMin> GetAllUsers()
        {

            try
            {
                Dictionary<int, UserMin> users = new Dictionary<int, UserMin>();
                MySqlCommand cmd = _sqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name, email FROM user;";

                _sqlService.TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32("id"); 
                        UserMin user = new UserMin();
                        user.Id = id; 
                        user.Name = reader.GetString("name");
                        user.Email = reader.GetString("email");
                        users.Add(id, user);
                    }
                }

                _sqlService.CloseConnection();

                return users;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in UserService GetAllUsers " + ex.Message);
            }

        }
        public List<UserMin> GetAllUsersList()
        {

            try
            {
                List<UserMin> users = new List<UserMin>();
                MySqlCommand cmd = _sqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name, email FROM user;";

                _sqlService.TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserMin user = new UserMin();
                        user.Id = reader.GetInt32("id");
                        user.Name = reader.GetString("name");
                        user.Email = reader.GetString("email");
                        users.Add(user);
                    }
                }

                _sqlService.CloseConnection();

                return users;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in UserService GetAllUsersList " + ex.Message);
            }

        }
    }
}
