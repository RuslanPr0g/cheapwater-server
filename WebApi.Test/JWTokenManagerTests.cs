using DataAccessLibrary.DB;
using DataAccessLibrary.DB.Entities;
using DataAccessLibrary.Encryption;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WEBApi.Authentication;
using Xunit;

namespace WebApi.Tests
{
    public class JWTokenManagerTests
    {
        private static string Key = "A test key, that is used for testing";
        private JWTokenManager _systemUnderTesting;
        private readonly Mock<IUserReadRepo> _repoMock = new Mock<IUserReadRepo>();
        private readonly Mock<IEncrypter> _encrypterMock = new Mock<IEncrypter>();
        private readonly User EmptyUser = null;

        public JWTokenManagerTests()
        {
            _systemUnderTesting = new JWTokenManager(Key, _repoMock.Object, _encrypterMock.Object);
        }
        [Fact]
        public async Task Authorize_ShouldFail_NoUser()
        {
            //Arrange
            string email = GetEmail();
            string password = GetPassword();
            _repoMock.Setup(x => x.FindUserByEmailAsync(email, CancellationToken.None)).ReturnsAsync(EmptyUser);
            //Act
            var result = await _systemUnderTesting.Authorize(email, password, CancellationToken.None);
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task Authorize_ShouldFail_BadPassword()
        {
            //Arrange
            string email = GetEmail();
            string password = GetPassword();
            _repoMock.Setup(x => x.FindUserByEmailAsync(email, CancellationToken.None)).ReturnsAsync(new User
            {
                Password = "Password, that don't coincide"
            });
            _encrypterMock.Setup(x => x.Encrypt(password)).ReturnsAsync(password);
            //Act
            var result = await _systemUnderTesting.Authorize(email, password, CancellationToken.None);
            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task Authorize_ShouldWork_ValidData()
        {
            //Arrange
            string email = GetEmail();
            string password = GetPassword();
            _repoMock.Setup(x => x.FindUserByEmailAsync(email, CancellationToken.None)).ReturnsAsync(new User
            {
                Password = GetPassword(),
                Id = Guid.NewGuid().ToString()
            });
            _encrypterMock.Setup(x => x.Encrypt(password)).ReturnsAsync(password);
            //Act
            var result = await _systemUnderTesting.Authorize(email, password, CancellationToken.None);
            //Assert
            Assert.NotNull(result);
        }
        private string GetEmail()
        {
            return "email@gmail.com";
        }
        private string GetPassword()
        {
            return "password";
        }
    }
}
