using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using WebAPI.Controllers;
using WebAPI.Data;
using WebAPI.DTOs;
using WebAPI.Helpers;
using WebAPITest.Data;
using Xunit.Abstractions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebAPITest
{
    public class UsersControllerTests
    {
        private readonly FakeUserRepository _repo = null;
        private readonly UsersController _controller = null;

        public UsersControllerTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>());
            var mapper = config.CreateMapper();

            _repo = new FakeUserRepository(FakeDataContext.InitData());
            _controller = new UsersController(_repo, mapper);
        }

        [Fact]
        public async void UC_GetUsers()
        {
            // Arrange
            int count = 10;
            UserParams userParams = new UserParams
            {
                PageNumber = 1,
                PageSize = 10,
                OrderBy = "userName"
            };

            // Act
            var actionResult = await _controller.GetUsers(userParams);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnUsers = result.Value as IEnumerable<MemberDto>;
            Assert.Equal(count, returnUsers.Count());
        }

        [Fact]
        public async void UC_GetUser()
        {
            // Arrange
            string username = "admin";

            // Act
            var actionResult = await _controller.GetUser(username);

            // Assert
            var result = actionResult.Result as OkObjectResult;
            var returnUser = result.Value as MemberDto;
            Assert.Equal(username, returnUser.UserName);
        }

        [Fact]
        public async void UC_UpdateUser()
        {
            // Arrange
            int userid = 2; // Lisa
            MemberUpdateDto data = new MemberUpdateDto {
                DisplayName = "Name Updated",
                FirstName = "First Name Updated",
                LastName = "Last Name Updated",
                Gender = "male"
            };

            // Act
            var actionResult = await _controller.UpdateUser(userid, data);

            // Assert
            var result = actionResult as Microsoft.AspNetCore.Mvc.NoContentResult;
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public async void UC_DeleteUser()
        {
            // Arrange
            int userid = 2; // Lisa

            // Act
            var actionResult = await _controller.DeleteUser(userid);

            // Assert
            var result = actionResult as Microsoft.AspNetCore.Mvc.NoContentResult;
            Assert.Equal(204, result.StatusCode);
        }
    }
}