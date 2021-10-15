using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Models
{
    public class Site
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public long IntervalTime { get; set; }
        public Site()
        {
            SiteEmails = new Collection<SiteEmail>();
        }
        public ICollection<SiteEmail> SiteEmails { get; set; }
    }

}