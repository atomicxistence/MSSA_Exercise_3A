using System.Collections.Generic;
using System.Linq;

namespace TaskrLibrary.Models
{
	public class Page
    {
        public List<Task> Tasks {get;}
		public bool IsFull => Tasks.Count >= pageSize;
		public bool IsFullyActioned => Tasks.Where(task => task.IsActioned).Count() >= pageSize;
		private int pageSize;

		public Page(int pageSize)
		{
			this.pageSize = pageSize;
			Tasks = new List<Task>();
		}
    }
}