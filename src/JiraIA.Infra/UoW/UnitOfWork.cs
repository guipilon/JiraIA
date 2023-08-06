using JiraIA.Domain.Interfaces;
using MongoDB.Driver;

namespace JiraIA.Infra.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IClientSessionHandle _clientSessionHandle;

        public UnitOfWork(IClientSessionHandle clientSessionHandle)
        {
            _clientSessionHandle = clientSessionHandle;
        }

        public async Task<bool> CommitAsync()
        {
            try
            {
                await _clientSessionHandle.CommitTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
