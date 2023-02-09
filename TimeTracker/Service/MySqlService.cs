using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;
using TimeTracker.Data;
using TimeTracker.Models;

namespace TimeTracker.Service
{
    public class MySqlService
    {
        private static MySqlConnection? _mySqlConnection;
        private static LoginState? _loginState;
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
                    CloseConnection();
                    time_Clock.clockHistories = getHistory(time_Clock.time_table.Id);
                }
                else
                {
                    CloseConnection();
                }

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

        public Dictionary<int, CollectionDictionary> getUserHasColection(int user_id)
        {
            try
            {
                List<ProjectMin> projects = getUserHasProject(user_id);

                Dictionary<int, CollectionDictionary> dictionary = new Dictionary<int, CollectionDictionary>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT collection.id, collection.name FROM userhascollection INNER JOIN collection on userhascollection.collection_id = collection.id WHERE user_id = @user_id";

                cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();

                
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var projectsEdit = projects.ToArray();

                        var id = reader.GetInt32("id");

                        List<Project> listProject = new List<Project>();

                        foreach (var x in projectsEdit)
                        {
                            if (x.Id == id)
                            {
                                listProject.Add(x.Project);
                                projects.Remove(x);
                            }
                        }

                        CollectionDictionary collectionDictionary = new CollectionDictionary();
                        collectionDictionary.Name = reader.GetString("name");
                        collectionDictionary.Projects = listProject;

                        dictionary.Add(id, collectionDictionary);
                    }
                }
                CloseConnection();

                return dictionary;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in getUserHasColection " + ex.Message);
            }
        }

        public List<ClockHistory> getHistory(int timeClock_id)
        {
            try
            {
                List<ClockHistory> list = new List<ClockHistory>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM clockhistory WHERE timeClock_id = @timeClock_id";

                cmd.Parameters.Add("@timeClock_id", MySqlDbType.Int32).Value = timeClock_id;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ClockHistory clockHistory = new ClockHistory();
                        ClockHistoryMin clockHistoryMin = new ClockHistoryMin();

                        //clockHistoryMin.Id = reader.GetInt32("id");
                        //clockHistoryMin.Project_name = _loginState.projects.

                        //clockHistory.Id = reader.GetInt32("id");

                        //_loginState.projects.g
                        //clockHistory.Project_id = reader.GetInt32("project_id");
                        //clockHistory.TimeClock_id = reader.GetInt32("timeClock_id");
                        //clockHistory.Minutes = reader.GetInt32("minutes");

                        if (!reader.IsDBNull("description"))
                            clockHistory.Description= reader.GetString("description");

                        list.Add(clockHistory);
                    }
                }
                CloseConnection();

                return list; ;

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
                cmd.CommandText = "SELECT project.id, project.name, project.collection_id FROM userhasproject INNER JOIN project on userhasproject.project_id = project.id WHERE user_id = @user_id;";

                cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProjectMin userProject = new ProjectMin();

                        Project project = new Project();
                        project.Id = reader.GetInt32("id");
                        project.Name = reader.GetString("name");

                        userProject.Id = reader.GetInt32("collection_id");

                        userProject.Project = project;

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

        public HttpStatusCode InsertTime(ClockHistoryInput clockHistory)
        {
            try
            {
                
                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO clockHistory (project_id, timeClock_id, minutes, description) VALUES (@project_id, @timeClock_id, @minutes, @description);";

                cmd.Parameters.Add("@project_id", MySqlDbType.Int32).Value = clockHistory.Project_id;
                cmd.Parameters.Add("@timeClock_id", MySqlDbType.Int32).Value = clockHistory.TimeClock_id;
                cmd.Parameters.Add("@minutes", MySqlDbType.Int32).Value = clockHistory.Minutes;
                cmd.Parameters.Add("@description", MySqlDbType.String).Value = clockHistory.Description;

                TryOpen();

                cmd.ExecuteNonQuery();

                CloseConnection();

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
