using Task_Priority_System;

// Queue-based Task Management System
var taskSystem = new TaskQueue();

taskSystem.AddTask(new TaskItem
{
    Title = "Fix login bug",
    CreatedAt = DateTime.Now,
    Description = "Users can't log in with special characters"
});

taskSystem.AddTask(new TaskItem
{
    Title = "Update documentation",
    CreatedAt = DateTime.Now,
    Description = "Add new API endpoints to docs"
});

taskSystem.AddTask(new TaskItem
{
    Title = "Review pull request",
    CreatedAt = DateTime.Now,
    Description = "PR #142 needs review"
});

taskSystem.ProcessNext();  // Processes "Fix login bug"
taskSystem.ProcessNext();  // Processes "Update documentation"

// Queue-based Task Management with Undo/Redo
var system = new TaskSystemWithUndoRedo();

system.AddTask(new TaskItem { Title = "Task A", CreatedAt = DateTime.Now });
system.AddTask(new TaskItem { Title = "Task B", CreatedAt = DateTime.Now });
system.Undo();  // Removes Task B
system.Redo();  // Re-adds Task B
