namespace Task_Priority_System;

public class TaskSystemWithUndoRedo
{
    private readonly Queue<TaskItem> _queue;
    private readonly Stack<TaskAction> _undoStack;
    private readonly Stack<TaskAction> _redoStack;

    public TaskSystemWithUndoRedo()
    {
        _queue = new Queue<TaskItem>();
        _undoStack = new Stack<TaskAction>();
        _redoStack = new Stack<TaskAction>();
    }

    public void AddTask(TaskItem task)
    {
        _queue.Enqueue(task);

        _undoStack.Push(new TaskAction
        {
            Type = ActionType.Add,
            Task = task
        });

        _redoStack.Clear(); // Clear redo history on new action

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

        _undoStack.Push(new TaskAction
        {
            Type = ActionType.Process,
            Task = task
        });

        _redoStack.Clear();

        Console.WriteLine($"Processed: {task.Title}");

        return task;
    }

    public void Undo()
    {
        if (_undoStack.Count == 0)
        {
            Console.WriteLine("Nothing to undo");
            return;
        }

        var action = _undoStack.Pop();

        _redoStack.Push(action);

        switch (action.Type)
        {
            case ActionType.Add:
                // Remove the task from queue (convert to list for removal)
                var tempList = new List<TaskItem>(_queue);
                tempList.Remove(action.Task);
                _queue.Clear();
                foreach (var item in tempList)
                    _queue.Enqueue(item);
                Console.WriteLine($"Undid adding: {action.Task.Title}");
                break;

            case ActionType.Process:
                // Re-add the task to the front
                var allTasks = _queue.ToList();
                _queue.Clear();
                _queue.Enqueue(action.Task);
                foreach (var item in allTasks)
                    _queue.Enqueue(item);
                Console.WriteLine($"Undid processing: {action.Task.Title}");
                break;
        }
    }

    public void Redo()
    {
        if (_redoStack.Count == 0)
        {
            Console.WriteLine("Nothing to redo");
            return;
        }

        var action = _redoStack.Pop();

        _undoStack.Push(action);

        switch (action.Type)
        {
            case ActionType.Add:
                _queue.Enqueue(action.Task);
                Console.WriteLine($"Redid adding: {action.Task.Title}");
                break;

            case ActionType.Process:
                _queue.Dequeue();
                Console.WriteLine($"Redid processing: {action.Task.Title}");
                break;
        }
    }
}