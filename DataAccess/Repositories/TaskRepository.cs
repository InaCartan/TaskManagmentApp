using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Mapper;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess.Repositories
{
    public class TaskRepository
    {
        private readonly AppDbContext db;

        public TaskRepository(AppDbContext db)
        {
            this.db = db;
        }
        
        public List<TaskItemDto> GetAllTasks()
        {
            var task = db.Tasks.Include(t => t.Assignee).ToList();
            return TaskMapper.MapListToDto(task);
        }

        public TaskItemDto? GetTaskById(int id)
        {
            var task = db.Tasks.Include(t => t.Assignee).FirstOrDefault(t => t.Id == id);
            if (task == null) return null;
            return TaskMapper.MapToDto(task);
        }

        public List<TaskItemDto> GetTaskByAssignee(int userId)
        {
            var task = db.Tasks.Include(t => t.Assignee).Where(t => t.AssigneeId == userId).ToList();
            return TaskMapper.MapListToDto(task);
        }

        
        public TaskItemDto AddTask(TaskItemDto task)
        {
            var model = TaskMapper.MapToModel(task);
            db.Tasks.Add(model);
            db.SaveChanges();
            db.Entry(model).Reference(m => m.Assignee).Load();

            return TaskMapper.MapToDto(model);
        }

        public void UpdateTask(TaskItemDto task)
        {
            
            var model = TaskMapper.MapToModel(task);
            model.Id = task.Id;

            db.Tasks.Attach(model);
            db.Entry(model).Property(p => p.Title).IsModified = true;
            db.Entry(model).Property(p => p.Description).IsModified = true;
            db.Entry(model).Property(p => p.Status).IsModified = true;
            db.Entry(model).Property(p => p.Deadline).IsModified = true;
            db.Entry(model).Property(p => p.AssigneeId).IsModified = true;

            db.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            var task = db.Tasks.Find(id);
            if (task == null) return;
            db.Tasks.Remove(task);
            db.SaveChanges();
        }
    }
}
