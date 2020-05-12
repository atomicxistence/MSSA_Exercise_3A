using System;

namespace TaskrLibrary.Models
{
	public class Task
    {
        public int TaskId { get; set;}
		public string Title { get; set;}
        public DateTime CreatedOn { get; set;}
        public bool IsActioned { get; set;}
        public bool IsCompleted { get; set;}
    }
}