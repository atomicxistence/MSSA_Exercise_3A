namespace TaskrLibrary
{
	interface IFileManager
	{
		TaskList Load();

		void Save(TaskList taskList);
	}
}
