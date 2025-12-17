namespace DTO
{
    public class TaskItemDto
    {
        public TaskItemDto(
           int id, string title, string description, string status,
           DateTime deadline, int assigneeId, UserDto assignee
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
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } 
        public DateTime Deadline { get; set; }
        public int AssigneeId { get; set; }
        public UserDto Assignee { get; set; }
    }
}
