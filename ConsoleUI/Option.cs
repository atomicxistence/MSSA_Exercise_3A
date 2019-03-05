namespace ConsoleUI
{
	public struct Option
	{
		public string Title { get; private set; }
		public int Index { get; private set; }

		public Option(string title, int index)
		{
			Title = title;
			Index = index;
		}
	}
}
