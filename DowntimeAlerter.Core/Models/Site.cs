namespace DowntimeAlerter.Core.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long IntervalTime { get; set; }
        public string Email { get; set; }
    }
}