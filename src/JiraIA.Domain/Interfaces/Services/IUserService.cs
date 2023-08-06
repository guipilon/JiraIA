using JiraIA.Domain.DTOs;

namespace JiraIA.Domain.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUser();

        Task<UserDTO> AddUser(UserDTO user);

        Task DeleteUser(string id);
    }
}
