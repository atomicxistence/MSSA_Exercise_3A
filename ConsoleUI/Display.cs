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

		private string prompt = "🡅 🡇 : Tasks | 🡄 🡆 : Pages | N  : New Task  | Esc : Quit";

		private Selection currentSelection;
		private Selection nextSelection;
		private Page currentPage;

		private ConsoleColor colorBorder = ConsoleColor.DarkGray;
		private ConsoleColor colorTitleBackground = ConsoleColor.Gray;
		private ConsoleColor colorTitle = ConsoleColor.Black;
		private ConsoleColor colorTaskActioned = ConsoleColor.DarkGray;
		private ConsoleColor colorTaskSelectedBG = ConsoleColor.White;
		private ConsoleColor colorTaskSelectedFG = ConsoleColor.Black;

		public Display()
		{
			Initialize();
		}

		public void Initialize()
		{
			SetWindowSize();
			Console.CursorVisible = false;
			currentSelection = new Selection(0, 0);
			CompleteRefresh();
		}

		public void Refresh(Page currentPage, Selection nextSelection)
		{
			this.currentPage = currentPage;
			this.nextSelection = nextSelection;

			if (WindowSizeHasChanged())
			{
				CompleteRefresh();
			}

			if (currentSelection.ItemIndex != nextSelection.ItemIndex)
			{
				PrintSelections();
				currentSelection = new Selection(nextSelection.ItemIndex, nextSelection.PageIndex);
			}

			if (currentSelection.PageIndex != nextSelection.PageIndex)
			{
				PrintPageContents();
				currentSelection = new Selection(nextSelection.ItemIndex, nextSelection.PageIndex);
			}
		}

		public void SubMenuRefresh(Menu subMenu, Selection newSelection)
		{
			// print submenu border
			// print submenu items
		}

		public void NewTaskEntry()
		{
			var prompt = "Create a New Task";
			// print new task border
			// print prompt
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
			PrintTitle();
			PrintPrompt();
			PrintBorder();
			PrintPageContents();
		}

		private void PrintSelections()
		{
			// print previous task with default colors
			// print next task with highlight colors
		}

		private void PrintPageContents()
		{
			// print the entire contents of the page
		}

		private void PrintTitle()
		{

		}

		private void PrintPrompt()
		{

		}

		private void PrintBorder()
		{

		}
	}
}
