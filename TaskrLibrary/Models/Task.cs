using System;

namespace TaskrLibrary.Models
{
	public class Task : IEquatable<Task>
    {
        public int TaskId { get; set;}
		public string Title { get; set;}
        public DateTime CreatedOn { get; set;}
        public bool IsActioned { get; set;}
        public bool IsCompleted { get; set;}

        public bool Equals(Task other) =>
            other == null ? false : TaskId == other.TaskId;
    }
}