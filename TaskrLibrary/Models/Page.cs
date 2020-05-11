using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskrLibrary.Models
{
	public class Page
    {
        public List<Task> Tasks {get;}
		public bool IsFull => Tasks.Count >= Global.PageSize;
		public bool IsFullyActioned => Tasks.Where(task => task.IsActioned).Count() >= Global.PageSize;

		public Page()
		{
			Tasks = new List<Task>();
		}
    }
}