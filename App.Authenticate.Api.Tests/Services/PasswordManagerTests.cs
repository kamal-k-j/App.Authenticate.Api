using App.Authenticate.Api.Options;
using App.Authenticate.Api.Services;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Xunit;

namespace App.Authenticate.Api.Tests.Services
{
    public class PasswordManagerTests
    {
        public Fixture AutoFixture { get; set; }

        public AutoMocker Mocker { get; set; }

        public PasswordManagerTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenHashAndVerify()
        {
            // Arrange
            var password = AutoFixture.Create<string>();
            var security = AutoFixture.Build<SecurityConfig>()
                .With(sec => sec.SaltSize, AutoFixture.Create<int>() + 8)
                .Create();

            Mocker.GetMock<IOptions<SecurityConfig>>()
                .Setup(cfg => cfg.Value)
                .Returns(security);

            var subject = Mocker.CreateInstance<PasswordManager>();

            // Act
            var result = subject.Generate(password);

            // Assert
            result.Should().NotBe(password);
            subject.Verify(result.PasswordHash, result.PasswordSalt, password).Should().BeTrue();
        }

        [Fact]
        public void WhenVerifyWrongPassword()
        {
            // Arrange
            var password = AutoFixture.Create<string>();
            var wrongPassword = AutoFixture.Create<string>();
            var security = AutoFixture.Build<SecurityConfig>()
                .With(sec => sec.SaltSize, AutoFixture.Create<int>() + 8)
                .Create();

            Mocker.GetMock<IOptions<SecurityConfig>>()
                .Setup(cfg => cfg.Value)
                .Returns(security);

            var subject = Mocker.CreateInstance<PasswordManager>();

            // Act
            var result = subject.Generate(password);

            // Assert
            result.Should().NotBe(password);
            subject.Verify(result.PasswordHash, result.PasswordSalt, wrongPassword).Should().BeFalse();
        }
    }
}
