using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;
using TimeTrackerApi.Models;
using TimeTrackerApi.Services;


namespace TimeTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClockController : ControllerBase
    {
        private readonly ILogger<ClockController> _logger;
        private MySqlService _mySqlService;

        public ClockController(ILogger<ClockController> logger, MySqlService mySqlService)
        {
            _logger = logger;
            _mySqlService = mySqlService;
        }
        [HttpPost("GetClockIn")]

        public CheckInTime GetClockIn(int user_id, DateOnly today)
        {
            try
            {
                CheckInTime time_Clock = new CheckInTime();
                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, user_id, start_time, finish_time, isFinish, date FROM time_clock WHERE user_id = @user_id AND date = @today";

                cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;
                cmd.Parameters.Add("@today", MySqlDbType.Date).Value = today;

                _mySqlService.TryOpen();

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
                    _mySqlService.CloseConnection();
                    time_Clock.clockHistories = GetHistory(time_Clock.time_table.Id);
                }
                else
                {
                    _mySqlService.CloseConnection();
                }

                return time_Clock;

            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }
        }
        [HttpPost("ClockIn")]

        public async Task<CheckInTime> ClockIn(ClockInModel clock_in)
        {
            try
            {
                CheckInTime checkTime = GetClockIn(clock_in.user_id, clock_in.date);
                if (!checkTime.isOpen)
                {
                    MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                    cmd.CommandText = "INSERT INTO time_clock (user_id, start_time,old_start_time, isFinish, date) VALUES (@user_id, @start_time, @start_time, @isFinish, @date)";

                    cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = clock_in.user_id;
                    cmd.Parameters.Add("@start_time", MySqlDbType.Time).Value = clock_in.start_time;
                    cmd.Parameters.Add("@isFinish", MySqlDbType.Bit).Value = 0;
                    cmd.Parameters.Add("@date", MySqlDbType.Date).Value = clock_in.date;

                    _mySqlService.TryOpen();

                    cmd.ExecuteNonQuery();

                    _mySqlService.CloseConnection();
                }
                checkTime = GetClockIn(clock_in.user_id, clock_in.date);


                return Task.FromResult(checkTime).Result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        [HttpPost("ClockOut")]

        public async Task<CheckInTime> ClockOut(ClockOutModel clockOut)
        {
            try
            {
                CheckInTime checkTime = GetClockIn(clockOut.user_id, clockOut.date);
                if (checkTime.isOpen)
                {
                    if (checkTime.time_table != null && !checkTime.time_table.isFinish)
                    {
                        MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                        cmd.CommandText = "UPDATE time_clock SET finish_time = @finish_time, old_finish_time = @finish_time, isFinish = @isFinish WHERE id = @id";

                        cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = checkTime.time_table.Id;
                        cmd.Parameters.Add("@finish_time", MySqlDbType.Time).Value = clockOut.finish_time;
                        cmd.Parameters.Add("@isFinish", MySqlDbType.Bit).Value = 1;

                        _mySqlService.TryOpen();

                        cmd.ExecuteNonQuery();

                        _mySqlService.CloseConnection();
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

        [HttpPost("GetHistory/{timeClock_id}")]
        public List<ClockHistoryMin> GetHistory(int timeClock_id)
        {
            try
            {
                List<ClockHistoryMin> list = new List<ClockHistoryMin>();

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM clockhistory WHERE timeClock_id = @timeClock_id";

                cmd.Parameters.Add("@timeClock_id", MySqlDbType.Int32).Value = timeClock_id;

                _mySqlService.TryOpen();

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
                _mySqlService.CloseConnection();

                return list;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in getUserHasColection " + ex.Message);
            }
        }

        [HttpPost("GetUserHasColection/{user_id}")]
        public Dictionary<int, CollectionDictionary> GetUserHasColection(int user_id)
        {
            try
            {
                List<ProjectMin> projects = getUserHasProject(user_id);

                Dictionary<int, CollectionDictionary> dictionary = new Dictionary<int, CollectionDictionary>();

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT collection.id, collection.name FROM userhascollection INNER JOIN collection on userhascollection.collection_id = collection.id WHERE user_id = @user_id";

                cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;

                _mySqlService.TryOpen();

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
                _mySqlService.CloseConnection();

                return dictionary;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in getUserHasColection " + ex.Message);
            }
        }

        [HttpPost("GetUserHasProject")]

        public List<ProjectMin> getUserHasProject(int user_id)
        {
            try
            {
                List<ProjectMin> list = new List<ProjectMin>();

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT project.id, project.name, project.collection_id FROM userhasproject INNER JOIN project on userhasproject.project_id = project.id WHERE user_id = @user_id;";

                cmd.Parameters.Add("@user_id", MySqlDbType.Int32).Value = user_id;

                _mySqlService.TryOpen();

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

                _mySqlService.CloseConnection();

                return list; ;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in getUserHasProject " + ex.Message);
            }
        }
        [HttpPost("InsertTime")]

        public HttpStatusCode InsertTime(ClockHistoryInput clockHistory)
        {
            try
            {

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO clockHistory (project_id, timeClock_id, minutes, description) VALUES (@project_id, @timeClock_id, @minutes, @description);";

                cmd.Parameters.Add("@project_id", MySqlDbType.Int32).Value = clockHistory.Project_id;
                cmd.Parameters.Add("@timeClock_id", MySqlDbType.Int32).Value = clockHistory.TimeClock_id;
                cmd.Parameters.Add("@minutes", MySqlDbType.Int32).Value = clockHistory.Minutes;
                cmd.Parameters.Add("@description", MySqlDbType.String).Value = clockHistory.Description;

                _mySqlService.TryOpen();

                cmd.ExecuteNonQuery();

                _mySqlService.CloseConnection();

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }


    }
}

