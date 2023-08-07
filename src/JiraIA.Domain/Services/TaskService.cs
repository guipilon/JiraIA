using AutoMapper;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces;
using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Interfaces.Services;
using JiraIA.Domain.Models;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace JiraIA.Domain.Services
{
    public class TaskService : BaseService, ITaskService
    {
        private ITaskRepository _taskRepository;
        private IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper, IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public IEnumerable<TaskDTO> GetAllTasks()
        {
            var tasks = _taskRepository.GetAllTasks();
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public IEnumerable<TaskDTO> GetTaskByStatus(string status)
        {
            var tasks = _taskRepository.GetTaskByStatus(status);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public IEnumerable<TaskDTO> GetTaskByUser(string username)
        {
            var tasks = _taskRepository.GetTaskByUser(username);
            return _mapper.Map<List<TaskDTO>>(tasks);
        }

        public TaskDTO GetTaskById(string id)
        {
            var task = _taskRepository.GetTaskById(id);
            return _mapper.Map<TaskDTO>(task);
        }

        public async Task<TaskDTO> UpdateTask(TaskDTO task)
        {
            var taskToBeUpdated = _mapper.Map<TaskModel>(task);
            var taskUpdated = await _taskRepository.UpdateTask(taskToBeUpdated);

            if (!await CommitAsync())
            {
                return default;
            }

            return _mapper.Map<TaskDTO>(taskUpdated);
        }

        public async Task<TaskDTO> CreateTask(TaskDTO task)
        {
            var newTask = _mapper.Map<TaskModel>(task);
            var taskCreated = await _taskRepository.CreateTask(newTask);

            if (!await CommitAsync())
            {
                return default;
            }

            return _mapper.Map<TaskDTO>(taskCreated);
        }

        public async Task<TaskDTO> DeleteTask(string id)
        {
            var taskToBeDeleted = _mapper.Map<TaskModel>(id);
            var taskDeleted = await _taskRepository.DeleteTask(taskToBeDeleted);

            if (!await CommitAsync())
            {
                return default;
            }

            return _mapper.Map<TaskDTO>(taskDeleted);
        }
    }
}
