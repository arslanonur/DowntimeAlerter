using AutoMapper;
using DowntimeAlerter.Core.Models;
using DowntimeAlerter.WebApi.DTO;
using DowntimeAlerter.WebApi.Mapping;
using Xunit;

namespace DowntimeAlerter.Test
{
    public class UserModelMappingTests
    {
        [Fact]
        public void User_To_UserDTO_Model_Mapper_Check()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = config.CreateMapper();

            //arrange act
            var user = new User();
            user.UserName = "user";
            user.Password = "1234";

            //assert
            var userDTO = mapper.Map<User, UserDTO>(user);
            Assert.Equal(userDTO.Username, user.UserName);
            Assert.Equal(userDTO.Password, user.Password);
        }
    }
}