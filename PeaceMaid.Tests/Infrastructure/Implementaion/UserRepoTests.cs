using Xunit;
using Moq;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;
using PeaceMaid.Infrastructure.Implementation;
using PeaceMaid.Infrastructure.Middleware;
using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.Interfaces.Authentication;
using Microsoft.Extensions.Configuration;


namespace PeaceMaid.Tests.Infrastructure.Implementaion
{
    public class UserRepoTests
    {
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<UserAuth> _mockUserAuth;
        private readonly AppDbContext _dbContext;
        private readonly UserRepo _userRepo;

        public UserRepoTests()
        {
            // In-memory DbContext
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("TestDB")
                .Options;
            _dbContext = new AppDbContext(options);

            // Mock dependencies
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockUserAuth = new Mock<UserAuth>();

            // Mock IConfiguration
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(config => config["JwtSettings:SecurityKey"]).Returns("TestKey123");

            // Initialize UserRepo
            _userRepo = new UserRepo(_dbContext, _mockPasswordHasher.Object, _mockUserAuth.Object, mockConfiguration.Object);
        }


        // Tests
        [Fact]
        public async Task AddAsync_ShouldAddUser_WhenValidDataProvided()
        {
             // Arrange
            var plainPassword = "password123";
            var hashedPassword = "hashed_password";

            _mockPasswordHasher
                .Setup(ph => ph.Hash(plainPassword))
                .Returns(hashedPassword); // Mock password hashing

            var user = new User
            {
                Email = "test@example.com",
                Username = "testUser",
                HashedPass = plainPassword,
                Address = null, // Optional field
                Bookings = null, // Optional field
                Reviews = null   // Optional field
            };

            // Act
            var response = await _userRepo.AddAsync(user);

            // Assert
            Assert.True(response.Flag);
            Assert.Single(_dbContext.Users);
            var savedUser = await _dbContext.Users.FirstAsync();
            Assert.Equal("testUser", savedUser.Username);
            Assert.Equal("test@example.com", savedUser.Email);
            Assert.Equal(hashedPassword, savedUser.HashedPass);
        }


        [Fact]
        public async Task AddAsync_ShouldFail_WhenEmailIsInvalid()
        {
            // Arrange
            var hashedPassword = "hashed_password";
            _mockPasswordHasher
                .Setup(ph => ph.Hash(It.IsAny<string>()))
                .Returns(hashedPassword);

            var user = new User
            {
                Username = "TestUser",
                Email = "invalid-email", // Invalid email
                HashedPass = hashedPassword
            };

            // Act
            var response = await _userRepo.AddAsync(user);

            // Assert
            Assert.False(response.Flag);
            Assert.Equal($"Invalid email address format: {user.Email}", response.Message);
        }
    }
}
