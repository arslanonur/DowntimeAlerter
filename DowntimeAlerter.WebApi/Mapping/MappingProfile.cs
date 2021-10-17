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
            CreateMap<Log, LogDTO>();
            CreateMap<NotificationLogs, NotificationLogDTO>();
            CreateMap<User, UserDTO>();

            // Resource to Domain            
            CreateMap<SiteDTO, Site>();            
            CreateMap<LogDTO, Log>();
            CreateMap<NotificationLogDTO, NotificationLogs>();            
            CreateMap<UserDTO, User>();            
        }
    }
}