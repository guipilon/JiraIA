using JiraIA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIA.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IEnumerable<User> GetAllUsers();

        User GetById(string id);

        IEnumerable<User> GetByRole(string role);

        Task<User> InsertUser(string username, string password, string role);

        Task<User> UpdateUserName(string id, string newUserName);

        Task DeleteUserAsync(string id);

    }
}
