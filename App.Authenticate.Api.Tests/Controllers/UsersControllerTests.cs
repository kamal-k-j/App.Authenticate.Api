using App.Authenticate.Api.Controllers;
using App.Authenticate.Api.Entities.Request;
using App.Authenticate.Api.Entities.Response;
using App.Authenticate.Api.Services.Authenticate;
using App.Authenticate.Api.Services.Register;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace App.Authenticate.Api.Tests.Controllers
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
        public void WhenRegister()
        {
            // Arrange
            var subject = Mocker.CreateInstance<UsersController>();
            var request = AutoFixture.Create<UserRegister>();

            // Act
            subject.Register(request);

            // Assert
            Mocker.Verify<IRegisterService>(service =>
                service.Register(request), Times.Once);
        }

        [Fact]
        public void WhenAuthenticateAndUserFound()
        {
            // Arrange
            var subject = Mocker.CreateInstance<UsersController>();
            var request = AutoFixture.Create<UserAuthenticate>();
            var response = AutoFixture.Create<User>();

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
            var request = AutoFixture.Create<UserAuthenticate>();

            Mocker.GetMock<IAuthenticateService>()
                .Setup(service => service.Authenticate(request.Email, request.Password))
                .Returns((User)null);

            // Act
            var result = subject.Authenticate(request);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
