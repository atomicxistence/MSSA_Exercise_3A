using System;

namespace TaskTracker
{
	[Serializable]
	public class Task
    {
		public string Title {get; private set;}
		public DateTime TimeStamp { get; private set; }
        public bool IsActioned {get; private set;}
        public bool IsCompleted {get; private set;}

        public Task(string title)
        {
            Title = title;
			TimeStamp = DateTime.Now;
			IsActioned = false;
			IsCompleted = false;
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