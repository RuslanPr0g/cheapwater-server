using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using WEBApi.Authentication;
using WEBApi.Controllers;
using WEBApi.DTOs;
using Xunit;

namespace WebApi.Test
{
    public class AutherizationControllerTests
    {
        private readonly AutherizationController _sut;
        private readonly Mock<IJWTokenManager> _managerMock = new Mock<IJWTokenManager>();
        private readonly Mock<IUserRepository> _repoMock = new Mock<IUserRepository>();
        private readonly IModelConverter _converter;

        public AutherizationControllerTests()
        {
            this._converter = new ModelConverter();
            _sut = new AutherizationController(_managerMock.Object, _repoMock.Object, _converter);
            
        }
        private void BasicMockSetup(UserRegistrationModel user)
        {
            _repoMock.Setup(x => x.InsertUserIntoTheDb(_converter.ConvertUserFromDTO(user))).Returns(Task.CompletedTask);
            _managerMock.Setup(x => x.Authorize("", "")).ReturnsAsync("ValidAccessToken");
        }
        [Fact]
        public async Task RegisterUser_ShouldFail_EmptyUser()
        {
            //Arrange
            UserRegistrationModel user = null;
            //Act
            BasicMockSetup(user);
            //Assert
            Assert.IsType<BadRequestObjectResult>(await _sut.RegisterUser(user));
        }
        [Fact]
        public async Task RegisterUser_ShouldFail_BadEmail()
        {
            //Arrange
            UserRegistrationModel user = new UserRegistrationModel
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
            UserRegistrationModel user = new UserRegistrationModel
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
            UserRegistrationModel user = null;
            //Act
            BasicMockSetup(user);
            //Assert
            Assert.IsType<BadRequestObjectResult>(await _sut.RegisterUser(user));
        }
    }
}
