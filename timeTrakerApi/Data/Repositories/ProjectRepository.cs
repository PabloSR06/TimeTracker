using MySqlConnector;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Data.Interfaces;
using System.Data;

namespace timeTrakerApi.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly MySqlDataSource _database;


        public ProjectRepository(MySqlDataSource database)
        {
            _database = database;
        }

        public List<ProjectModel> Get()
        {
            List<ProjectModel> projects = new List<ProjectModel>();
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "SELECT * FROM " + Constants.Tables.Projects;
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(ReadProjectFromReader(reader));

                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return projects;
        }

        public ProjectModel GetById(int id)
        {
            ProjectModel project = new ProjectModel();
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                string query = "SELECT * FROM " + Constants.Tables.Projects + " WHERE Id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            project = ReadProjectFromReader(reader);
                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return project;
        }
        public bool Insert(ProjectModel project)
        {
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "INSERT INTO " + Constants.Tables.Projects + " (Name, Description) VALUES (@Name, @Description)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", project.Name);
                    command.Parameters.AddWithValue("@Description", project.Description);

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

                string query = "DELETE FROM " + Constants.Tables.Projects + " WHERE Id = @Id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
        }
        private ProjectModel ReadProjectFromReader(MySqlDataReader reader)
        {
            ProjectModel project = new ProjectModel();

            if (!reader.IsDBNull(nameof(ProjectModel.Id)))
                project.Id = reader.GetInt32(nameof(ProjectModel.Id));
            if (!reader.IsDBNull(nameof(ProjectModel.ClientId)))
                project.ClientId = reader.GetInt32(nameof(ProjectModel.ClientId));
            if (!reader.IsDBNull(nameof(ProjectModel.Name)))
                project.Name = reader.GetString(nameof(ProjectModel.Name));
            if (!reader.IsDBNull(nameof(ProjectModel.Description)))
                project.Description = reader.GetString(nameof(ProjectModel.Description));
            if (!reader.IsDBNull(nameof(ProjectModel.CreateOnDate)))
                project.CreateOnDate = reader.GetDateTime(nameof(ProjectModel.CreateOnDate));
            if (!reader.IsDBNull(nameof(ProjectModel.LastModifiedOnDate)))
                project.LastModifiedOnDate = reader.GetDateTime(nameof(ProjectModel.LastModifiedOnDate));

            return project;
        }
    }
}
