using System;
using DowntimeAlerter.Core.Enums;

namespace DowntimeAlerter.MVC.DTO
{
    public class NotificationLogDTO
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string SiteName { get; set; }
        public string Message { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime CheckedDate { get; set; }
    }
}