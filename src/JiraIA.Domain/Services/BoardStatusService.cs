using AutoMapper;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces;
using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Interfaces.Services;
using JiraIA.Domain.Models;

namespace JiraIA.Domain.Services
{
    public class BoardStatusService : BaseService, IBoardStatusService
    {
        private IBoardStatusRepository _boardStatusRepository;
        private IMapper _mapper;

        public BoardStatusService(IBoardStatusRepository boardStatusRepository, IMapper mapper, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _boardStatusRepository = boardStatusRepository;
            _mapper = mapper;
        }

        public async Task<BoardStatusDTO> AddBoardStatus(BoardStatusDTO boardStatus)
        {
            var newBoardStatus = _mapper.Map<BoardStatus>(boardStatus);
            var boardStatusCreated = await _boardStatusRepository.InsertBoardStatus(newBoardStatus);

            if (!await CommitAsync())
            {
                return default;
            }

            return _mapper.Map<BoardStatusDTO>(boardStatusCreated);
        }

        public async Task<BoardStatusDTO> DeleteBoardStatus(string id)
        {
            var boardStatusToBeDeleated = _boardStatusRepository.GetBoardStatusById(id);

            if (boardStatusToBeDeleated == null)
            {
                return default;
            }

            boardStatusToBeDeleated.IsDeleted = true;

            _boardStatusRepository.UpdateBoardStatus(boardStatusToBeDeleated);

            if (!await CommitAsync())
            {
                return default;
            }

            return _mapper.Map<BoardStatusDTO>(boardStatusToBeDeleated);
        }

        public IEnumerable<BoardStatusDTO> GetAllBoardStatus()
        {
            var boardStatus = _boardStatusRepository.GetAllBoardStatus();
            return _mapper.Map<List<BoardStatusDTO>>(boardStatus);
        }
    }
}
