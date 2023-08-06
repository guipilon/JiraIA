using JiraIA.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIA.Domain.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUser();

        Task<UserDTO> AddUser(UserDTO user);

        Task DeleteUser(string id);
    }
}
