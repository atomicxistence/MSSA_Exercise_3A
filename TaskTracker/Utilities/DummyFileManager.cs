using System;

namespace TaskrLibrary
{
	public class DummyFileManager : IFileManager
	{
		TaskList dummyTaskList;

		public DummyFileManager()
		{
			dummyTaskList = new TaskList();
			var dummyPage = new Page();
			dummyTaskList.Pages.Add(dummyPage);

			var task1 = new Task("Go for a run");
			var task2 = new Task("Take a shower");
			var task3 = new Task("Change the oil in the car");
			task3.Actioned();
			var task4 = new Task("Make the best video game the world has ever seen");

			dummyTaskList.Pages[0].Tasks.Add(task1);
			dummyTaskList.Pages[0].Tasks.Add(task2);
			dummyTaskList.Pages[0].Tasks.Add(task3);
			dummyTaskList.Pages[0].Tasks.Add(task4);
			dummyTaskList.Pages[0].Tasks.Add(task3);
		}

		public TaskList Load() => dummyTaskList;

		public void Save(TaskList taskList)
		{
			throw new NotImplementedException();
		}
	}
}
