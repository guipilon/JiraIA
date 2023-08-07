using JiraIA.Domain.DTOs;
using JiraIA.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JiraIA.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskDTO>> GetAllTasks()
        {
            var tasks = _taskService.GetAllTasks();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskDTO> GetTaskById([FromRoute] string id)
        {
            var task = _taskService.GetTaskById(id);

            if (task == null) return NotFound("Task not found");
            else return Ok(task);

        }

        [HttpGet("{status}")]
        public ActionResult<IEnumerable<TaskDTO>> GetTaskByStatus([FromRoute] string status)
        {
            var tasks = _taskService.GetTaskByStatus(status);

            if (!tasks.Any()) return NotFound($"No tasks with status {status}");
            else return Ok(tasks);

        }

        [HttpGet("{user}")]
        public ActionResult<IEnumerable<TaskDTO>> GetTaskByUser([FromRoute] string username)
        {
            var tasks = _taskService.GetTaskByUser(username);

            if (!tasks.Any()) return NotFound($"No tasks assigned to {username}");
            else return Ok(tasks);

        }

        [HttpPost]
        public async Task<ActionResult<TaskDTO>> CreateTask([FromBody] TaskDTO task)
        {
            var taskCreated = await _taskService.CreateTask(task);

            return Ok(taskCreated);
        }

        [HttpPut]
        public async Task<ActionResult<TaskDTO>> UpdateTask([FromBody] TaskDTO task)
        {
            var taskUpdated = await _taskService.UpdateTask(task);

            if (taskUpdated == null) return NotFound("Task not found");
            else return Ok(taskUpdated);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTask([FromRoute] string id)
        {
            await _taskService.DeleteTask(id);

            return Ok();
        }

    }
}
