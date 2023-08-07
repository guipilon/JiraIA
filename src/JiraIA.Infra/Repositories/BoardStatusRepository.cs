using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Models;
using MongoDB.Driver;

namespace JiraIA.Infra.Repositories
{
    public class BoardStatusRepository : BaseRepository<BoardStatus>, IBoardStatusRepository
    {
        public BoardStatusRepository(JiraIAContext context, IClientSessionHandle clientSessionHandle)
            : base(context, clientSessionHandle) { }


        public IEnumerable<BoardStatus> GetAllBoardStatus()
        {
            return GetQueryble()
                .Where(x => x.IsDeleted == false)
                .ToList();
        }

        public BoardStatus GetBoardStatusById(string id)
        {
            return GetQueryble()
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
        }

        public async void UpdateBoardStatus(BoardStatus boardStatus)
        {
            await UpdateAsync(boardStatus);
        }

        public async Task<BoardStatus> InsertBoardStatus(BoardStatus boardStatus)
        {
            await InsertAsync(boardStatus);

            return boardStatus;
        }
    }
}
