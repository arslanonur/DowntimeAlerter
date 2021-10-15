using DowntimeAlerter.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Core.Models
{
    public class NotificationLogs
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string SiteName { get; set; }                
        public NotificationType NotificationType { get; set; }
        public DateTime CheckedDate { get; set; }
        public string Message { get; set; }
    }
}
