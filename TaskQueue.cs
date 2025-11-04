namespace Task_Priority_System;
public class TaskQueue
{
    private readonly Queue<TaskItem> _queue;

    public TaskQueue()
    {
        _queue = new Queue<TaskItem>();
    }

    public void AddTask(TaskItem task)
    {
        _queue.Enqueue(task);
        Console.WriteLine($"Added task: {task.Title}");
    }

    public TaskItem? ProcessNext()
    {
        if (_queue.Count == 0)
        {
            Console.WriteLine("No tasks to process");
            return null;
        }

        var task = _queue.Dequeue();
        Console.WriteLine($"Processing: {task.Title}");
        return task;
    }

    public TaskItem? Peek()
    {
        return _queue.Count > 0 ? _queue.Peek() : null;
    }

    public int Count => _queue.Count;
}
