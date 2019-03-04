using System;

namespace TaskTracker
{
    public class Task
    {
        public string Title {get; private set;}
        public bool IsActioned {get; private set;}
        public bool IsCompleted {get; private set;}

        public Task(string title)
        {
            Title = title;
        }

        public void Actioned()
        {
            IsActioned = true;
        }

        public void Completed()
        {
            IsCompleted = true;
        }
    }
}