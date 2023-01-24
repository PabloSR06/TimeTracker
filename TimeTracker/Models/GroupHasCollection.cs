namespace TimeTracker.Models
{
    public class GroupHasCollection
    {       
        public int group_id { get; set; }
        public int collection_id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

    }

    public class GroupHasCollectionMin
    {
        public int group_id { get; set; }
        public int collection_id { get; set; }
    }
}
