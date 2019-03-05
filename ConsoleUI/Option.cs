namespace ConsoleUI
{
	public struct Option
	{
		public string Title { get; private set; }
		public OptionType Action { get; private set; }

		public Option(string title, OptionType action)
		{
			Title = title;
			Action = action;
		}
	}
}
