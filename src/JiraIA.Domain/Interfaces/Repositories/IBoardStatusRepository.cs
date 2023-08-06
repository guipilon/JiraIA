using JiraIA.Domain.Models;

namespace JiraIA.Domain.Interfaces.Repositories
{
    public interface IBoardStatusRepository : IBaseRepository<BoardStatus>
    {
        IEnumerable<BoardStatus> GetAllBoardStatus();

        Task<BoardStatus> InsertBoardStatus(BoardStatus boardStatus);

        BoardStatus GetBoardStatusById(string id);

        void UpdateBoardStatus(BoardStatus boardStatus);
    }
}
