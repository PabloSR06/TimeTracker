using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
using TimeTrackerApi.Models;
using TimeTrackerApi.Services;

namespace TimeTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private MySqlService _mySqlService;



        public UserController(ILogger<UserController> logger, MySqlService mySqlService)
        {
            _logger = logger;
            _mySqlService = mySqlService;
        }

        private UserModel GetUser(string email)
        {
            try
            {
                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name, email, password FROM user WHERE email = @email;";

                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

                _mySqlService.TryOpen();

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

                _mySqlService.CloseConnection();

                return user;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in UserService GetUser " + ex.Message);
            }
        }

        

        private bool CheckPassword(UserModelInput input)
        {
            string hashedPassword = HashPassword(input.Password);
            UserModel UserInfo = GetUser(input.Email);

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
        
        [HttpGet("GetAllUsers")]

        public Dictionary<int, UserMin> GetAllUsers()
        {

            try
            {
                Dictionary<int, UserMin> users = new Dictionary<int, UserMin>();
                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name, email FROM user;";

                _mySqlService.TryOpen();

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

                _mySqlService.CloseConnection();

                return users;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in UserService GetAllUsers " + ex.Message);
            }

        }
        
        [HttpGet("GetAllUsersList")]

        public List<UserMin> GetAllUsersList()
        {

            try
            {
                List<UserMin> users = new List<UserMin>();
                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name, email FROM user;";

                _mySqlService.TryOpen();

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

                _mySqlService.CloseConnection();

                return users;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in UserService GetAllUsersList " + ex.Message);
            }

        }
    }
}

