using JiraIA.API.Controllers;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace JiraIA.UnitTest
{
    public class BoardStatusControllerShould
    {
        private Mock<IBoardStatusService> _boardStatusService;

        public BoardStatusControllerShould()
        {
            _boardStatusService = new Mock<IBoardStatusService>();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BoardStatusControllerShouldReturnListOfUsers()
        {
            var httpContext = new DefaultHttpContext();

            var boardStatusToBeValidated = new BoardStatusDTO()
            {
                Name = "InProgress",
                IsDeleted = false,
                Id = Guid.NewGuid().ToString()
            };

            _boardStatusService.Setup(x => x.GetAllBoardStatus()).Returns(new List<BoardStatusDTO>() {
                boardStatusToBeValidated
            });

            var boardStatusController = new BoardStatusController(
                    _boardStatusService.Object
                );

            boardStatusController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var result = boardStatusController.GetAllBoardStatus();

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var response = result.Result as OkObjectResult;
            var responsePayload = response.Value as List<BoardStatusDTO>;

            Assert.That(responsePayload.Count(), Is.EqualTo(1));
            Assert.That(responsePayload.First(), Is.EqualTo(boardStatusToBeValidated));
        }

        [Test]
        public void BoardStatusControllerShouldAddUser()
        {
            var httpContext = new DefaultHttpContext();

            var boardStatusToBeValidated = new BoardStatusDTO()
            {
                Name = "InProgress",
                IsDeleted = false,
                Id = Guid.NewGuid().ToString()
            };

            _boardStatusService.Setup(x => x.AddBoardStatus(It.IsAny<BoardStatusDTO>())).Returns(Task.FromResult(boardStatusToBeValidated));

            var boardStatusController = new BoardStatusController(
                    _boardStatusService.Object
                );

            boardStatusController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var result = boardStatusController.AddBoardStatus(boardStatusToBeValidated);

            Assert.That(result.Result, Is.TypeOf<ActionResult<BoardStatusDTO>>());
        }

        [Test]
        public void BoardStatusControllerShouldDeleteUser()
        {
            var httpContext = new DefaultHttpContext();

            var boardStatusToBeValidated = new BoardStatusDTO()
            {
                Name = "InProgress",
                IsDeleted = true,
                Id = Guid.NewGuid().ToString()
            };

            var boardStatusController = new BoardStatusController(
                    _boardStatusService.Object
            );

            boardStatusController.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            _boardStatusService.Setup(x => x.DeleteBoardStatus(It.IsAny<string>())).Returns(Task.FromResult(boardStatusToBeValidated));


            var result = boardStatusController.DeleteBoardStatus(Guid.NewGuid().ToString());

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            var response = result.Result as OkObjectResult;
            var responsePayload = response.Value as BoardStatusDTO;
            Assert.That(responsePayload.IsDeleted, Is.EqualTo(true));
        }
    }
}