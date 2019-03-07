using System;

namespace TaskrConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.CursorVisible = false;

			TaskrConsoleManager tcm = new TaskrConsoleManager();
			tcm.Run();
		}
	}
}
