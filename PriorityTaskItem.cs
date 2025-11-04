namespace Task_Priority_System;
public class PriorityTaskItem
{
    public string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    public TaskPriority Priority { get; set; }
}
