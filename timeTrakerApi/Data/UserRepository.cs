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
                            user = ReadProjectFromReader(reader);
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return user;
        }
        private UserModel ReadProjectFromReader(MySqlDataReader reader)
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
