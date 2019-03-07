using System;
using System.Collections.Generic;

namespace TaskrLibrary
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
		public bool IsFullyActioned
		{
			get
			{
				int actionedTasksCount = 0;

				for (int i = 0; i < Global.PageSize; i++)
				{
					actionedTasksCount += Tasks[i].IsActioned ? 1 : 0;
				}

				return actionedTasksCount >= Global.PageSize;
			}
		}

		public Page()
		{
			Tasks = new List<Task>();
		}
    }
}