// ** BismiIllah Ar-Rahamaan Ar-Raheem ** \\
namespace DataAccess.Models
{
    public class User
    {
        // entity framework require a parameterless constructor
        public User() { }

        public User(
            int id, string fullName, string email,
            string role, string password,
            List<TaskItem> assignedTasks
        )
        {
            this.Id = id;
            this.FullName = fullName;
            this.Email = email;
            this.Role = role;
            this.Password = password;
            this.AssignedTasks = assignedTasks;
        }


        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Role: Admin, Manager, Member
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public List<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    }
}