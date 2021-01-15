using DataAccessLibrary.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBApi.DTOs;
using Xunit;

namespace WebApi.Tests
{
    public class ModelConverterTests
    {
        private ModelConverter _sut;
        public ModelConverterTests()
        {
            _sut = new ModelConverter();
        }
        [Fact]
        public void ConvertUserFromDTO_OutputValuesCorrespond()
        {
            //Arrange
            var dto = GetUserDto();
            var expected = GetExpectedUser();
            //Act
            var actual = _sut.ConvertUserFromDTO(dto);
            //Assert
            Assert.Equal(expected.Username, actual.Username);
            Assert.Equal(expected.Password, actual.Password);
            Assert.Equal(expected.Email, actual.Email);
        }
        [Fact]
        public void ConvertUserFromDTO_InitialBalanceIsZero()
        {
            //Arrange
            var dto = GetUserDto();
            //Act
            var actual = _sut.ConvertUserFromDTO(dto);
            //Assert
            Assert.Equal(0, actual.Balance);
        }
        private string email = "theruslan.prog@gmail.com";
        private string nickname = "RuslanPr0g";
        private string password = "Tasman2020";
        public UserRegistrationModel GetUserDto()
        {
            return new UserRegistrationModel
            {
                Email = email,
                Nickname = nickname,
                Password = password
            };
        }

        private User GetExpectedUser()
        {
            return new User
            {
               Email = email,
               Username = nickname,
               Password = password,
               Balance = 0
            };
        }
    }
}
