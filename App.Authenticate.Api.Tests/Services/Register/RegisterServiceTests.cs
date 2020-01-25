using App.Authenticate.Api.Data;
using App.Authenticate.Api.Entities.Request;
using App.Authenticate.Api.Entities.Response;
using App.Authenticate.Api.Services.Register;
using AutoFixture;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace App.Authenticate.Api.Tests.Services.Register
{
    public class RegisterServiceTests
    {
        public Fixture AutoFixture { get; set; }

        public AutoMocker Mocker { get; set; }

        public RegisterServiceTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenRegister()
        {
            // Arrange
            var subject = Mocker.CreateInstance<RegisterService>();
            var userRegister = AutoFixture.Create<UserRegister>();
            var user = AutoFixture.Create<User>();

            Mocker.GetMock<IMapUserRegister>()
                .Setup(service => service.Map(userRegister))
                .Returns(user);

            // Act
            subject.Register(userRegister);

            // Assert
            Mocker.Verify<IStoreUser>(service =>
                service.Store(user), Times.Once);
        }
    }
}
