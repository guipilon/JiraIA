using AutoMapper;
using JiraIA.API.Configurations;
using JiraIA.API.Controllers;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces;
using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Models;
using JiraIA.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JiraIA.UnitTest
{
    public class TaskServiceShould
    {
        private Mock<ITaskRepository> _taskRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private IMapper _mapper;

        public TaskServiceShould()
        {
            _taskRepository = new Mock<ITaskRepository>();
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
        public void TaskServiceShouldReturnListOfTasks()
        {
            var taskToBeValidated = new TaskModel()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                AssignedTo = "user",
                Deadline = DateTime.Now.AddDays(1),
                Description = "description",
                IsFavorited = true,
                Name = "name",
                Status = "InProgress"
            };

            _taskRepository.Setup(x => x.GetAllTasks()).Returns(new List<TaskModel>() {
                taskToBeValidated
            });

            var taskService = new TaskService(
                _taskRepository.Object,
                _mapper,
                _unitOfWork.Object
                );

            var result = taskService.GetAllTasks();

            var resultAfterMap = _mapper.Map<List<TaskModel>>(result);

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void TaskServiceShouldReturnListOfTasksByUser()
        {
            var taskToBeValidated = new TaskModel()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                AssignedTo = "user",
                Deadline = DateTime.Now.AddDays(1),
                Description = "description",
                IsFavorited = true,
                Name = "name",
                Status = "InProgress"
            };

            _taskRepository.Setup(x => x.GetTaskByUser(It.IsAny<string>())).Returns(new List<TaskModel>() {
                taskToBeValidated
            });

            var taskService = new TaskService(
                _taskRepository.Object,
                _mapper,
                _unitOfWork.Object
                );

            var result = taskService.GetTaskByUser("user");

            var resultAfterMap = _mapper.Map<List<TaskModel>>(result);

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void TaskServiceShouldReturnListOfTasksByStatus()
        {
            var taskToBeValidated = new TaskModel()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                AssignedTo = "user",
                Deadline = DateTime.Now.AddDays(1),
                Description = "description",
                IsFavorited = true,
                Name = "name",
                Status = "InProgress"
            };

            _taskRepository.Setup(x => x.GetTaskByStatus(It.IsAny<string>())).Returns(new List<TaskModel>() {
                taskToBeValidated
            });

            var taskService = new TaskService(
                _taskRepository.Object,
                _mapper,
                _unitOfWork.Object
                );

            var result = taskService.GetTaskByStatus("InProgress");

            var resultAfterMap = _mapper.Map<List<TaskModel>>(result);

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void TaskServiceShouldReturnListOfTasksById()
        {
            var taskToBeValidated = new TaskModel()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                AssignedTo = "user",
                Deadline = DateTime.Now.AddDays(1),
                Description = "description",
                IsFavorited = true,
                Name = "name",
                Status = "InProgress"
            };

            _taskRepository.Setup(x => x.GetTaskById(It.IsAny<string>())).Returns(taskToBeValidated);

            var taskService = new TaskService(
                _taskRepository.Object,
                _mapper,
                _unitOfWork.Object
                );

            var result = taskService.GetTaskById("id");

            var resultAfterMap = _mapper.Map<TaskModel>(result);

            Assert.That(resultAfterMap, Is.EqualTo(taskToBeValidated));
        }

        [Test]
        public void TaskServiceShouldAddTask()
        {
            var taskToBeCreated = new TaskDTO()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                AssignedTo = "user",
                Deadline = DateTime.Now.AddDays(1),
                Description = "description",
                IsFavorited = true,
                Name = "name",
                Status = "InProgress"
            };

            var taskModel = _mapper.Map<TaskModel>(taskToBeCreated);

            _taskRepository.Setup(x => x.CreateTask(It.IsAny<TaskModel>())).Returns(Task.FromResult(taskModel));
            _unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.FromResult(true));

            var taskService = new TaskService(
                _taskRepository.Object,
                _mapper,
                _unitOfWork.Object
                );

            var result = taskService.CreateTask(taskToBeCreated);

            Assert.That(result.Result, Is.TypeOf<TaskDTO>());
        }

        [Test]
        public void TaskServiceShouldDeleteTask()
        {
            var taskToBeDeleted = new TaskDTO()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                AssignedTo = "user",
                Deadline = DateTime.Now.AddDays(1),
                Description = "description",
                IsFavorited = true,
                Name = "name",
                Status = "InProgress"
            };

            var taskModel = _mapper.Map<TaskModel>(taskToBeDeleted);

            _taskRepository.Setup(x => x.GetTaskById(It.IsAny<string>())).Returns(taskModel);
            _taskRepository.Setup(x => x.DeleteTask(It.IsAny<string>())).Returns(Task.FromResult(taskModel));
            _unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.FromResult(true));

            var taskService = new TaskService(
                _taskRepository.Object,
                _mapper,
                _unitOfWork.Object
                );

            var result = taskService.DeleteTask("id");

            Assert.That(result.Result, Is.TypeOf<TaskDTO>());
        }

        [Test]
        public void TaskServiceShouldUpdateTask()
        {
            var taskToBeUpdated = new TaskDTO()
            {
                CreatedAt = DateTime.Now,
                Id = Guid.NewGuid().ToString(),
                AssignedTo = "user",
                Deadline = DateTime.Now.AddDays(1),
                Description = "description",
                IsFavorited = true,
                Name = "name",
                Status = "InProgress"
            };

            var taskModel = _mapper.Map<TaskModel>(taskToBeUpdated);

            _taskRepository.Setup(x => x.UpdateTask(It.IsAny<TaskModel>())).Returns(Task.FromResult(taskModel));
            _unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.FromResult(true));

            var taskService = new TaskService(
                _taskRepository.Object,
                _mapper,
                _unitOfWork.Object
                );

            var result = taskService.UpdateTask(taskToBeUpdated);

            Assert.That(result.Result, Is.TypeOf<TaskDTO>());
        }
    }
}