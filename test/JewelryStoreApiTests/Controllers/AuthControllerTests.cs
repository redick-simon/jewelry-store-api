using JewelryStoreApi;
using JewelryStoreApi.Controllers;
using JewelryStoreApi.Model;
using JewelryStoreApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace JewelryStoreApiTests
{
    public class AuthControllerTests
    {
        private Mock<IRepository> _mockRepository;
        private IUserService _userService;
        private AuthController _controller;

        private readonly IList<User> _users = new List<User>
        {
            new User { Username = "user1", Password = "PasW$r!", UserType = UserType.NormalUser}
        };

        public AuthControllerTests()
        {
            _mockRepository = new Mock<IRepository>();
            _mockRepository.Setup(repo => repo.GetUsers().Result).Returns(_users);
            _userService = new UserService(_mockRepository.Object);
            _controller = new AuthController(_userService);
;       }


        [Theory]
        [InlineData("user1", "PasW$r!", true)]
        [InlineData("user", "PasW$r!", false)]
        public void TestGet(string username, string password, bool expected)
        {
            var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");

            string encodedString = Convert.ToBase64String(bytes);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add("Authorization", $"Basic {encodedString}");

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };

            var result = _controller.Get() as ObjectResult;

            var validateResult = result.Value as ValidateResult;

            Assert.Equal(expected, validateResult.Valid);
        }
    }
}
