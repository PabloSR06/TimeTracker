using timeTrakerApi.Data.Interface;
using MySqlConnector;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly MySqlDataSource _database;
        private readonly string table = "projects";


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

        public ProjectModel GetById(string id)
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
        private ProjectModel ReadProjectFromReader(MySqlDataReader reader)
        {
            
                ProjectModel project = new ProjectModel();

                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectModel.Id))))
                    project.Id = reader.GetInt32(reader.GetOrdinal("id"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectModel.Name))))
                    project.Name = reader.GetString(reader.GetOrdinal("name"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectModel.Description))))
                    project.Description = reader.GetString(reader.GetOrdinal("description"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectModel.CreateOnDate))))
                    project.CreateOnDate = reader.GetDateTime(reader.GetOrdinal("createondate"));
                if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectModel.LastModifiedOnDate))))
                    project.LastModifiedOnDate = reader.GetDateTime(reader.GetOrdinal("lastmodifiedondate"));


                return project;
            
        }
    }
}
