using DataAccess.Context;
using DataAccess.Mapper;
using DataAccess.Models;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly AppDbContext db;

        public UserRepository(AppDbContext db)
        {
            this.db = db;
        }

        public List<UserDto> GetAllUsers()
        {
            var users = db.Users
                          .Include(u => u.AssignedTasks)
                          .ToList();

            return UserMapper.MapListToDto(users);
        }

        public UserDto? GetUserById(int id)
        {
            var u = db.Users
                      .Include(u => u.AssignedTasks)
                      .FirstOrDefault(u => u.Id == id);

            if (u == null) return null;
            return UserMapper.MapToDto(u);
        }

        public UserDto? GetUserByEmail(string email)
        {
            var user = db.Users
                         .Include(u => u.AssignedTasks)
                         .FirstOrDefault(u => u.Email == email);

            if (user == null) return null;
            return UserMapper.MapToDto(user);
        }

        public void AddUser(UserDto user)
        {
            var model = UserMapper.MapToModel(user);
            db.Users.Add(model);
            db.SaveChanges();
        }

        public void UpdateUser(UserDto user)
        {
            
            var existing = db.Users
                             .Include(u => u.AssignedTasks)
                             .FirstOrDefault(u => u.Id == user.Id);

            if (existing == null)
            {
                // Not found -> create new (upsert behavior)
                var created = UserMapper.MapToModel(user);
                db.Users.Add(created);
                db.SaveChanges();
                return;
            }

            
            existing.FullName = user.FullName;
            existing.Email = user.Email;
            existing.Role = user.Role;
            existing.Password = user.Password;

            

            db.SaveChanges();
        }

        public void DeleteUser(int id)
        {
           
            var user = db.Users
                           .Include(u => u.AssignedTasks)
                           .FirstOrDefault(u => u.Id == id);

            if (user == null) return;

            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
