namespace TimeTracker.Models
{
    public class ProjectMin
    {
        public int Id { get; set; }
        public Project Project { get; set; }
    }
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UserHasProject
    {
        public int user_id { get; set; }
        public int project_id { get; set; }

        
    }
}
