using System;
using System.Collections.Generic;

namespace DowntimeAlerter.MVC.DTO
{
    public class SiteDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long IntervalTime { get; set; }
        public DateTime CheckedDate { get; set; }
        public IEnumerable<SiteEmailDTO> SiteEmails { get; set; }
    }
}