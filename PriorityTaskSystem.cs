namespace Task_Priority_System;
using System;
using System.Collections.Generic;

public class PriorityTaskSystem
{
    private readonly PriorityQueue<PriorityTaskItem, (int priority, DateTime createdAt)> _priorityQueue;
    private readonly Stack<TaskAction> _undoStack;
    private readonly Stack<TaskAction> _redoStack;

    public PriorityTaskSystem()
    {
        // Lower priority number = higher priority
        _priorityQueue = new PriorityQueue<PriorityTaskItem, (int, DateTime)>();
        _undoStack = new Stack<TaskAction>();
        _redoStack = new Stack<TaskAction>();
    }

    public void AddTask(PriorityTaskItem task)
    {
        // Use tuple for priority: (priority level, creation time)
        // This ensures FIFO order for tasks with same priority
        _priorityQueue.Enqueue(task, ((int)task.Priority, task.CreatedAt));

        _undoStack.Push(new TaskAction
        {
            Type = ActionType.Add,
            Task = new TaskItem
            {
                Title = task.Title,
                CreatedAt = task.CreatedAt,
                Description = task.Description
            }
        });
        _redoStack.Clear();

        Console.WriteLine($"Added {task.Priority} priority task: {task.Title}");
    }

    public PriorityTaskItem ProcessNext()
    {
        if (_priorityQueue.Count == 0)
        {
            Console.WriteLine("No tasks to process");
            return null;
        }

        var task = _priorityQueue.Dequeue();
        Console.WriteLine($"Processing {task.Priority} priority task: {task.Title}");
        return task;
    }

    public PriorityTaskItem Peek()
    {
        return _priorityQueue.Count > 0 ? _priorityQueue.Peek() : null;
    }

    public int Count => _priorityQueue.Count;
}
