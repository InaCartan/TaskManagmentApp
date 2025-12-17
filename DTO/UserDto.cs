using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UserDto
    {
        public UserDto(
            int id, string fullName, string role, string email, 
            string password, List<TaskItemDto> assignedTasks
        )
        {
            this.Id = id;
            this.FullName = fullName;
            this.Role = role;
            this.Email = email;
            this.Password = password;
            this.AssignedTasks = assignedTasks ?? new List<TaskItemDto>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // Role: Admin, Manager, Member
        public string Email { get; set; }
        public string Password { get; set; }
        public List<TaskItemDto> AssignedTasks { get; set; } = new List<TaskItemDto>();
    }
}
