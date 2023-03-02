using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TimeTrackerApi.Models;
using TimeTrackerApi.Services;

namespace TimeTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {

        private readonly ILogger<ProjectsController> _logger;
        private MySqlService _mySqlService;

        public ProjectsController(ILogger<ProjectsController> logger, MySqlService mySqlService)
        {
            _logger = logger;
            _mySqlService = mySqlService;
        }

        

        [HttpGet("GetColectionDictionary")]

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
        [HttpGet("GetAllProjects")]

        public Dictionary<int, string> GetAllProjects()
        {
            try
            {
                Dictionary<int, string> projects = new Dictionary<int, string>();

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name FROM project;";

                _mySqlService.TryOpen();

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

                _mySqlService.CloseConnection();

                return projects;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in GetAllProjects " + ex.Message);
            }
        }
        [HttpGet("GetAllProjectsCollection")]

        public List<ProjectCollection> GetAllProjectsCollection()
        {
            try
            {
                List<ProjectCollection> projects = new List<ProjectCollection>();

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name, collection_id FROM project;";

                _mySqlService.TryOpen();

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

                _mySqlService.CloseConnection();

                return projects;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in GetAllProjectsCollection " + ex.Message);
            }
        }

        [HttpGet("GetAllCollections")]

        public Dictionary<int, string> GetAllCollections()
        {
            try
            {
                Dictionary<int, string> collections = new Dictionary<int, string>();

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT id, name FROM collection;";

                _mySqlService.TryOpen();

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

                _mySqlService.CloseConnection();

                return collections;

            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error in GetAllCollections " + ex.Message);
            }
        }

        [HttpPost("GetProjectUser/{project_id}")]

        public List<UserHasValue> GetProjectUser(int project_id)
        {
            try
            {
                List<UserHasValue> list = new List<UserHasValue>();

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT * from userhasproject where project_id = @project_id;";

                cmd.Parameters.Add("@project_id", MySqlDbType.Int32).Value = project_id;

                _mySqlService.TryOpen();

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
                _mySqlService.CloseConnection();

                return list;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in GetProjectUser " + ex.Message);
            }
        }
        [HttpPost("GetCollectionUser/{collection_id}")]

        public List<UserHasValue> GetCollectionUser(int collection_id)
        {
            try
            {
                List<UserHasValue> list = new List<UserHasValue>();

                MySqlCommand cmd = _mySqlService.GetConnection().CreateCommand();
                cmd.CommandText = "SELECT * from userhascollection where collection_id = @collection_id;";

                cmd.Parameters.Add("@collection_id", MySqlDbType.Int32).Value = collection_id;

                _mySqlService.TryOpen();

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
                _mySqlService.CloseConnection();

                return list;

            }
            catch (Exception ex)
            {

                throw new ArgumentException("Error in GetCollectionUser " + ex.Message);
            }
        }

    }
}
