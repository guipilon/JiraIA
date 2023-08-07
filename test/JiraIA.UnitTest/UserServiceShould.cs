using AutoMapper;
using JiraIA.API.Configurations;
using JiraIA.API.Controllers;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces;
using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Interfaces.Services;
using JiraIA.Domain.Models;
using JiraIA.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JiraIA.UnitTest
{
    public class UserServiceShould
    {
        private Mock<IUserRepository> _userRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private IMapper _mapper;

        public UserServiceShould()
        {
            _userRepository = new Mock<IUserRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();
        }

        [SetUp]
        public void Setup()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Test]
        public void UserServiceShouldGetAllUser()
        {
            var userToBeValidated = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Password = "password",
                Role = "developer",
                UserName = "name"
            };

            _userRepository.Setup(x => x.GetAllUsers()).Returns(new List<User>() {
                userToBeValidated
            });

            var userService = new UserService(
                    _userRepository.Object,
                    _mapper,
                    _unitOfWork.Object
                );

            var result = userService.GetAllUser();

            var resultAfterMap = _mapper.Map<List<User>>(result);

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void UserServiceShouldAddUser()
        {
            var userToBeValidated = new UserDTO()
            {
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                Password = "password",
                Role = "developer",
                UserName = "name"
            };

            var user = _mapper.Map<User>(userToBeValidated);

            _userRepository.Setup(x => x.InsertUser(It.IsAny<User>())).Returns(Task.FromResult(user));
            _unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.FromResult(true));

            var userService = new UserService(
                    _userRepository.Object,
                    _mapper,
                    _unitOfWork.Object
                );

            var result = userService.AddUser(userToBeValidated);

            Assert.That(result.Result, Is.TypeOf<UserDTO>());
        }

        [Test]
        public void UserServiceShouldDeleteUser()
        {
            var idToBeValidated = Guid.NewGuid().ToString();

            _userRepository.Setup(x => x.DeleteUserAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
            _unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.FromResult(true));

            var userService = new UserService(
                    _userRepository.Object,
                    _mapper,
                    _unitOfWork.Object
                );

            var result = userService.DeleteUser(idToBeValidated);
            /* TODO:
            I created the DeleteUser returning void.
            This is bad for testing.
            Need to change the return type maybe to bool.
            */
            Assert.Pass();
        }

    }
}