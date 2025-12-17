using DTO;
using DataAccess.Models;

namespace DataAccess.Mapper
{
    internal class UserMapper
    {
        // Model -> DTO (shallow: no nested tasks)
        public static UserDto MapToDtoShallow(User user)
        {
            return new UserDto(
                user.Id,
                user.FullName,
                user.Role,
                user.Email,
                user.Password,
                new List<TaskItemDto>() // shallow: do not include assigned tasks to prevent recursion
            );
        }

        // Model -> DTO (full): map user's tasks using task mapper that uses shallow assignees
        public static UserDto MapToDto(User user)
        {
            return new UserDto(
                user.Id,
                user.FullName,
                user.Role,
                user.Email,
                user.Password,
                TaskMapper.MapListToDtoShallow(user.AssignedTasks)
            );
        }

        public static List<UserDto> MapListToDto(List<User> userList)
        {
            var userDtos = new List<UserDto>();
            foreach (User user in userList)
            {
                userDtos.Add(MapToDto(user));
            }
            return userDtos;
        }

        // DTO -> Model
        public static User MapToModel(UserDto user)
        {
            var userModel = new User
            {
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Password = user.Password,
                AssignedTasks = new List<TaskItem>()
            };

            var taskModels = TaskMapper.MapListToModel(user.AssignedTasks ?? new List<TaskItemDto>());
            foreach (var t in taskModels)
            {
                // attach the in-memory user so EF will insert tasks with correct FK when creating a new user with tasks
                t.Assignee = userModel;
                t.AssigneeId = 0;
                userModel.AssignedTasks.Add(t);
            }

            userModel.Id = user.Id;
            return userModel;
        }
    }
}
