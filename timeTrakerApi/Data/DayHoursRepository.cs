using timeTrakerApi.Data.Interface;
using MySqlConnector;
using timeTrakerApi.Models.Project;

namespace timeTrakerApi.Data
{
    public class DayHoursRepository : IDayHoursRepository
    {
        private readonly MySqlDataSource _database;

        public DayHoursRepository(MySqlDataSource database)
        {
            _database = database;
        }

        public List<DayHoursModel> GetDayHours()
        {
            List<DayHoursModel> dayHours = new List<DayHoursModel>();
            using (MySqlConnection connection = _database.CreateConnection())
            {
                connection.Open();

                string query = "SELECT * FROM " + Constants.Tables.DayHours;
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
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
    }
}

