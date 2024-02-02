using timeTrakerApi.Data.Interface;
using MySqlConnector;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data
{
    public class ClientRepository : IClientRepository
    {
        private readonly MySqlDataSource _database;


        public ClientRepository(MySqlDataSource database)
        {
            _database = database;
        }

        public List<ClientModel> Get()
        {
            List<ClientModel> clients = new List<ClientModel>();
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "SELECT * FROM " + Constants.Tables.Clients;
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            clients.Add(ReadClientFromReader(reader));
                           
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return clients;
        }

        public ClientModel GetById(string id)
        {
            ClientModel client = new ClientModel();
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                string query = "SELECT * FROM " + Constants.Tables.Clients + " WHERE Id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            client = ReadClientFromReader(reader);
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return client;
        }
        public bool Insert(ClientModel client)
        {
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "INSERT INTO " + Constants.Tables.Clients + " (Name) VALUES (@Name)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", client.Name);

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
        private ClientModel ReadClientFromReader(MySqlDataReader reader)
        {
            ClientModel client = new ClientModel();

                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.Id))))
                    client.Id = reader.GetInt32(reader.GetOrdinal("id"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.Name))))
                    client.Name = reader.GetString(reader.GetOrdinal("name"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.CreateOnDate))))
                    client.CreateOnDate = reader.GetDateTime(reader.GetOrdinal("createondate"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.LastModifiedOnDate))))
                    client.LastModifiedOnDate = reader.GetDateTime(reader.GetOrdinal("lastmodifiedondate"));


                return client;
            
        }
    }
}
