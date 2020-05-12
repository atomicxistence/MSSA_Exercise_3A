using System.Collections.Generic;
using System.Linq;

namespace TaskrLibrary.Models
{
	public class Page
    {
        public List<Task> Tasks {get;}
		public bool IsFull => Tasks.Count >= NewTaskr.PageSize;
		public bool IsFullyActioned => Tasks.Where(task => task.IsActioned).Count() >= NewTaskr.PageSize;

		public Page()
		{
			Tasks = new List<Task>();
		}
    }
}