namespace TaskTracker
{
	interface IFileManager
	{
		TaskList Load();

		void Save(TaskList taskList);
	}
}
