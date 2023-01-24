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
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
