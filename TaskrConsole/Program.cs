using System;

namespace TaskrConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			Console.CursorVisible = false;

			new TaskrConsoleManager().Run();
		}
	}
}
