using JiraIA.Domain.DTOs;

namespace JiraIA.Domain.Interfaces.Services
{
    public interface ITaskService
    {
        IEnumerable<TaskDTO> GetAllTasks();

        IEnumerable<TaskDTO> GetTaskByStatus(string status);

        IEnumerable<TaskDTO> GetTaskByUser(string username);

        TaskDTO GetTaskById(string id);

        Task<TaskDTO> UpdateTask(TaskDTO task);

        Task<TaskDTO> CreateTask(TaskDTO user);

        Task<TaskDTO> DeleteTask(string id);
    }
}
