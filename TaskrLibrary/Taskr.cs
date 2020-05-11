using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TaskrLibrary.Models;

namespace TaskrLibrary
{
    public class NewTaskr
    {
        private TaskList taskList;
		private IFileManager fileManager;

        public NewTaskr()
        {
            fileManager = new XMLFileManager();
			LoadTaskList();
        }

        public void AddTask(string description)
        {

        }

        private void InsertTaskOnLastPage(Task task)
		{
			if (taskList.Pages.Last().IsFull)
			{
				var newPage = new Page();
				newPage.Tasks.Add(task);
				taskList.Pages.Add(newPage);
			}
			else
			{
				taskList.Pages.Last().Tasks.Add(task);
			}
		}

        private void LoadTaskList()
		{
			try
			{
				taskList = fileManager.Load();
			}
			catch (FileNotFoundException)
			{
				taskList = new TaskList();
			}
		}

        private void SaveTaskList()
        {
            fileManager.Save(taskList);
        }
    }
}