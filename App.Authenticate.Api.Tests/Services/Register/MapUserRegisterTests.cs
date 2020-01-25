using App.Authenticate.Api.Entities.Request;
using App.Authenticate.Api.Services;
using App.Authenticate.Api.Services.Register;
using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;

namespace App.Authenticate.Api.Tests.Services.Register
{
    public class MapUserRegisterTests
    {
        public Fixture AutoFixture { get; set; }

        public AutoMocker Mocker { get; set; }

        public MapUserRegisterTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenMap()
        {
            // Arrange
            var subject = Mocker.CreateInstance<MapUserRegister>();
            var userRegister = AutoFixture.Create<UserRegister>();
            var generatedPassword = AutoFixture.Create<GeneratedPassword>();

            Mocker.GetMock<IPasswordManager>()
                .Setup(service => service.Generate(userRegister.Password))
                .Returns(generatedPassword);

            // Act
            var result = subject.Map(userRegister);

            // Assert
            result.Should().BeEquivalentTo(userRegister, options => options
                .Excluding(u => u.Password)
            );
            result.PasswordHash.Should().BeEquivalentTo(generatedPassword.PasswordHash);
            result.HashSalt.Should().BeEquivalentTo(generatedPassword.PasswordSalt);
        }
    }
}
