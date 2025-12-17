using DTO;
using DataAccess.Repositories;

namespace BusinessLogic
{
    public class TaskItemBLL
    {
        private TaskRepository taskRepo;

        public TaskItemBLL(TaskRepository taskR)
        {
            taskRepo = taskR;
        }

        public List<TaskItemDto> GetAllTasks()
        {
            return taskRepo.GetAllTasks();
        }

        public TaskItemDto GetTaskById(int id)
        {
            return taskRepo.GetTaskById(id);
        }

        public List<TaskItemDto> GetTaskByAssignee(int userId)
        {
            return taskRepo.GetTaskByAssignee(userId);
        }

        
        public TaskItemDto AddTask(TaskItemDto task)
        {
            return taskRepo.AddTask(task);
        }

        public void UpdateTask(TaskItemDto task)
        {
            taskRepo.UpdateTask(task);
        }

        public void DeleteTask(int id)
        {
            taskRepo.DeleteTask(id);
        }
    }
}
