namespace TaskrConsoleUI.Common
{
	public enum MenuType
	{
		TaskMenu,
		ConfirmationMenu,
	}

	public enum ActionType
	{
		NextItem,
		PreviousItem,
		NextPage,
		PreviousPage,
		Select,
		NewTask,
		Quit,
		Back,
		Invalid,
	}

	public enum SelectionState
	{
		MainSelection,
		SubMenuSelection,
		ConfirmationSelection,
	}

	public enum OptionType
	{
		Yes,
		No,
		ActionTask,
		CompleteTask,
		Back,
	}
}
