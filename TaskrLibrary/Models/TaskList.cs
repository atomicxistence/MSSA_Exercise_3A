using System;
using System.Collections.Generic;

namespace TaskrLibrary.Models
{
	public class TaskList
    {
        public List<Page> Pages {get; private set;}

		public TaskList()
		{
			Pages = new List<Page>();
		}
    }
}