namespace TaskTracker
{
	interface IFileManager
	{
		TaskList Load();

		bool Save(TaskList taskList);
	}
}
