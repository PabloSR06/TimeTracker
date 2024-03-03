using MySqlConnector;
using timeTrackerApi.Models.Project;
using System.IdentityModel.Tokens.Jwt;
using timeTrackerApi.Services.Interfaces;
using System.Data;
using timeTrackerApi.Models.User;
using timeTrackerApi.Data.Interfaces;

namespace timeTrackerApi.Data.Repositories
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

        

        public BasicUserModel? GetById(int id)
        {
            BasicUserModel? user = default;

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
                            user = ReadBasicUserFromReader(reader);
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return user;
        }

        public UserProfileModel? GetUserLogIn(UserCredentialsModel input)
        {
            UserProfileModel profile = default;

            string base64Password = EncodePasswordToBase64(input.Password);

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("UserLogIn", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@inputEmail", input.Email);
                    command.Parameters.AddWithValue("@inputPassword", base64Password);


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
            string base64Password = EncodePasswordToBase64(user.Password);

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "INSERT INTO " + Constants.Tables.Users + " (Name, Email, Password) VALUES (@Name, @Email, @Password)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", base64Password);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        public bool Delete(int id)
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

        public BasicUserModel? GetUserWithEmail(string email)
        {
            BasicUserModel? user = default;

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                string query = "SELECT name, email FROM " + Constants.Tables.Users + " WHERE Email = @Email";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = ReadBasicUserFromReader(reader);
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

            BasicUserModel? userProfile = GetUserWithEmail(email);

            if (userProfile == null)
                return false;

            ////TODO: Add name to user profile

            JwtSecurityToken token = _tokenService.GenerateGuestToken(userProfile);
            _mailService.SendForgotMail(userProfile.Email, userProfile.Name, token);


            return true;
        }

        public bool UpdatePassword(ResetPasswordModel userCredential, int userId)
        {
            int affectedRows = 0;

            string base64Password = EncodePasswordToBase64(userCredential.Password);

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("ResetPassword", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", userId);
                    command.Parameters.AddWithValue("@password", base64Password);

                    affectedRows = command.ExecuteNonQuery();
                }
                connection.Dispose();
            }

            return affectedRows > 0;
        }

        static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        string DecodeFrom64(string encodedData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(encodedData);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                return new String(decoded_char);
            }
            catch (FormatException ex)
            {
                throw new Exception("Input is not a valid Base64 string: " + ex.Message);
            }
        }
        private UserModel ReadUserFromReader(MySqlDataReader reader)
        {
            UserModel user = new UserModel();

            if (!reader.IsDBNull(nameof(UserModel.Id)))
                user.Id = reader.GetInt32(nameof(UserModel.Id));
            if (!reader.IsDBNull(nameof(UserModel.Name)))
                user.Name = reader.GetString(nameof(UserModel.Name));
            if (!reader.IsDBNull(nameof(UserModel.Email)))
                user.Email = reader.GetString(nameof(UserModel.Email));

            return user;

        }

        private BasicUserModel ReadBasicUserFromReader(MySqlDataReader reader)
        {
            BasicUserModel user = new BasicUserModel();

            if (!reader.IsDBNull(nameof(BasicUserModel.Name)))
                user.Name = reader.GetString(nameof(BasicUserModel.Name));
            if (!reader.IsDBNull(nameof(BasicUserModel.Email)))
                user.Email = reader.GetString(nameof(BasicUserModel.Email));

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
