using MySqlConnector;
using timeTrakerApi.Models.Project;
using System.Data;
using System;
using timeTrakerApi.Data.Interfaces;
using timeTrakerApi.Models.Project.Input;
using timeTrakerApi.Models.Day.Input;
using timeTrakerApi.Models.Day;

namespace timeTrakerApi.Data.Repositories
{
    public class ChronoRepository : IChronoRepository
    {
        private readonly MySqlDataSource _database;

        public ChronoRepository(MySqlDataSource database)
        {
            _database = database;
        }


        public List<DayHoursModel> GetDayHours(HourInputModel input, int userId)
        {
            List<DayHoursModel>? dayHours = new List<DayHoursModel>();

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("GetDayHours", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userIdInput", userId);
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

        public List<HoursProjectModel> GetProjectHours(HourInputModel input, int userId)
        {
            List<HoursProjectModel>? projectHours = new List<HoursProjectModel>();

            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("GetProjectHours", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userIdInput", userId);
                    command.Parameters.AddWithValue("@from", input.From);
                    command.Parameters.AddWithValue("@to", input.To);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projectHours.Add(ReadHoursProjectModelFromReader(reader));
                        }
                    }
                }
                connection.Dispose();
            }
            return projectHours;
        }

        public bool InsertDayHours(DayInputModel input, int userId)
        {
            int rowsAffected = 0;
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("InsertDayHours", connection))
                {
                    DateTime date = DateTime.Now.ToUniversalTime();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@type", input.Type);
                    command.Parameters.AddWithValue("@date", input.Date);

                    rowsAffected = command.ExecuteNonQuery();
                }
                connection.Dispose();
            }
            return rowsAffected > 0;
        }

        public bool InsertProjectHours(ProjectTimeInputModel input, int userId)
        {
            int rowsAffected = 0;
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("InsertProjectHours", connection))
                {
                    DateTime date = DateTime.Now.ToUniversalTime();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@projectId", input.ProjectId);
                    command.Parameters.AddWithValue("@minutes", input.Minutes);
                    command.Parameters.AddWithValue("@date", input.Date);

                    rowsAffected = command.ExecuteNonQuery();
                }
                connection.Dispose();
            }
            return rowsAffected > 0;
        }


        private DayHoursModel ReadDayHoursFromReader(MySqlDataReader reader)
        {
            DayHoursModel dayHours = new DayHoursModel();

            if (!reader.IsDBNull(nameof(DayHoursModel.Id)))
                dayHours.Id = reader.GetInt32(nameof(DayHoursModel.Id));
            if (!reader.IsDBNull(reader.GetOrdinal(nameof(DayHoursModel.UserId))))
                dayHours.UserId = reader.GetInt32(nameof(DayHoursModel.UserId));
            if (!reader.IsDBNull(nameof(DayHoursModel.Type)))
                dayHours.Type = reader.GetBoolean(nameof(DayHoursModel.Type));
            if (!reader.IsDBNull(nameof(DayHoursModel.Date)))
                dayHours.Date = reader.GetDateTime(nameof(DayHoursModel.Date));
            if (!reader.IsDBNull(nameof(DayHoursModel.CreateOnDate)))
                dayHours.CreateOnDate = reader.GetDateTime(nameof(DayHoursModel.CreateOnDate));
            if (!reader.IsDBNull(nameof(DayHoursModel.LastModifiedOnDate)))
                dayHours.LastModifiedOnDate = reader.GetDateTime(nameof(DayHoursModel.LastModifiedOnDate));

            return dayHours;
        }
        private HoursProjectModel ReadHoursProjectModelFromReader(MySqlDataReader reader)
        {
            HoursProjectModel hoursProjectModel = new HoursProjectModel();
            if (!reader.IsDBNull(nameof(HoursProjectModel.Id)))
                hoursProjectModel.Id = reader.GetInt32(nameof(HoursProjectModel.Id));
            if (!reader.IsDBNull(nameof(HoursProjectModel.UserId)))
                hoursProjectModel.UserId = reader.GetInt32(nameof(HoursProjectModel.UserId));
            if (!reader.IsDBNull(nameof(HoursProjectModel.ProjectId)))
                hoursProjectModel.ProjectId = reader.GetInt32(nameof(HoursProjectModel.ProjectId));
            if (!reader.IsDBNull(nameof(HoursProjectModel.Date)))
                hoursProjectModel.Date = reader.GetDateTime(nameof(HoursProjectModel.Date));
            if (!reader.IsDBNull(nameof(HoursProjectModel.Minutes)))
                hoursProjectModel.Minutes = reader.GetInt32(nameof(HoursProjectModel.Minutes));
            if (!reader.IsDBNull(nameof(HoursProjectModel.ProjectName)))
                hoursProjectModel.ProjectName = reader.GetString(nameof(HoursProjectModel.ProjectName));
            if (!reader.IsDBNull(nameof(HoursProjectModel.ClientName)))
                hoursProjectModel.ClientName = reader.GetString(nameof(HoursProjectModel.ClientName));
            if (!reader.IsDBNull(nameof(HoursProjectModel.Description)))
                hoursProjectModel.Description = reader.GetString(nameof(HoursProjectModel.Description));
            return hoursProjectModel;
        }
    }
        
}

