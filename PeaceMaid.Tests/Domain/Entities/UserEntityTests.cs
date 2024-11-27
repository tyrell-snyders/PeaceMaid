using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Tests.Domain.Entities
{
    public class UserEntityTests
    {
        [Fact]
        public void UserEntity_ShouldInitializeCorrectly()
        {
            // Arrange & Act
            var user = new User
            {
                Id = 1,
                Username = "Test User",
                Email = "test@example.com",
                HashedPass = "testPassword_129-204898"
            };

            // Assert
            Assert.Equal(1, user.Id);
            Assert.Equal("Test User", user.Username);
            Assert.Equal("test@example.com", user.Email);
            Assert.Equal("testPassword_129-204898", user.HashedPass);
        }
    }
}
