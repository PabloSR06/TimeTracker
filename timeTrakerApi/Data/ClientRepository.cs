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
                            clients.Add(ReadProjectFromReader(reader));
                           
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
                            client = ReadProjectFromReader(reader);
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return client;
        }
        private ClientModel ReadProjectFromReader(MySqlDataReader reader)
        {

            ClientModel client = new ClientModel();

                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.Id))))
                    client.Id = reader.GetInt32(reader.GetOrdinal("id"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.Name))))
                    client.Name = reader.GetString(reader.GetOrdinal("name"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.Description))))
                    client.Description = reader.GetString(reader.GetOrdinal("description"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.CreateOnDate))))
                    client.CreateOnDate = reader.GetDateTime(reader.GetOrdinal("createondate"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ClientModel.LastModifiedOnDate))))
                    client.LastModifiedOnDate = reader.GetDateTime(reader.GetOrdinal("lastmodifiedondate"));


                return client;
            
        }
    }
}
