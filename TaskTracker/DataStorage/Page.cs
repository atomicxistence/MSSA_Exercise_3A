using System;
using System.Collections.Generic;

namespace TaskTracker
{
	[Serializable]
	public class Page
    {
        public List<Task> Tasks {get; private set;}
		public bool IsFull
		{
			get
			{
				return Tasks.Count >= Global.PageSize;
			}
		}
    }
}