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
			fileManager = new DummyFileManager();
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
			InsertTaskOnLastPage(task);
		}

		public void CopyTaskToEndOfList(Task task)
		{
			InsertTaskOnLastPage(task);
		} 

		private void InsertTaskOnLastPage(Task task)
		{
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
				var page = new Page();
				taskList.Pages.Add(page);
			}
		}

		public void SaveTaskList()
		{
			fileManager.Save(taskList);
		}
	}
}
