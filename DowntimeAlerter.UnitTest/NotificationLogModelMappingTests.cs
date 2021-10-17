using System;
using AutoMapper;
using DowntimeAlerter.Core.Enums;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.WebApi.DTO;
using DowntimeAlerter.WebApi.Mapping;
using Xunit;

namespace DowntimeAlerter.Test
{
    public class NotificationLogModelMappingTests
    {
        [Fact]
        public void NotificationLog_To_NotificationLogDTO_Model_Mapper_Check()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            //arrange act
            var log = new NotificationLogs();
            log.Id = 1;
            log.NotificationType = NotificationType.Email;
            log.Message = "Message";
            log.SiteName = "Google";
            log.CheckedDate = DateTime.Now;
            log.State = "Up";

            //assert
            var notificationLogDTO = mapper.Map<NotificationLogs, NotificationLogDTO>(log);
            Assert.Equal(notificationLogDTO.Id, log.Id);
            Assert.Equal(notificationLogDTO.Message, log.Message);
            Assert.Equal(notificationLogDTO.NotificationType, log.NotificationType);
            Assert.Equal(notificationLogDTO.CheckedDate, log.CheckedDate);
            Assert.Equal(notificationLogDTO.State, log.State);
            Assert.Equal(notificationLogDTO.SiteName, log.SiteName);
        }
    }
}