using System.ComponentModel;
using System.Globalization;

namespace TimeTracker.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }
    public class CollectionMin
    {
        public string Name { get; set; }
        public List<Project> Projects { get; set; }
    }


    public class CollectionProject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Project>? Projects { get; set; }

    }

    public class CollectionDictionary
    {
        public string? Name { get; set; }
        public List<Project>? Projects { get; set; }
    }
    public class UserHasCollection
    {
        public int User_id { get; set; }
        public int Project_id { get; set; }

    }


}
