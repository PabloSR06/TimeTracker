using timeTrakerApi.Data.Interface;
using MySqlConnector;
using timeTrakerApi.Models.Project;
using System.IdentityModel.Tokens.Jwt;
using timeTrakerApi.Services.Interfaces;
using System.Data;
using timeTrakerApi.Models.User;

namespace timeTrakerApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlDataSource _database;
        private readonly IMailService _mailService;
        private readonly ITokenService _tokenService;


        public UserRepository(MySqlDataSource database, IMailService mailService, ITokenService tokenService)
        {
            _database = database;
            _mailService = mailService;
            _tokenService = tokenService;
        }

        public List<UserModel> Get()
        {

            List<UserModel> users = new List<UserModel>();
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "SELECT * FROM " + Constants.Tables.Users;
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(ReadUserFromReader(reader));

                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return users;
        }

        public UserModel GetById(string id)
        {
            UserModel user = new UserModel();

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                string query = "SELECT * FROM " + Constants.Tables.Users + " WHERE Id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = ReadUserFromReader(reader);
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return user;
        }

        public UserProfileModel GetUserLogIn(UserCredentialsModel input)
        {
            UserProfileModel profile = new UserProfileModel();

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("UserLogIn", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@inputEmail", input.Email);
                    command.Parameters.AddWithValue("@inputPassword", input.Password);


                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            profile = ReadProfileFromReader(reader);
                        }
                        reader.Close();
                    }
                }
                connection.Dispose();
            }
            return profile;
        }

        public bool Insert(UserModel user)
        {
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "INSERT INTO " + Constants.Tables.Users + " (Name, Email, Password) VALUES (@Name, @Email, @Password)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        public bool Delete(string id)
        {
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "DELETE FROM " + Constants.Tables.Users + " WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }

        public UserModel? GetUserWithEmail(string email)
        {
            UserModel? user = default;

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                string query = "SELECT * FROM " + Constants.Tables.Users + " WHERE Email = @Email";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = ReadUserFromReader(reader);
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return user;
        }
        public bool ForgotPassword(string email)
        {

            UserModel? userProfile = GetUserWithEmail(email);

            if (userProfile == null)
                return false;

            ////TODO: Add name to user profile

            JwtSecurityToken token = _tokenService.GenerateGuestToken(userProfile);
            _mailService.SendForgotMail(userProfile.Email, userProfile.Name, token);


            return true;
        }

        public bool ResetPassword(UserCredentialsModel userCredential, string userId)
        {
            int affectedRows = 0;

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("ResetPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", userId);
                    command.Parameters.AddWithValue("@password", userCredential.Password);

                    affectedRows = command.ExecuteNonQuery();
                }
                connection.Dispose();
            }

            return affectedRows > 0;
        }

        private UserModel ReadUserFromReader(MySqlDataReader reader)
        {

            UserModel user = new UserModel();

            if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserModel.Id))))
                user.Id = reader.GetInt32(reader.GetOrdinal("id"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserModel.Name))))
                user.Name = reader.GetString(reader.GetOrdinal("name"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserModel.Email))))
                user.Email = reader.GetString(reader.GetOrdinal("email"));

            return user;

        }
        private UserProfileModel ReadProfileFromReader(MySqlDataReader reader)
        {

            UserProfileModel profile = new UserProfileModel();

            if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserProfileModel.Id))))
                profile.Id = reader.GetInt32(nameof(UserProfileModel.Id));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserProfileModel.Name))))
                profile.Name = reader.GetString(nameof(UserProfileModel.Name));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserProfileModel.Email))))
                profile.Email = reader.GetString(nameof(UserProfileModel.Email));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserProfileModel.Roles))))
                profile.Roles = reader.GetString(nameof(UserProfileModel.Roles)).Split(",").ToList();

            return profile;

        }
    }
}
