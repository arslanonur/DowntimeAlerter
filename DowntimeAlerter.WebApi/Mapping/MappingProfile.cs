using AutoMapper;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.WebApi.DTO;

namespace DowntimeAlerter.WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource            
            CreateMap<Site, SiteDTO>();            
            CreateMap<Logs, LogsDTO>();
            CreateMap<NotificationLogs, NotificationLogDTO>();

            // Resource to Domain            
            CreateMap<SiteDTO, Site>();            
            CreateMap<LogsDTO, Logs>();
            CreateMap<NotificationLogDTO, NotificationLogs>();            
        }
    }
}