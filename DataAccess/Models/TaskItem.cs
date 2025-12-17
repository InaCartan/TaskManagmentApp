namespace DataAccess.Models
{
    public class TaskItem
    {
        // entity framework  require a parameterless constructor
        public TaskItem() { }

        public TaskItem(
            int id, string title, string description, TStatus status,
            DateTime deadline, int assigneeId, User assignee
        )
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Status = status;
            this.Deadline = deadline;
            this.AssigneeId = assigneeId;
            this.Assignee = assignee;
        }

        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TStatus Status { get; set; } = TStatus.Todo;
        public DateTime Deadline { get; set; }
        public int AssigneeId { get; set; }
        public User Assignee { get; set; } = null!;
    }
}
