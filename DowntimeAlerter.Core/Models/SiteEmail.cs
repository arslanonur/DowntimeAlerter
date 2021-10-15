using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Models
{
    public class SiteEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public Site Site { get; set; }
        public int SiteId { get; set; }
    }
}
