using App.Authenticate.Api.Data;
using App.Authenticate.Api.Entities.Response;
using App.Authenticate.Api.Services;
using AutoFixture;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Moq.AutoMock;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace App.Authenticate.Api.Tests.Services
{
    public class AuthenticateServiceTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public AuthenticateServiceTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenAuthenticateSuccess()
        {
            // Arrange
            var subject = Mocker.CreateInstance<AuthenticateService>();
            var email = AutoFixture.Create<string>();
            var password = AutoFixture.Create<string>();
            var user = AutoFixture.Create<User>();
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Guid.NewGuid().ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var securityToken = jwtTokenHandler.CreateToken(tokenDescriptor);
            var expectedToken = jwtTokenHandler.WriteToken(securityToken);

            Mocker.GetMock<IGetUser>()
                .Setup(getter => getter.Get(email, password))
                .Returns(user);

            Mocker.GetMock<ICreateUserToken>()
                .Setup(service => service.Create(
                    It.IsAny<JwtSecurityTokenHandler>(),
                    user.Id)
                )
                .Returns(securityToken);

            // Act
            var result = subject.Authenticate(email, password);

            // Assert
            result.Should().BeEquivalentTo(user, options => options.Excluding(u => u.Token));
            result.Token.Should().Be(expectedToken);
        }

        [Fact]
        public void WhenAuthenticateFails()
        {
            // Arrange
            var subject = Mocker.CreateInstance<AuthenticateService>();
            var email = AutoFixture.Create<string>();
            var password = AutoFixture.Create<string>();
            var user = AutoFixture.Create<User>();

            Mocker.GetMock<IGetUser>()
                .Setup(getter => getter.Get(email, password))
                .Returns((User)null);

            // Act
            var result = subject.Authenticate(email, password);

            // Assert
            Mocker.Verify<ICreateUserToken>(service => service.Create(
                    It.IsAny<JwtSecurityTokenHandler>(),
                    user.Id),
                Times.Never
            );
            result.Should().BeNull();
        }
    }
}
