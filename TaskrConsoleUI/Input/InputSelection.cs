using System;
using TaskrConsoleUI.Common;

namespace TaskrConsoleUI.Input
{
	public class InputSelection
	{
		public ActionType Selection(ConsoleKey input, SelectionState state) =>
			state switch
			{
                SelectionState.MainSelection => 		TaskSelect(input),
                SelectionState.SubMenuSelection => 		ActionSelect(input),
                SelectionState.ConfirmationSelection => ConfirmationSelect(input),
				_=> 									ActionType.Invalid
			};

		private ActionType TaskSelect(ConsoleKey input) =>
			input switch
			{
                ConsoleKey.Enter => 	ActionType.Select,
                ConsoleKey.N =>			ActionType.NewTask,
                ConsoleKey.UpArrow => 	ActionType.PreviousItem,
                ConsoleKey.DownArrow => ActionType.NextItem,
                ConsoleKey.RightArrow => ActionType.NextPage,
                ConsoleKey.LeftArrow => ActionType.PreviousPage,
                ConsoleKey.Escape => 	ActionType.Quit,
				_=> 					ActionType.Invalid
			};

		private ActionType ActionSelect(ConsoleKey input) =>
			input switch			
			{
                ConsoleKey.Enter => 	ActionType.Select,
                ConsoleKey.UpArrow => 	ActionType.PreviousItem,
                ConsoleKey.DownArrow => ActionType.NextItem,
                ConsoleKey.Escape => 	ActionType.Back,
				_ => 					ActionType.Invalid

			};

		private ActionType ConfirmationSelect(ConsoleKey input) =>
			input switch
			{
                ConsoleKey.Enter => 	ActionType.Select,
                ConsoleKey.UpArrow => 	ActionType.PreviousItem,
                ConsoleKey.DownArrow => ActionType.NextItem,
				_=> 					ActionType.Invalid
			};
	}
}
