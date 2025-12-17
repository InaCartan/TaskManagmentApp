using DTO;
using DataAccess.Models;

namespace DataAccess.Mapper
{
    internal class TaskMapper
    {
        // Model -> DTO (shallow assignee to avoid circular recursion)
        public static TaskItemDto MapToDtoShallow(TaskItem task)
        {
            return new TaskItemDto(
                task.Id,
                task.Title,
                task.Description,
                task.Status.ToString(),
                task.Deadline,
                task.AssigneeId,
                UserMapper.MapToDtoShallow(task.Assignee) // shallow user: no nested tasks
            );
        }

        // Model -> DTO (full) - uses shallow assignee mapping to break cycles
        public static TaskItemDto MapToDto(TaskItem task)
        {
            return MapToDtoShallow(task);
        }

        // List<Model> -> DTO (shallow)
        public static List<TaskItemDto> MapListToDtoShallow(List<TaskItem> taskItemList)
        {
            var taskItemsDtos = new List<TaskItemDto>();
            foreach (TaskItem taskItem in taskItemList)
            {
                taskItemsDtos.Add(MapToDtoShallow(taskItem));
            }
            return taskItemsDtos;
        }

        // List<Model> -> DTO (full)
        public static List<TaskItemDto> MapListToDto(List<TaskItem> taskItemList)
        {
            return MapListToDtoShallow(taskItemList);
        }

        // DTO -> Model
        public static TaskItem MapToModel(TaskItemDto taskDto)
        {
            // Create model without constructing/attaching a User instance.
            // Only set AssigneeId so EF will link to an existing user by FK, or let UserMapper attach when creating a new user with tasks.
            return new TaskItem
            {
                Title = taskDto.Title,
                Description = taskDto.Description,
                Status = Enum.Parse<TStatus>(taskDto.Status),
                Deadline = taskDto.Deadline,
                AssigneeId = taskDto.AssigneeId
            };
        }

        // List<DTO> -> Model
        public static List<TaskItem> MapListToModel(List<TaskItemDto> taskItemList)
        {
            var taskItems = new List<TaskItem>();
            foreach (TaskItemDto t in taskItemList)
            {
                taskItems.Add(MapToModel(t));
            }
            return taskItems;
        }
    }
}
