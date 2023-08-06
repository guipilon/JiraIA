using AutoMapper;
using JiraIA.Domain.DTOs;
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
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper) 
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public IEnumerable<UserDTO> GetAllUser()
        {
            var users = _userRepository.GetAllUsers();
            return _mapper.Map<List<UserDTO>>(users);
        }
    }
}
