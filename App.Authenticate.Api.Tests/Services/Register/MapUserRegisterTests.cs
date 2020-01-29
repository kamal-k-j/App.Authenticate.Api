using App.Authenticate.Api.Entities.Request;
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

            // Act
            var result = subject.Map(userRegister);

            // Assert
            result.Should().BeEquivalentTo(userRegister);
        }
    }
}
