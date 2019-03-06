using System;
using TaskTracker;

namespace ConsoleUI
{
	public class Display
	{
		#region DisplayVariables
		private int currentWindowWidth;
		private int currentWindowHeight;
		private int centeredWindowTop;
		private int centeredWindowLeft;
		private int widthMin = 80;
		private int heightMin = Global.PageSize + 3;
		private int pageTopOffset;
		private int pageLeftOffset = 2;

		private string prompt = "🡅 🡇 : Tasks | 🡄 🡆 : Pages | N  : New Task  | Esc : Quit";

		private Selection currentSelection;
		private Selection nextSelection;
		private Page currentPage;

		private ConsoleColor colorBorder = ConsoleColor.DarkGray;
		private ConsoleColor colorTitleBackground = ConsoleColor.Blue;
		private ConsoleColor colorTitle = ConsoleColor.Black;
		private ConsoleColor colorTaskActioned = ConsoleColor.DarkGray;
		private ConsoleColor colorTaskSelectedBG = ConsoleColor.DarkYellow;
		private ConsoleColor colorTaskSelectedFG = ConsoleColor.Black;

		private string selectionIndicator = " ► ";
		private string[] title = new string[]
			{"                                         ",
			 "████████╗ █████╗ ███████╗██╗  ██╗██████╗ ", 
			 "╚══██╔══╝██╔══██╗██╔════╝██║ ██╔╝██╔══██╗",
			 "   ██║   ███████║███████╗█████╔╝ ██████╔╝",
			 "   ██║   ██╔══██║╚════██║██╔═██╗ ██╔══██╗",
			 "   ██║   ██║  ██║███████║██║  ██╗██║  ██║",
			 "   ╚═╝   ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝",
			 "                                         "};
		#endregion

		public void Initialize(Page currentPage)
		{
			this.currentPage = currentPage;
			heightMin += title.Length;
			SetInitialWindowSize();
			pageTopOffset = centeredWindowTop + title.Length + 2;
			Console.CursorVisible = false;
			currentSelection = new Selection(0, 0);
			nextSelection = new Selection(0, 0);
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
				PrintSelections();
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
			prompt = "Create a New Task";
			// print new task border
			// print prompt
		}

		private void SetInitialWindowSize()
		{
			Console.SetWindowSize(widthMin, heightMin);
			currentWindowWidth = Console.WindowWidth;
			currentWindowHeight = Console.WindowHeight;

			SetCenteredWindowEdges();
		}

		private void SetCenteredWindowEdges()
		{
			centeredWindowLeft = (currentWindowWidth / 2) - (widthMin / 2);
			centeredWindowTop = (currentWindowHeight / 2) - (heightMin / 2);
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
				currentWindowHeight = Console.WindowHeight;
			}

			return hasChanged;
		}

		private void CompleteRefresh()
		{
			Console.Clear();
			SetCenteredWindowEdges();
			PrintTitle();
			PrintPrompt();
			PrintBorder();
			PrintPageContents();
			PrintSelections();
		}

		private void PrintSelections()
		{
			var currentTask = currentPage.Tasks[currentSelection.ItemIndex];
			var nextTask = currentPage.Tasks[nextSelection.ItemIndex];

			PrintCurrentSelection(currentTask);
			PrintNextSelection(nextTask);

		}


		private void PrintCurrentSelection(Task currentTask)
		{
			Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset,
												  pageTopOffset + currentSelection.ItemIndex);
			PrintEmptySpaceFill(widthMin - pageLeftOffset);

			if (currentTask.IsActioned)
			{
				Console.ForegroundColor = colorTaskActioned;
			}

			Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset,
									  pageTopOffset + currentSelection.ItemIndex);
			Console.Write(currentTask.Title);
		}

		private void PrintNextSelection(Task nextTask)
		{
			Console.ForegroundColor = nextTask.IsActioned ? colorTaskActioned : colorTaskSelectedFG;
			Console.BackgroundColor = colorTaskSelectedBG;
			Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset,
									  pageTopOffset + nextSelection.ItemIndex);
			Console.Write($"{selectionIndicator}{nextTask.Title}");
			PrintEmptySpaceFill(widthMin - pageLeftOffset - nextTask.Title.Length - selectionIndicator.Length);
			Console.ResetColor();
		}

		private void PrintTitle()
		{
			Console.SetCursorPosition(centeredWindowLeft, centeredWindowTop);
			Console.ForegroundColor = colorTitle;
			Console.BackgroundColor = colorTitleBackground;
			// Write Background Color
			for (int i = 0; i < title.Length; i++)
			{
				PrintEmptySpaceFill(widthMin);
				Console.Write("\n");
			}
			// Write Centered Title
			for (int i = 0; i < title.Length; i++)
			{
				Console.SetCursorPosition(centeredWindowLeft + (title[i].Length / 2), centeredWindowTop + i);
				Console.Write(title[i]);
			}
			Console.ResetColor();
		}

		private void PrintPrompt()
		{
			Console.SetCursorPosition(centeredWindowLeft + (prompt.Length / 2), centeredWindowTop + title.Length);
			Console.Write(prompt);
		}

		private void PrintBorder()
		{
			// is border needed?
		}

		private void PrintPageContents()
		{
			for (int i = 0; i < currentPage.Tasks.Count; i++)
			{
				Console.SetCursorPosition(centeredWindowLeft + pageLeftOffset, pageTopOffset + i);
				Console.Write(currentPage.Tasks[i].Title);
			}
		}

		private void PrintEmptySpaceFill(int emptySpace)
		{
			for (int i = 0; i < emptySpace; i++)
			{
				Console.Write(" ");
			}
		}
	}
}
