using AutoMapper;
using JiraIA.API.Configurations;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces;
using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Models;
using JiraIA.Domain.Services;
using Moq;

namespace JiraIA.UnitTest
{
    public class BoardStatusServiceShould
    {
        private Mock<IBoardStatusRepository> _boardStatusRepository;
        private Mock<IUnitOfWork> _unitOfWork;
        private IMapper _mapper;

        public BoardStatusServiceShould()
        {
            _boardStatusRepository = new Mock<IBoardStatusRepository>();
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
        public void BoardStatusServiceShouldGetAllBoardStatus()
        {
            var boardStatusToBeValidated = new BoardStatus()
            {
                Id = Guid.NewGuid().ToString(),
                IsDeleted = false,
                Name = "Completed"
            };

            _boardStatusRepository.Setup(x => x.GetAllBoardStatus()).Returns(new List<BoardStatus>() {
                boardStatusToBeValidated
            });

            var boardService = new BoardStatusService(
                    _boardStatusRepository.Object,
                    _mapper,
                    _unitOfWork.Object
                );

            var result = boardService.GetAllBoardStatus();

            var resultAfterMap = _mapper.Map<List<BoardStatus>>(result);

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public void BoardStatusServiceShouldAddBoardStatus()
        {
            var boardStatusToBeValidatedDTO = new BoardStatusDTO()
            {
                Id = Guid.NewGuid().ToString(),
                IsDeleted = false,
                Name = "Completed"
            };

            var boardStatusToBeInserted = _mapper.Map<BoardStatus>(boardStatusToBeValidatedDTO);

            _boardStatusRepository.Setup(x => x.InsertBoardStatus(boardStatusToBeInserted)).Returns(Task.FromResult(boardStatusToBeInserted));
            _unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.FromResult(true));

            var boardService = new BoardStatusService(
                    _boardStatusRepository.Object,
                    _mapper,
                    _unitOfWork.Object
                );

            var result = boardService.AddBoardStatus(boardStatusToBeValidatedDTO);

            Assert.That(result.Result, Is.TypeOf<BoardStatusDTO>());
        }

        [Test]
        public void BoardStatusServiceShouldDeleteUser()
        {
            var idToBeValidated = Guid.NewGuid().ToString();

            var boardStatusToBeValidated = new BoardStatus()
            {
                Id = idToBeValidated,
                IsDeleted = false,
                Name = "Completed"
            };

            _boardStatusRepository.Setup(x => x.GetBoardStatusById(It.IsAny<string>())).Returns(boardStatusToBeValidated);
            _boardStatusRepository.Setup(x => x.UpdateBoardStatus(It.IsAny<BoardStatus>()));
            _unitOfWork.Setup(x => x.CommitAsync()).Returns(Task.FromResult(true));

            var boardService = new BoardStatusService(
                    _boardStatusRepository.Object,
                    _mapper,
                    _unitOfWork.Object
                );

            var result = boardService.DeleteBoardStatus(idToBeValidated);

            Assert.That(boardStatusToBeValidated.IsDeleted, Is.EqualTo(true));
        }

    }
}