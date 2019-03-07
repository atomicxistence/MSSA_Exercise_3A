using System;

namespace TaskTracker
{
	[Serializable]
	public class Task
    {
		public string Title {get; set;}
		public DateTime TimeStamp { get; set; }
        public bool IsActioned {get; set;}
        public bool IsCompleted {get; set;}

		public Task() { }

        public Task(string title)
        {
            Title = title;
			TimeStamp = DateTime.Now;
			IsActioned = false;
			IsCompleted = false;
        }

		public Task(string title, DateTime timeStamp)
		{
			Title = title;
			TimeStamp = timeStamp;
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