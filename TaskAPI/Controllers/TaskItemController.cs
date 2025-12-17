using DTO;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly TaskItemBLL taskItem;

        public TaskItemController(TaskItemBLL taskItem)
        {
            this.taskItem = taskItem;
        }

        // GET: api/TaskItem
        [HttpGet]
        public ActionResult<List<TaskItemDto>> GetAll()
        {
            var items = taskItem.GetAllTasks();
            return Ok(items);
        }

        // GET: api/TaskItem/{id}
        [HttpGet("{id}")]
        public ActionResult<TaskItemDto> GetTaskById(int id)
        {
            var task = taskItem.GetTaskById(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // GET: api/TaskItem/assignee/{userId}
        [HttpGet("assignee/{userId}")]
        public ActionResult<List<TaskItemDto>> GetTaskByAssignee(int userId)
        {
            var items = taskItem.GetTaskByAssignee(userId);
            return Ok(items);
        }

        // POST: api/TaskItem
        [HttpPost]
        public ActionResult<TaskItemDto> AddTask([FromBody] TaskItemDto task)
        {
            if (task == null) return BadRequest();

            task.Assignee ??= new UserDto(task.AssigneeId, string.Empty, string.Empty, string.Empty, string.Empty, new List<TaskItemDto>());

            var registeredTask = taskItem.AddTask(task);

            return CreatedAtAction(nameof(GetTaskById), new { id = registeredTask.Id }, registeredTask);
        }

        // PUT: api/TaskItem/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskItemDto task)
        {
            if (task == null || id != task.Id) return BadRequest();

            var t = taskItem.GetTaskById(id);
            if (t == null) return NotFound();

            taskItem.UpdateTask(task);
            return NoContent();
        }

        // DELETE: api/TaskItem/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var t = taskItem.GetTaskById(id);
            if (t == null) return NotFound();

            taskItem.DeleteTask(id);
            return NoContent();
        }
    }
}
