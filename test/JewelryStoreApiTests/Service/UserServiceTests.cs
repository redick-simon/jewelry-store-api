using JewelryStoreApi;
using JewelryStoreApi.Model;
using JewelryStoreApi.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JewelryStoreApiTests.Service
{
    public class UserServiceTests
    {
        private Mock<IRepository> _mockRepository;
        private IUserService _userService;

        private readonly IList<User> _users = new List<User>
        {
            new User { Username = "user1", Password = "PasW$r!", UserType = UserType.NormalUser },
            new User { Username = "user2", Password = "Spec@!#!", UserType = UserType.PrivilegedUser },
        };

        public UserServiceTests()
        {
            _mockRepository = new Mock<IRepository>();
            _mockRepository.Setup(repo => repo.GetUsers().Result).Returns(_users);
            _userService = new UserService(_mockRepository.Object);
        }

        [Theory]
        [InlineData("user1", "PasW$r!", true, "NormalUser")]
        [InlineData("user", "PasW$r!", false, "NotFound")]
        [InlineData("user2", "Spec@!#!", true, "PrivilegedUser")]
        [InlineData(null, null, false, "NotFound")]
        public void TestValidate(string username, string password, bool expected, string expectedType)
        {
            var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");

            string basicAuth = $"Basic {Convert.ToBase64String(bytes)}";

            var validateResult = _userService.Validate(basicAuth);

            Assert.Equal(expected, validateResult.Valid);
            Assert.Equal(expectedType, validateResult.UserType);
        }
    }
}
