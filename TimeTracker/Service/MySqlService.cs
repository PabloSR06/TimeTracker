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

        public CheckInTime GetClockIn(int user_id, DateOnly today)
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
                        if (!reader.IsDBNull("finish_time"))
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
                CheckInTime checkTime = GetClockIn(clock_in.user_id, clock_in.date);
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
                checkTime = GetClockIn(clock_in.user_id, clock_in.date);


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
                CheckInTime checkTime = GetClockIn(clockOut.user_id, clockOut.date);
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
                checkTime = GetClockIn(clockOut.user_id, clockOut.date);


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

        public Dictionary<int, CollectionDictionary> GetColectionDictionary()
        {
            try
            {
                List<ProjectCollection> projects = GetAllProjectsCollection();
                var collections = GetAllCollections();

                Dictionary<int, CollectionDictionary> dictionary = new Dictionary<int, CollectionDictionary>();

                foreach (var x in collections)
                {
                    CollectionDictionary collectionDictionary = new CollectionDictionary
                    {
                        Name = x.Value,
                        Projects = GetProjectByIdCollection(projects, x.Key)
                    };
                    dictionary.Add(x.Key, collectionDictionary);
                }


                return dictionary;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in getUserHasColection " + ex.Message);
            }
        }
        private List<Project> GetProjectByIdCollection(List<ProjectCollection> projects, int collection_id)
        {
            List<Project> list = new List<Project>();
            foreach (var project in projects)
            {
                if (project.Collection_Id == collection_id)
                {
                    list.Add(new Project
                    {
                        Id = project.Id,
                        Name = project.Name
                    });
                }
            }
            return list;
        }

        public List<ClockHistoryMin> getHistory(int timeClock_id)
        {
            try
            {
                List<ClockHistoryMin> list = new List<ClockHistoryMin>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM clockhistory WHERE timeClock_id = @timeClock_id";

                cmd.Parameters.Add("@timeClock_id", MySqlDbType.Int32).Value = timeClock_id;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ClockHistoryMin clockHistoryMin = new ClockHistoryMin();

                        clockHistoryMin.Id = reader.GetInt32("id");

                        clockHistoryMin.Project_id = reader.GetInt32("project_id");
                        clockHistoryMin.TimeClock_id = reader.GetInt32("timeClock_id");
                        clockHistoryMin.Minutes = reader.GetInt32("minutes");

                        if (!reader.IsDBNull("description"))
                            clockHistoryMin.Description = reader.GetString("description");

                        list.Add(clockHistoryMin);
                    }
                }
                CloseConnection();

                return list;

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

        public Dictionary<int, string> GetAllProjects()
        {
            try
            {
                Dictionary<int, string> projects = new Dictionary<int, string>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name FROM project;";

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32("id");
                        var name = reader.GetString("name");

                        projects.Add(id, name);
                    }
                }

                CloseConnection();

                return projects;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in GetAllProjects " + ex.Message);
            }
        }

        public List<ProjectCollection> GetAllProjectsCollection()
        {
            try
            {
                List<ProjectCollection> projects = new List<ProjectCollection>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name, collection_id FROM project;";

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ProjectCollection project = new ProjectCollection
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("name"),
                            Collection_Id = reader.GetInt32("collection_id")
                        };
                        projects.Add(project);
                    }
                }

                CloseConnection();

                return projects;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in GetAllProjectsCollection " + ex.Message);
            }
        }

        public Dictionary<int, string> GetAllCollections()
        {
            try
            {
                Dictionary<int, string> collections = new Dictionary<int, string>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name FROM collection;";

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var id = reader.GetInt32("id");
                        var name = reader.GetString("name");

                        collections.Add(id, name);
                    }
                }

                CloseConnection();

                return collections;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in GetAllCollections " + ex.Message);
            }
        }

        public List<UserHasValue> GetProjectUser(int project_id)
        {
            try
            {
                List<UserHasValue> list = new List<UserHasValue>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT * from userhasproject where project_id = @project_id;";

                cmd.Parameters.Add("@project_id", MySqlDbType.Int32).Value = project_id;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        UserHasValue user = new UserHasValue();

                        user.User_id = reader.GetInt32("user_id");
                        user.Id = reader.GetInt32("project_id");

                        list.Add(user);

                    }
                }
                CloseConnection();

                return list;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in GetProjectUser " + ex.Message);
            }
        }

        public List<UserHasValue> GetCollectionUser(int collection_id)
        {
            try
            {
                List<UserHasValue> list = new List<UserHasValue>();

                MySqlCommand cmd = GetConnection().CreateCommand();
                cmd.CommandText = "SELECT * from userhascollection where collection_id = @collection_id;";

                cmd.Parameters.Add("@collection_id", MySqlDbType.Int32).Value = collection_id;

                TryOpen();

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        UserHasValue user = new UserHasValue();

                        user.User_id = reader.GetInt32("user_id");
                        user.Id = reader.GetInt32("collection_id");

                        list.Add(user);

                    }
                }
                CloseConnection();

                return list;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in GetCollectionUser " + ex.Message);
            }
        }
    }
}
