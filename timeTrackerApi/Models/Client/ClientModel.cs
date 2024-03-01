namespace timeTrackerApi.Models.Client
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreateOnDate { get; set; }
        public DateTime? LastModifiedOnDate { get; set; }
    }
}
