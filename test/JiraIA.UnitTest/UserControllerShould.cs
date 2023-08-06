using JiraIA.API.Controllers;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JiraIA.UnitTest
{
    public class UserControllerShould
    {
        private Mock<IUserService> _userService;

        public UserControllerShould()
        {
            _userService = new Mock<IUserService>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UserControllerShouldReturnListOfUsers()
        {
            var httpContext = new DefaultHttpContext();

            var userToBeValidated = new UserDTO()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Password = "password",
                Role = "developer",
                UserName = "name"
            };

            _userService.Setup(x => x.GetAllUser()).Returns(new List<UserDTO>() {
                userToBeValidated
            });

            var userController = new UserController(
                    _userService.Object
                );

            userController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var result = userController.GetAllUser();

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var response = result.Result as OkObjectResult;
            var responsePayload = response.Value as List<UserDTO>;

            Assert.That(responsePayload.Count(), Is.EqualTo(1));
            Assert.That(responsePayload.First(), Is.EqualTo(userToBeValidated));
        }

        [Test]
        public void UserControllerShouldAddUser()
        {
            var httpContext = new DefaultHttpContext();

            var userToBeValidated = new UserDTO()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Password = "password",
                Role = "developer",
                UserName = "name"
            };

            _userService.Setup(x => x.AddUser(It.IsAny<UserDTO>())).Returns(Task.FromResult(userToBeValidated));

            var userController = new UserController(
                    _userService.Object
                );

            userController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var result = userController.AddUser(userToBeValidated);

            Assert.That(result.Result, Is.TypeOf<ActionResult<UserDTO>>());
        }

        [Test]
        public void UserControllerShouldDeleteUser()
        {
            var httpContext = new DefaultHttpContext();

            var userToBeValidated = new UserDTO()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                Password = "password",
                Role = "developer",
                UserName = "name"
            };

            _userService.Setup(x => x.DeleteUser(It.IsAny<string>())).Returns(Task.CompletedTask);

            var userController = new UserController(
                    _userService.Object
                );

            userController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var result = userController.DeleteUser(Guid.NewGuid().ToString());

            Assert.That(result.Result, Is.TypeOf<OkResult>());
        }
    }
}