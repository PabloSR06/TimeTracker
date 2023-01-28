using MySql.Data.MySqlClient;
using System.Data;
using System.Data.Common;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Service
{
    public class MySqlService
    {
        private static MySqlConnection? _mySqlConnection;
        public MySqlService(IConfiguration configuration)
        {
            var sqlIsConfigured = !string.IsNullOrEmpty(configuration["sqlkeys:server"])
                && !string.IsNullOrEmpty(configuration["sqlkeys:user"])
                && !string.IsNullOrEmpty(configuration["sqlkeys:password"])
                && !string.IsNullOrEmpty(configuration["sqlkeys:database"]);

            if (sqlIsConfigured)
            {
                string keys = $"server={configuration["sqlkeys:server"]};userid={configuration["sqlkeys:user"]};password={configuration["sqlkeys:password"]};database={configuration["sqlkeys:database"]}";
                _mySqlConnection = new MySqlConnection(keys);
            }
            else
            {
                throw new ArgumentException("SQL not configured");
            }
        }

        public void CloseConnection()
        {
            if (_mySqlConnection != null)
            {
                _mySqlConnection.Close();
            }
        }

        public void TryOpen()
        {
            if (_mySqlConnection != null)
            {
                _mySqlConnection.Open();
            }
        }
        public MySqlConnection GetConnection()
        {
            if (_mySqlConnection != null)
            {
                return _mySqlConnection;
            }
            return null;
        }

        public CheckInTime GetClockIn(int user_id, DateTime today)
        {
            try
            {
                CheckInTime time_Clock = new CheckInTime();
                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, user_id, start_time, finish_time, isFinish, date FROM time_clock WHERE user_id = @user_id AND date = @today";

                cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;
                cmd.Parameters.Add("@today", MySqlDbType.Date).Value = today;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();

                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        time_Clock.isOpen = true;
                        TimeClock timeClock = new TimeClock();

                        timeClock.Id = reader.GetInt32("id");
                        timeClock.user_id = reader.GetInt32("user_id");
                        if (!reader.IsDBNull("start_time"))
                            timeClock.start_time = reader.GetTimeSpan("start_time");
                        if(!reader.IsDBNull("finish_time"))
                            timeClock.finish_time = reader.GetTimeSpan("finish_time");
                        timeClock.isFinish = reader.GetBoolean("isFinish");
                        if (!reader.IsDBNull("date"))
                            timeClock.date = reader.GetDateTime("date");
                        

                        time_Clock.time_table = timeClock;

                    }
                }
                CloseConnection();

                return time_Clock;

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<CheckInTime> ClockIn(ClockInModel clock_in)
        {
            try
            {
                CheckInTime checkTime = GetClockIn(clock_in.user_id, DateTime.Today);
                if (!checkTime.isOpen)
                {
                    MySqlCommand cmd = GetConnection().CreateCommand();
                    cmd.CommandText = "INSERT INTO time_clock (user_id, start_time,old_start_time, isFinish, date) VALUES (@user_id, @start_time, @start_time, @isFinish, @date)";

                    cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = clock_in.user_id;
                    cmd.Parameters.Add("@start_time", MySqlDbType.Time).Value = clock_in.start_time;
                    cmd.Parameters.Add("@isFinish", MySqlDbType.Bit).Value = 0;
                    cmd.Parameters.Add("@date", MySqlDbType.Date).Value = clock_in.date;

                    TryOpen();

                    cmd.ExecuteNonQuery();

                    CloseConnection();
                }
                checkTime = GetClockIn(clock_in.user_id, DateTime.Today);


                return Task.FromResult(checkTime).Result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<CheckInTime> ClockOut(ClockOutModel clockOut)
        {
            try
            {
                CheckInTime checkTime = GetClockIn(clockOut.user_id, DateTime.Today);
                if (checkTime.isOpen)
                {
                    if (checkTime.time_table != null && !checkTime.time_table.isFinish)
                    {
                        MySqlCommand cmd = GetConnection().CreateCommand();
                        cmd.CommandText = "UPDATE time_clock SET finish_time = @finish_time, old_finish_time = @finish_time, isFinish = @isFinish WHERE id = @id";

                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = checkTime.time_table.Id;
                        cmd.Parameters.Add("@finish_time", MySqlDbType.Time).Value = clockOut.finish_time;
                        cmd.Parameters.Add("@isFinish", MySqlDbType.Bit).Value = 1;

                        TryOpen();

                        cmd.ExecuteNonQuery();

                        CloseConnection();
                    }
                }
                checkTime = GetClockIn(clockOut.user_id, DateTime.Today);


                return Task.FromResult(checkTime).Result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


        public async Task<List<CollectionMin>> getAllColections()
        {
            try
            {
                List<CollectionMin> list = new List<CollectionMin>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name FROM collection";

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CollectionMin collection = new CollectionMin();

                        collection.Id = reader.GetInt32("id");

                        if (!reader.IsDBNull("name"))
                            collection.Name = reader.GetString("name");

                        list.Add(collection);
                    }
                }
                CloseConnection();

                return Task.FromResult(list).Result; ;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in getAllCollections " + ex.Message);
            }
        }

        public  List<CollectionMin> getUserHasColection(int user_id)
        {
            try
            {
                List<CollectionMin> list = new List<CollectionMin>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT collection.id, collection.name FROM userhascollection INNER JOIN collection on userhascollection.collection_id = collection.id WHERE user_id = @user_id";

                cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CollectionMin userCollection = new CollectionMin();

                        userCollection.Id = reader.GetInt32("id");

                        userCollection.Name = reader.GetString("name");

                        list.Add(userCollection);
                    }
                }
                CloseConnection();

                return  list; ;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in getUserHasColection " + ex.Message);
            }
        }

        public List<ProjectMin> getUserHasProject(int user_id)
        {
            try
            {
                List<ProjectMin> list = new List<ProjectMin>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT project.id, project.name, project.collection_id FROM userhasproject INNER JOIN project on userhasproject.project_id = project.id WHERE user_id = 1;\r\n";

                cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProjectMin userProject = new ProjectMin();

                        userProject.Id = reader.GetInt32("id");

                        userProject.Name = reader.GetString("name");

                        userProject.collection_id = reader.GetInt32("colection_id");


                        list.Add(userProject);
                    }
                }
                CloseConnection();

                return list; ;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in getUserHasProject " + ex.Message);
            }
        }
    }
}
