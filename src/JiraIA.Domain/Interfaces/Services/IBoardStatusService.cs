using JiraIA.Domain.DTOs;

namespace JiraIA.Domain.Interfaces.Services
{
    public interface IBoardStatusService
    {
        IEnumerable<BoardStatusDTO> GetAllBoardStatus();

        Task<BoardStatusDTO> AddBoardStatus(BoardStatusDTO user);

        Task<BoardStatusDTO> DeleteBoardStatus(string id);
    }
}
