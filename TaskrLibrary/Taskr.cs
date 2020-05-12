using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using TaskrLibrary.Models;

namespace TaskrLibrary
{
    public class NewTaskr
    {
		private int pageNumber = 1;
        private List<Page> Pages { get; set;}
		public Page CurrentPage => Pages[pageNumber - 1];
		public Page NextPage => ChangePage(1);
		public Page PreviousPage => ChangePage(-1);

		public static int PageSize => 25;

        public NewTaskr()
        {
			Pages = IntializePages();
        }


        public Page CreateTask(string description)
        {
			var task = new Task()
			{
				Title = description,
				CreatedOn = DateTime.Now,
				IsActioned = false,
				IsCompleted = false
			};
			//TODO: save to db and return task with id
			InsertTaskOnLastPage(task);
			return CurrentPage;
        }

		public Page ActionTask(Task task)
		{
			var newTask = new Task()
			{
				Title = task.Title,
				CreatedOn = task.CreatedOn,
				IsActioned = false,
				IsCompleted = false
			};
			//TODO: insert new task to db and return task with id
			InsertTaskOnLastPage(newTask);

			task.IsActioned = true;
			//TODO: update task status in db
			return CurrentPage;
		}

		public Page CompleteTask(Task task)
		{
			task.IsCompleted = true;
			//TODO: update task status in db
			return CurrentPage;
		}

        private List<Page> IntializePages()
        {
            //TODO: try to load from database
			return new List<Page>()
			{
				new Page()
			};
        }

		private Page ChangePage(int amount)
		{
			pageNumber = (pageNumber + amount) % Pages.Count();
			return CurrentPage;
		}

        private void InsertTaskOnLastPage(Task task)
		{
			if (Pages.Last().IsFull)
			{
				var newPage = new Page();
				newPage.Tasks.Add(task);
				Pages.Add(newPage);
			}
			else
			{
				Pages.Last().Tasks.Add(task);
			}
		}
    }
}