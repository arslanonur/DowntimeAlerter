using AutoMapper;
using DowntimeAlerter.Core.Enums;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.Core.Services;
using DowntimeAlerter.Core.Utilities;
using DowntimeAlerter.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using MailKit.Net.Smtp;
using MailKit.Security;
using System.Net.Http;
using Hangfire;
using Hangfire.Storage;
using DowntimeAlerter.WebApi.ActionFilters;

namespace DowntimeAlerter.WebApi.Controllers
{
    [ServiceFilter(typeof(LoginFilterAttribute))]
    public class JobController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<JobController> _logger;
        private readonly MailSettings _mailSettings;
        private readonly IMapper _mapper;
        private readonly INotificationLogsService _notificaitionLogService;
        private readonly ISiteService _siteService;

        public JobController(ILogger<JobController> logger, ISiteService siteService, IMapper mapper,
            IOptions<MailSettings> mailSettings, INotificationLogsService notificaitionLogService)
        {
            _siteService = siteService;
            _notificaitionLogService = notificaitionLogService;
            _logger = logger;
            _mapper = mapper;
            _httpClient = new HttpClient();
            _mailSettings = mailSettings.Value;
        }

        public void StartRecurringNotificationJob()
        {
            RemoveJob();
            var sites = _siteService.GetAllSites();
            var siteResources = _mapper.Map<IEnumerable<Site>, IEnumerable<SiteDTO>>(sites.Result);
            foreach (var item in siteResources)
            {
                item.CheckedDate = DateTime.Now;
            }

            RecurringJob.AddOrUpdate(() => SendMail(siteResources), Cron.Minutely);
        }

        public void SendMail(IEnumerable<SiteDTO> siteResources)
        {
            //get all sites
            try
            {
                foreach (var item in siteResources)
                {
                    var timeDifference = (DateTime.Now - item.CheckedDate).TotalSeconds;
                    if (!(timeDifference >= item.IntervalTime)) continue;

                    try
                    {
                        var userEmails = item.Email;
                        SendEmailToSiteUsers(userEmails, item);
                        _logger.LogError("Mail gönderildi");
                    }
                    catch (Exception ex)
                    {
                        var notificaitionLog = new NotificationLogs();
                        var message = item.Url + " Name Not Resolved.";
                        notificaitionLog.Message = message;
                        notificaitionLog.SiteName = item.Name;
                        notificaitionLog.State = "Name Not Resolved";
                        notificaitionLog.NotificationType = NotificationType.Email;
                        SaveNotificatonLog(notificaitionLog);
                        _logger.LogError("An error occured for " + item.Name +
                                         " while checking health of it. System Message:" +
                                         ex.Message);
                    }

                    item.CheckedDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public void SendEmail(MailRequest mailRequest)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();

                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
        public void SendEmailToSiteUsers(string userEmail, SiteDTO site)
        {

            var responseMsg = _httpClient.GetAsync(site.Url).GetAwaiter().GetResult();
            var request = new MailRequest();
            request.ToEmail = userEmail;
            request.Subject = "Downtime Alerter";
            var notificaitionLog = new NotificationLogs();
            if ((int)responseMsg.StatusCode >= 200 && (int)responseMsg.StatusCode <= 299)
            {
                var message = site.Url + " is UP.";
                notificaitionLog.Message = message;
                notificaitionLog.SiteName = site.Name;
                notificaitionLog.State = "Up";
                notificaitionLog.NotificationType = NotificationType.Email;
                SaveNotificatonLog(notificaitionLog);
                request.Body = message;
            }
            else
            {
                var message = site.Url + " is DOWN.";
                notificaitionLog.Message = message;
                notificaitionLog.SiteName = site.Name;
                notificaitionLog.State = "Down";
                notificaitionLog.NotificationType = NotificationType.Email;
                SaveNotificatonLog(notificaitionLog);
                request.Body = message;
                SendEmail(request);
            }

        }
        public void RemoveJob()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                    RecurringJob.RemoveIfExists(recurringJob.Id);
            }
        }
        public void SaveNotificatonLog(NotificationLogs notificationLog)
        {
            try
            {
                notificationLog.CheckedDate = DateTime.Now;
                _notificaitionLogService.CreateNotificationLog(notificationLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

    }
}
