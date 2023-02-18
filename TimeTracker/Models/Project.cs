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
        public int User_id { get; set; }
        public int Project_id { get; set; }
     
    }

    public class ProjectCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Collection_Id { get; set; }

    }


}
