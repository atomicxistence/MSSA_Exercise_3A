using System.Collections.Generic;
using TaskrLibrary.Models;

namespace TaskrLibrary
{
	interface IFileManager
	{
		List<Page> Load();

		void Save(List<Page> pages);
	}
}
