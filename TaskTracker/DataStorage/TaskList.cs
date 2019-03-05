using System;
using System.Collections.Generic;

namespace TaskTracker
{
	[Serializable]
	public class TaskList
    {
        public List<Page> Pages {get; private set;}
    }
}