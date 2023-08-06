using JiraIA.Domain.Interfaces;

namespace JiraIA.Domain.Services
{
    public abstract class BaseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected async Task<bool> CommitAsync()
        {
            return await _unitOfWork.CommitAsync();
        }
    }
}
