using System;

namespace DowntimeAlerter.WebApi.DTO
{
    public class SiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long IntervalTime { get; set; }
        public string Email { get; set; }
        public DateTime CheckedDate { get; set; }
    }
}