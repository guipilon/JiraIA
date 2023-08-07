using JiraIA.Domain.Interfaces.Repositories;
using JiraIA.Domain.Models;
using MongoDB.Driver;

namespace JiraIA.Infra.Repositories
{
    public class TaskRepository : BaseRepository<TaskModel>, ITaskRepository
    {
        public TaskRepository(JiraIAContext context, IClientSessionHandle clientSessionHandle)
            : base(context, clientSessionHandle) { }

        public async Task<TaskModel> CreateTask(TaskModel task)
        {
            await InsertAsync(task);

            return task;
        }

        public async Task<TaskModel> DeleteTask(string id)
        {
            var taskToBeDeleted = GetTaskById(id);

            await DeleteAsync(id);

            return taskToBeDeleted;
        }

        public IEnumerable<TaskModel> GetAllTasks()
        {
            return GetQueryble().ToList();
        }

        public TaskModel GetTaskById(string id)
        {
            return GetQueryble()
                .Where(x => x.Id == id)
                .First();
        }

        public IEnumerable<TaskModel> GetTaskByStatus(string status)
        {
            return GetQueryble()
                .Where(x => x.Status == status)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.IsFavorited)
                .ToList();
        }

        public IEnumerable<TaskModel> GetTaskByUser(string username)
        {
            return GetQueryble()
                .Where(x => x.AssignedTo == username)
                .ToList();
        }

        public async Task<TaskModel> UpdateTask(TaskModel task)
        {
            await UpdateAsync(task);

            return task;
        }
    }
}
