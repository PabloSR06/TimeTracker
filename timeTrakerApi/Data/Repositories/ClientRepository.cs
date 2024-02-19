﻿using MySqlConnector;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Data.Interfaces;
using System.Data;

namespace timeTrakerApi.Data.Repositories
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
    }
}
