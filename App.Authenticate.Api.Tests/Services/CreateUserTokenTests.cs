using App.Authenticate.Api.Entities;
using App.Authenticate.Api.Services;
using AutoFixture;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Moq.AutoMock;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace App.Authenticate.Api.Tests.Services
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

            // Act
            var result = subject.Create(jwtTokenHandler, user.Id);

            // Assert
            result.Should().BeEquivalentTo(securityToken);
        }
    }
}
