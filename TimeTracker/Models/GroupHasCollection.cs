namespace TimeTracker.Models
{
    public class UserHasCollection
    {       
        public int user_id { get; set; }
        public int collection_id { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class UserHasCollectionMin
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
