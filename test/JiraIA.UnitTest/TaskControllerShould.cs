using JiraIA.API.Controllers;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net.Http;

namespace JiraIA.UnitTest
{
    public class TaskControllerShould
    {
        private Mock<ITaskService> _taskService;
        private DefaultHttpContext _httpContext;

        public TaskControllerShould()
        {
            _taskService = new Mock<ITaskService>();
        }

        [SetUp]
        public void Setup()
        {
            _httpContext = new DefaultHttpContext();
        }

        [Test]
        public void TaskControllerShouldReturnListOfTasks()
        {
            var taskToBeValidated = new TaskDTO()
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

            _taskService.Setup(x => x.GetAllTasks()).Returns(new List<TaskDTO>() {
                taskToBeValidated
            });

            var taskController = new TaskController(
                    _taskService.Object
                );

            taskController.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };

            var result = taskController.GetAllTasks();

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var response = result.Result as OkObjectResult;
            var responsePayload = response.Value as List<TaskDTO>;

            Assert.That(responsePayload.Count(), Is.EqualTo(1));
            Assert.That(responsePayload.First(), Is.EqualTo(taskToBeValidated));
        }

        [Test]
        public void TaskControllerShouldReturnListOfTasksByUser()
        {
            var taskToBeValidated = new TaskDTO()
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

            _taskService.Setup(x => x.GetTaskByUser(It.IsAny<string>())).Returns(new List<TaskDTO>() {
                taskToBeValidated
            });

            var taskController = new TaskController(
                    _taskService.Object
                );

            taskController.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };

            var result = taskController.GetTaskByUser("user");

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var response = result.Result as OkObjectResult;
            var responsePayload = response.Value as List<TaskDTO>;

            Assert.That(responsePayload.Count(), Is.EqualTo(1));
            Assert.That(responsePayload.First(), Is.EqualTo(taskToBeValidated));
        }

        [Test]
        public void TaskControllerShouldReturnListOfTasksByStatus()
        {
            var taskToBeValidated = new TaskDTO()
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

            _taskService.Setup(x => x.GetTaskByStatus(It.IsAny<string>())).Returns(new List<TaskDTO>() {
                taskToBeValidated
            });

            var taskController = new TaskController(
                    _taskService.Object
                );

            taskController.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };

            var result = taskController.GetTaskByStatus("user");

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var response = result.Result as OkObjectResult;
            var responsePayload = response.Value as List<TaskDTO>;

            Assert.That(responsePayload.Count(), Is.EqualTo(1));
            Assert.That(responsePayload.First(), Is.EqualTo(taskToBeValidated));
        }

        [Test]
        public void TaskControllerShouldReturnListOfTasksById()
        {
            var taskToBeValidated = new TaskDTO()
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

            _taskService.Setup(x => x.GetTaskById(It.IsAny<string>())).Returns(taskToBeValidated);

            var taskController = new TaskController(
                    _taskService.Object
                );

            taskController.ControllerContext = new ControllerContext()
            {
                HttpContext = _httpContext
            };

            var result = taskController.GetTaskById("id");

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var response = result.Result as OkObjectResult;
            var responsePayload = response.Value as TaskDTO;

            Assert.That(responsePayload, Is.EqualTo(taskToBeValidated));
        }

        [Test]
        public void TaskControllerShouldAddTask()
        {
            var httpContext = new DefaultHttpContext();

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

            _taskService.Setup(x => x.CreateTask(It.IsAny<TaskDTO>())).Returns(Task.FromResult(taskToBeCreated));

            var taskController = new TaskController(
                    _taskService.Object
                );

            taskController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var result = taskController.CreateTask(taskToBeCreated);

            Assert.That(result.Result, Is.TypeOf<ActionResult<TaskDTO>>());
        }

        [Test]
        public void TaskControllerShouldDeleteTask()
        {
            var httpContext = new DefaultHttpContext();

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

            _taskService.Setup(x => x.DeleteTask(It.IsAny<string>())).Returns(Task.FromResult(taskToBeDeleted));

            var taskController = new TaskController(
                    _taskService.Object
                );

            taskController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var result = taskController.DeleteTask(Guid.NewGuid().ToString());

            Assert.That(result.Result, Is.TypeOf<OkResult>());
        }

        [Test]
        public void TaskControllerShouldUpdateTask()
        {
            var httpContext = new DefaultHttpContext();

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

            _taskService.Setup(x => x.UpdateTask(It.IsAny<TaskDTO>())).Returns(Task.FromResult(taskToBeUpdated));

            var taskController = new TaskController(
                    _taskService.Object
                );

            taskController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var result = taskController.UpdateTask(taskToBeUpdated);

            Assert.That(result.Result, Is.TypeOf<ActionResult<TaskDTO>>());
        }
    }
}