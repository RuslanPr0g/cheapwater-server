using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using DataAccessLibrary.DB.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using WEBApi.Authentication;
using WEBApi.Controllers;
using Xunit;

namespace WebApi.Test
{
    public class AutherizationControllerTests
    {
        private readonly AutherizationController _sut;
        private readonly Mock<IJWTokenManager> _managerMock = new Mock<IJWTokenManager>();
        private readonly Mock<IUserRepository> _repoMock = new Mock<IUserRepository>();
        public AutherizationControllerTests()
        {
            _sut = new AutherizationController(_managerMock.Object, _repoMock.Object);
        }
        private void BasicMockSetup(UserModel user)
        {
            _repoMock.Setup(x => x.InsertUserIntoTheDb(user)).Returns(Task.CompletedTask);
            _managerMock.Setup(x => x.Authorize("", "")).ReturnsAsync("ValidAccessToken");
        }
        [Fact]
        public async Task RegisterUser_ShouldFail_EmptyUser()
        {
            //Arrange
            UserModel user = null;
            //Act
            BasicMockSetup(user);
            //Assert
            Assert.IsType<BadRequestObjectResult>(await _sut.RegisterUser(user));
        }
        [Fact]
        public async Task RegisterUser_ShouldFail_BadEmail()
        {
            //Arrange
            UserModel user = new UserModel
            {
                Email = "improper email",
                Nickname = "ProperNickName",
                Password = "Password123"
            };
            //Act
            BasicMockSetup(user);
            //Assert
            Assert.IsType<BadRequestObjectResult>(await _sut.RegisterUser(user));
        }
        [Fact]
        public async Task RegisterUser_ShouldFail_BadNickname()
        {
            //Arrange
            UserModel user = new UserModel
            {
                Email = "email@gmail.com",
                Nickname = "1",
                Password = "Password123"
            };
            //Act
            BasicMockSetup(user);
            //Assert
            Assert.IsType<BadRequestObjectResult>(await _sut.RegisterUser(user));
        }
        [Fact]
        public async Task LoginUser_ShouldFail_EmptyUser()
        {
            //Arrange
            UserModel user = null;
            //Act
            BasicMockSetup(user);
            //Assert
            Assert.IsType<BadRequestObjectResult>(await _sut.RegisterUser(user));
        }
    }
}
