using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIA.Infra.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(JiraIAContext context, IClientSessionHandle clientSessionHandle)
            : base(context, clientSessionHandle) { }

        public async Task DeleteUserAsync(string id)
        {
            await DeleteAsync(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return GetQueryble().ToList();
        }

        public User GetById(string id)
        {
            return GetQueryble()
                .Where(x => x.Id == id)
                .First();
        }

        public IEnumerable<User> GetByRole(string role)
        {
            return GetQueryble()
                .Where(x => x.Role == role)
                .ToList();
        }

        public async Task<User> InsertUser(User user)
        {
            await InsertAsync(user);

            return user;
        }

        public async Task<User> UpdateUserName(string id, string newUserName)
        {
            var currUser = GetById(id);

            currUser.UserName = newUserName;

            await UpdateAsync(currUser);

            return currUser;
        }
    }
}
