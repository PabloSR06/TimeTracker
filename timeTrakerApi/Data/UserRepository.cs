using timeTrakerApi.Data.Interface;
using MySqlConnector;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly MySqlDataSource _database;
        private readonly string table = "users";


        public UserRepository(MySqlDataSource database)
        {
            _database = database;
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
                            users.Add(ReadProjectFromReader(reader));
                           
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
        private UserModel ReadUserFromReader(MySqlDataReader reader)
        {

            UserModel user = new UserModel();

                if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserModel.Id))))
                    user.Id = reader.GetInt32(reader.GetOrdinal("id"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserModel.Name))))
                    user.Name = reader.GetString(reader.GetOrdinal("name"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserModel.Email))))
                    user.Email = reader.GetString(reader.GetOrdinal("email"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserModel.CreateOnDate))))
                    user.CreateOnDate = reader.GetDateTime(reader.GetOrdinal("createondate"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(UserModel.LastModifiedOnDate))))
                    user.LastModifiedOnDate = reader.GetDateTime(reader.GetOrdinal("lastmodifiedondate"));


                return user;
            
        }
    }
}
