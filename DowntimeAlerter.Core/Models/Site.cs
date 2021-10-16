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
        public string Email { get; set; }
    }

}