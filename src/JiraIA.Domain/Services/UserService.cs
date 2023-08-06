using AutoMapper;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces;
using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Interfaces.Services;
using JiraIA.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraIA.Domain.Services
{
    public class UserService : BaseService, IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork) 
            :base(unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<UserDTO> GetAllUser()
        {
            var users = _userRepository.GetAllUsers();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public async Task<UserDTO> AddUser(UserDTO user)
        {
            var newUser = _mapper.Map<User>(user);
            var userCreated = await _userRepository.InsertUser(newUser);

            if (!await CommitAsync())
            {
                return default;
            }

            return _mapper.Map<UserDTO>(userCreated);
        }

        public async Task DeleteUser(string id)
        {
            await _userRepository.DeleteUserAsync(id);

            await CommitAsync();
        }
    }
}
