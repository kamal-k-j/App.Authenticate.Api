using App.Authenticate.Api.Entities.Response;
using App.Authenticate.Api.Options;
using App.Authenticate.Api.Services.Authenticate;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq.AutoMock;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace App.Authenticate.Api.Tests.Services.Authenticate
{
    public class CreateUserTokenTests
    {
        public Fixture AutoFixture { get; set; }

        public AutoMocker Mocker { get; set; }

        public CreateUserTokenTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenCreate()
        {
            // Arrange
            var subject = Mocker.CreateInstance<CreateUserToken>();
            var user = AutoFixture.Create<User>();
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtConfig = new JwtConfig
            {
                SecretKey = Guid.NewGuid().ToString(),
                ExpireDays = AutoFixture.Create<byte>()
            };
            var key = Encoding.ASCII.GetBytes(jwtConfig.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(jwtConfig.ExpireDays),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var securityToken = jwtTokenHandler.CreateToken(tokenDescriptor);

            Mocker.GetMock<IOptions<JwtConfig>>()
                .Setup(service => service.Value)
                .Returns(jwtConfig);

            // Act
            var result = subject.Create(jwtTokenHandler, user.Id);

            // Assert
            result.Should().BeEquivalentTo(securityToken);
        }
    }
}
