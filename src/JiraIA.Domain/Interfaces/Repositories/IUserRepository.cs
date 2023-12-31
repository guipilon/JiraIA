﻿using JiraIA.Domain.Models;

namespace JiraIA.Domain.Interfaces.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        IEnumerable<User> GetAllUsers();

        User GetById(string id);

        IEnumerable<User> GetByRole(string role);

        Task<User> InsertUser(User user);

        Task<User> UpdateUserName(string id, string newUserName);

        Task DeleteUserAsync(string id);

    }
}
