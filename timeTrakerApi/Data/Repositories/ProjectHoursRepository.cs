using MySqlConnector;
using timeTrakerApi.Models.Project;
using timeTrakerApi.Data.Interfaces;

namespace timeTrakerApi.Data.Repositories
{
    public class ProjectHoursRepository : IProjectHoursRepository
    {
        private readonly MySqlDataSource _database;


        public ProjectHoursRepository(MySqlDataSource database)
        {
            _database = database;
        }

        public List<ProjectHoursModel> GetProjectHours()
        {
            List<ProjectHoursModel> projectHours = new List<ProjectHoursModel>();
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "SELECT * FROM " + Constants.Tables.ProjectsHours;
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projectHours.Add(ReadProjectHoursFromReader(reader));

                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return projectHours;
        }
        private ProjectHoursModel ReadProjectHoursFromReader(MySqlDataReader reader)
        {
            ProjectHoursModel projectHours = new ProjectHoursModel();

            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.Id))))
                projectHours.Id = reader.GetInt32(reader.GetOrdinal("id"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.UserId))))
                projectHours.UserId = reader.GetInt32(reader.GetOrdinal("userid"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.ProjectId))))
                projectHours.ProjectId = reader.GetInt32(reader.GetOrdinal("projectid"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.Minutes))))
                projectHours.Minutes = reader.GetInt32(reader.GetOrdinal("minutes"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.CreateOnDate))))
                projectHours.CreateOnDate = reader.GetDateTime(reader.GetOrdinal("createondate"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.LastModifiedOnDate))))
                projectHours.LastModifiedOnDate = reader.GetDateTime(reader.GetOrdinal("lastmodifiedondate"));

            return projectHours;
        }
    }
}

