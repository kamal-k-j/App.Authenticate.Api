using App.Authenticate.Api.Data;
using App.Authenticate.Api.Data.Dto;
using App.Authenticate.Api.Entities.Response;
using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;

namespace App.Authenticate.Api.Tests.Data
{
    public class GetUserTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public GetUserTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenGetAndUserFound()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetUser>();
            var email = "kamal.jassal@outlook.com";
            var password = "Password123.";
            var userDto = new UserDto
            {
                Email = email,
                FirstName = "Kamal",
                LastName = "Jassal",
                Id = 1,
                Password = password
            };
            var expectedResult = new User
            {
                Email = userDto.Email,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Id = userDto.Id,
                Token = string.Empty
            };

            // Act
            var result = subject.Get(email, password);

            // Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public void WhenGetAndUseNotFoundAndPasswordWrong()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetUser>();
            var email = "kamal.jassal@outlook.com";
            var password = AutoFixture.Create<string>();

            // Act
            var result = subject.Get(email, password);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void WhenGetAndUserNotFound()
        {
            // Arrange
            var subject = Mocker.CreateInstance<GetUser>();
            var email = AutoFixture.Create<string>();
            var password = "Password123.";

            // Act
            var result = subject.Get(email, password);

            // Assert
            result.Should().BeNull();
        }
    }
}
