using System;
using TaskTracker;

namespace ConsoleUI
{
	public class Display
	{
		private int currentWindowWidth;
		private int currentWindowHeight;
		private int widthMin = 80;
		private int heightMin = Global.PageSize + 10;

		public Display()
		{
			Initialize();
		}

		public void Initialize()
		{
			SetWindowSize();
			Console.CursorVisible = false;
			
		}

		public void Refresh(Page currentPage)
		{
			if (WindowSizeHasChanged())
			{
				CompleteRefresh();
			}

			// print effected tasks
		}

		private void SetWindowSize()
		{
			Console.SetWindowSize(widthMin, heightMin);
			currentWindowWidth = Console.WindowWidth;
			currentWindowHeight = Console.WindowHeight;
		}

		private bool WindowSizeHasChanged()
		{
			bool hasChanged = false;

			while (Console.WindowWidth < widthMin || Console.WindowHeight < heightMin)
			{
				hasChanged = true;
				Console.Clear();
				Console.WriteLine($"Please adjust your window size to at least 80 by {heightMin}. Press any key to continue.");
				Console.ReadKey();
			}

			if (Console.WindowWidth != currentWindowWidth || Console.WindowHeight != currentWindowHeight)
			{
				hasChanged = true;
				currentWindowWidth = Console.WindowWidth;
				currentWindowWidth = Console.WindowHeight;
			}

			return hasChanged;
		}

		private void CompleteRefresh()
		{
			Console.Clear();
			// print title
			// print subtitle
			// print border
			// print list
		}
	}
}
