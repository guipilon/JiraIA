using JiraIA.Domain.Models;

namespace JiraIA.Domain.Interfaces.Repositories
{
    public interface ITaskRepository : IBaseRepository<TaskModel>
    {
        IEnumerable<TaskModel> GetAllTasks();

        IEnumerable<TaskModel> GetTaskByStatus(string status);

        IEnumerable<TaskModel> GetTaskByUser(string username);

        TaskModel GetTaskById(string id);

        Task<TaskModel> UpdateTask(TaskModel task);

        Task<TaskModel> CreateTask(TaskModel task);

        Task<TaskModel> DeleteTask(string id);
    }
}
