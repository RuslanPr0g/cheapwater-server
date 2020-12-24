using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBApi.Authentication;
using WEBApi.Controllers;
using Xunit;

namespace DataAccessLibrary.Tests
{
    public class AutherizationControllerTests
    {
        private readonly AutherizationController _sut;
        //private readonly Mock<IJWTokenManager> _managerMock = new Mock<IJWTokenManager>();
        //private readonly Mock<IUserRepository> _repoMock = new Mock<IUserRepository>();
        /*public AutherizationControllerTests()
        {
            //_sut = new AutherizationController(_managerMock.Object, _repoMock.Object);
        }*//*

        [Fact]
        public async Task RegisterUser_ShouldFail_EmptyUser()
        {
            //Arrange
            UserModel user = null;
            //Act
            _repoMock.Setup(x => x.InsertUserIntoTheDb(user)).Returns(Task.CompletedTask);
            _managerMock.Setup(x => x.Authorize("","")).ReturnsAsync("ValidAccessToken");
            //Assert
            await Assert.ThrowsAsync<Exception>(async () => { await _sut.RegisterUser(user); });
        }
        [Fact]
        public async Task RegisterUser_ShouldFail_ImproperEmail()
        {
            //Arrange
            UserModel user = new UserModel
            {
                Email = "improper email",
                Nickname = "ProperNickName",
                Password = "Password123"
            };
            //Act
            _repoMock.Setup(x => x.InsertUserIntoTheDb(user)).Returns(Task.CompletedTask);
            _managerMock.Setup(x => x.Authorize("", "")).ReturnsAsync("ValidAccessToken");

            //Assert
            await Assert.ThrowsAsync<Exception>(async () => { await _sut.RegisterUser(user); });
        }*/
        [Fact]
        public async Task penis()
        {
            Assert.Equal("penis", "penis");
        }

    }
}
