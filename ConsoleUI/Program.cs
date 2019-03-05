using TaskTracker;

namespace ConsoleUI
{
	class Program
	{
		static void Main(string[] args)
		{
			var taskr = new Taskr();
			var display = new Display();
			var currentSelection = new Selection(itemIndex: 0, pageIndex: 0);

			while (true)
			{
				// display current page of tasks
				display.Refresh(taskr.GetPage(currentSelection.PageIndex));
				// user task selection
					// task selection submenu
					// next page of tasks
					// exit & save
			}
		}
	}
}
