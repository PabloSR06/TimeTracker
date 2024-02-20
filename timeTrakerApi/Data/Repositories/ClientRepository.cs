using MySqlConnector;
using timeTrakerApi.Data.Interfaces;
using System.Data;
using timeTrakerApi.Models.Client;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace timeTrakerApi.Data.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly MySqlDataSource _database;


        public ClientRepository(MySqlDataSource database)
        {
            _database = database;
        }

        public List<BasicClientModel> Get()
        {
            List<BasicClientModel> clients = new List<BasicClientModel>();
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
                            clients.Add(ReadBasicClientFromReader(reader));

                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return clients;
        }

        public BasicClientModel? GetById(int id)
        {
            BasicClientModel? client = default;
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
                            client = ReadBasicClientFromReader(reader);
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return client;
        }
        public bool Insert(BasicClientModel client)
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
        public bool Delete(int id)
        {
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "DELETE FROM " + Constants.Tables.Clients + " WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        public bool Update(BasicClientModel input)
        {
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "UPDATE FROM " + Constants.Tables.Clients + "SET Name = @Name WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", input.Id);
                    command.Parameters.AddWithValue("@Name", input.Name);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        private ClientModel ReadClientFromReader(MySqlDataReader reader)
        {
            ClientModel client = new ClientModel();

            if (!reader.IsDBNull(nameof(ClientModel.Id)))
                client.Id = reader.GetInt32(nameof(ClientModel.Id));
            if (!reader.IsDBNull(nameof(ClientModel.Name)))
                client.Name = reader.GetString(nameof(ClientModel.Name));
            if (!reader.IsDBNull(nameof(ClientModel.CreateOnDate)))
                client.CreateOnDate = reader.GetDateTime(nameof(ClientModel.CreateOnDate));
            if (!reader.IsDBNull(nameof(ClientModel.LastModifiedOnDate)))
                client.LastModifiedOnDate = reader.GetDateTime(nameof(ClientModel.LastModifiedOnDate));


            return client;
        }
        private BasicClientModel ReadBasicClientFromReader(MySqlDataReader reader)
        {
            BasicClientModel client = new BasicClientModel();

            if (!reader.IsDBNull(nameof(BasicClientModel.Id)))
                client.Id = reader.GetInt32(nameof(BasicClientModel.Id));
            if (!reader.IsDBNull(nameof(BasicClientModel.Name)))
                client.Name = reader.GetString(nameof(BasicClientModel.Name));

            return client;

        }
    }
}
