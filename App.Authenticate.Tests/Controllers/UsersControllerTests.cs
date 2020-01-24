using App.Authenticate.Controllers;
using App.Authenticate.Models.Request;
using App.Authenticate.Services;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq.AutoMock;
using Xunit;

namespace App.Authenticate.Tests.Controllers
{
    public class UsersControllerTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public UsersControllerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenAuthenticateAndUserFound()
        {
            // Arrange
            var subject = Mocker.CreateInstance<UsersController>();
            var request = AutoFixture.Create<User>();
            var response = AutoFixture.Create<Entities.User>();

            Mocker.GetMock<IAuthenticateService>()
                .Setup(service => service.Authenticate(request.Email, request.Password))
                .Returns(response);

            // Act
            var result = subject.Authenticate(request);

            // Assert
            ((OkObjectResult)result).Value.Should().Be(response);
        }

        [Fact]
        public void WhenAuthenticateAndUserNotFound()
        {
            // Arrange
            var subject = Mocker.CreateInstance<UsersController>();
            var request = AutoFixture.Create<User>();

            Mocker.GetMock<IAuthenticateService>()
                .Setup(service => service.Authenticate(request.Email, request.Password))
                .Returns((Entities.User)null);

            // Act
            var result = subject.Authenticate(request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
