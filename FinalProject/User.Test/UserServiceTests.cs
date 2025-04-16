using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.Services;
using FinalProject.Models;
using FinalProject.Models.Entities;


namespace User.Test
{
    public class UserServiceTests
    {
        private readonly Mock<IUserService> _mockRepo;
        private readonly FinalProject.Models.Entities.User _testUser;

        public UserServiceTests()
        {
            _mockRepo = new Mock<IUserService>();
            _testUser = new FinalProject.Models.Entities.User
            {
                Id = _testUser.Id,
                Name = "Test User",
                ProfilePicture = "Test.jpeg",
                Bio = "This is a User test",
                Email = "testuser@test.com",
                Phone = "1234567890"
            };
        }

        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnUsers()
        {
            _mockRepo.Setup(repo => repo.GetAllUsersAsync()).ReturnsAsync(new List<FinalProject.Models.Entities.User> { _testUser });
            var result = await _mockRepo.Object.GetAllUsersAsync();
            Assert.Single(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
        {
            _mockRepo.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(_testUser);
            var result = await _mockRepo.Object.GetUserByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(_testUser.Id, result.Id);
        }

        [Fact]
        public async Task AddAsync_ShouldAddUser()
        {
            _mockRepo.Setup(repo => repo.AddUserAsync(It.IsAny<FinalProject.Models.Entities.User>())).Returns(Task.CompletedTask);
            await _mockRepo.Object.AddUserAsync(_testUser);
            _mockRepo.Verify(repo => repo.AddUserAsync(It.IsAny<FinalProject.Models.Entities.User>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUser()
        {
            _mockRepo.Setup(repo => repo.UpdateUserAsync(It.IsAny<FinalProject.Models.Entities.User>())).Returns(Task.CompletedTask);
            await _mockRepo.Object.UpdateUserAsync(_testUser);
            _mockRepo.Verify(repo => repo.UpdateUserAsync(It.IsAny<FinalProject.Models.Entities.User>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteUser()
        {
            _mockRepo.Setup(repo => repo.DeleteUserAsync(1)).Returns(Task.CompletedTask);
            await _mockRepo.Object.DeleteUserAsync(1);
            _mockRepo.Verify(repo => repo.DeleteUserAsync(1), Times.Once);
        }

    }
}
