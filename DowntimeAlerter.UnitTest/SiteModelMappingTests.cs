using AutoMapper;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.WebApi.DTO;
using DowntimeAlerter.WebApi.Mapping;
using Xunit;

namespace DowntimeAlerter.Test
{
    public class SiteModelMappingTests
    {
        [Fact]
        public void Site_To_SiteDTO_Model_Mapper_Check()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            //arrange act
            var site = new Site();
            site.Id = 1;
            site.Name = "New Site";
            site.Url = "https://google.com";

            //assert
            var siteDTO = mapper.Map<Site, SiteDTO>(site);
            Assert.Equal(siteDTO.Id, site.Id);
            Assert.Equal(siteDTO.Name, site.Name);
            Assert.Equal(siteDTO.Url, site.Url);
        }
    }
}