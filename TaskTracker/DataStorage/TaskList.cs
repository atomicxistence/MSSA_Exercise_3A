using System;
using System.Collections.Generic;

namespace TaskrLibrary
{
	[Serializable]
	public class TaskList
    {
        public List<Page> Pages {get; private set;}

		public TaskList()
		{
			Pages = new List<Page>();
		}
    }
}