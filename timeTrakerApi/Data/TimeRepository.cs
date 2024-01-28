using timeTrakerApi.Data.Interface;
using MySqlConnector;
using timeTrakerApi.Models.Project;
using System.Data;

namespace timeTrakerApi.Data
{
    public class TimeRepository : ITimeRepository
    {
        private readonly MySqlDataSource _database;

        public TimeRepository(MySqlDataSource database)
        {
            _database = database;
        }

        
        public List<DayHoursModel> GetDayHoursByUserId(string userId)
        {
            List<DayHoursModel> dayHours = new List<DayHoursModel>();
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "SELECT * FROM " + Constants.Tables.DayHours + " WHERE UserId = @UserId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dayHours.Add(ReadDayHoursFromReader(reader));

                        }
                        reader.Close();
                    }
                    connection.Dispose();
                }
            }
            return dayHours;
        }

        public List<DayHoursModel> GetDayHours(HourInputModel input)
        {
            List<DayHoursModel>? dayHours = new List<DayHoursModel>();

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("GetDayHours", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", input.UserId);
                    command.Parameters.AddWithValue("@from", input.From);
                    command.Parameters.AddWithValue("@to", input.To);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dayHours.Add(ReadDayHoursFromReader(reader));
                        }
                    }
                }
                connection.Dispose();
            }
            return dayHours;
        }

        public List<ProjectHoursModel> GetProjectHours(HourInputModel input)
        {
            List<ProjectHoursModel>? projectHours = new List<ProjectHoursModel>();

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("GetProjectHours", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", input.UserId);
                    command.Parameters.AddWithValue("@from", input.From);
                    command.Parameters.AddWithValue("@to", input.To);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projectHours.Add(ReadProjectHoursFromReader(reader));
                        }
                    }
                }
                connection.Dispose();
            }
            return projectHours;
        }

        private DayHoursModel ReadDayHoursFromReader(MySqlDataReader reader)
        {
            DayHoursModel dayHours = new DayHoursModel();

            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.Id))))
                dayHours.Id = reader.GetInt32(reader.GetOrdinal("id"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.UserId))))
                dayHours.UserId = reader.GetInt32(reader.GetOrdinal("userid"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.Type))))
                dayHours.Type = reader.GetBoolean(reader.GetOrdinal("type"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.Date))))
                dayHours.Date = reader.GetDateTime(reader.GetOrdinal("date"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.CreateOnDate))))
                dayHours.CreateOnDate = reader.GetDateTime(reader.GetOrdinal("createondate"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.LastModifiedOnDate))))
                dayHours.LastModifiedOnDate = reader.GetDateTime(reader.GetOrdinal("lastmodifiedondate"));

            return dayHours;
        }
        private ProjectHoursModel ReadProjectHoursFromReader(MySqlDataReader reader)
        {
            ProjectHoursModel projectHours = new ProjectHoursModel();

            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.Id))))
                projectHours.Id = reader.GetInt32(reader.GetOrdinal("id"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.UserId))))
                projectHours.UserId = reader.GetInt32(reader.GetOrdinal("userid"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.ProjectId))))
                projectHours.ProjectId = reader.GetInt32("projectid");
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.Date))))
                projectHours.Date = reader.GetDateTime("date");
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(ProjectHoursModel.Minutes))))
                projectHours.Minutes = reader.GetInt32("minutes");
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.CreateOnDate))))
                projectHours.CreateOnDate = reader.GetDateTime(reader.GetOrdinal("createondate"));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.LastModifiedOnDate))))
                projectHours.LastModifiedOnDate = reader.GetDateTime(reader.GetOrdinal("lastmodifiedondate"));

            return projectHours;
        }
    }
}

