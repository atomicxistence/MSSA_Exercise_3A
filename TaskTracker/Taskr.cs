using System;
using System.IO;

namespace TaskTracker
{
	public class Taskr
	{
		private TaskList taskList;
		private IFileManager fileManager;

		public Taskr()
		{
			fileManager = new XMLFileManager();
			LoadTaskList();
		}

		public Page GetPage(int pageIndex)
		{
			return taskList.Pages[pageIndex];
		}

		public int GetTotalPageCount() => taskList.Pages.Count;

		public void AddTask(string taskTitle)
		{
			var task = new Task(taskTitle);

			if (taskList.Pages[taskList.Pages.Count - 1].IsFull)
			{
				var newPage = new Page();
				newPage.Tasks.Add(task);
				taskList.Pages.Add(newPage);
			}
			else
			{
				taskList.Pages[taskList.Pages.Count - 1].Tasks.Add(task);
			}
		}

		private void LoadTaskList()
		{
			try
			{
				taskList = fileManager.Load();
			}
			catch (FileNotFoundException)
			{
				taskList = new TaskList();
			}
		}

		public void SaveTaskList()
		{
			fileManager.Save(taskList);
		}
	}
}
