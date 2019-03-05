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

		public void AddTask(string taskTitle)
		{
			var task = new Task(taskTitle);

			if (taskList.Pages[taskList.Pages.Count].IsFull)
			{
				var newPage = new Page();
				newPage.Tasks.Add(task);
				taskList.Pages.Add(newPage);
			}
			else
			{
				taskList.Pages[taskList.Pages.Count].Tasks.Add(task);
			}
		}

		private void LoadTaskList()
		{
			taskList = fileManager.Load();
		}

		public void SaveTaskList()
		{
			fileManager.Save(taskList);
		}
	}
}
