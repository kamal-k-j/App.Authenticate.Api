using App.Authenticate.Api.Data.Dto;
using App.Authenticate.Api.Entities.Response;
using AutoFixture;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;

namespace App.Authenticate.Api.Tests.Data.Dto
{
    public class UserDtoTests
    {
        public Fixture AutoFixture { get; set; }
        public AutoMocker Mocker { get; set; }

        public UserDtoTests()
        {
            AutoFixture = new Fixture();
            Mocker = new AutoMocker();
        }

        [Fact]
        public void WhenCastingToUser()
        {
            // Arrange
            var subject = Mocker.CreateInstance<UserDto>();

            // Act
            var result = (User)subject;

            // Assert
            result.Should().BeEquivalentTo(subject, options =>
                options.Excluding(u => u.Password));
            result.Token.Should().BeEmpty();
        }
    }
}
