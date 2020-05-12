using System;
using System.Collections.Generic;
using System.Linq;
using TaskrLibrary.Models;
using TaskrLibrary.FileIO;

namespace TaskrLibrary
{
    public class NewTaskr
    {
		public int PageSize => 25;

		private int pageNumber = 1;
        private List<Page> Pages { get; set;}
		public Page CurrentPage => Pages[pageNumber - 1];
		public Page NextPage => ChangePage(1);
		public Page PreviousPage => ChangePage(-1);
		private readonly IFileTransaction file;

        public NewTaskr(IFileTransaction file)
        {
			this.file = file;
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
			task = file.InsertTask(task);
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
			newTask = file.InsertTask(newTask);
			InsertTaskOnLastPage(newTask);

			task.IsActioned = true;
			file.UpdateTask(task);
			return CurrentPage;
		}

		public Page CompleteTask(Task task)
		{
			task.IsCompleted = true;
			file.UpdateTask(task);
			return CurrentPage;
		}

        private List<Page> IntializePages()
        {
            var tasks = file.Load();
            if (tasks == null)
            {
                return new List<Page>()
                {
                    new Page(PageSize)
                };
            }

            return CreatePagesOfTasks(tasks);
        }

        private List<Page> CreatePagesOfTasks(List<Task> tasks)
        {
            var pages = new List<Page>();
            var relevantTasks = tasks.SkipWhile(x => x.IsActioned);
            for (int i = 0; i < relevantTasks.Count() % PageSize; i++)
            {
                var page = new Page();
                page.Tasks.AddRange(relevantTasks.Skip(i * PageSize).Take(PageSize));
                pages.Add(page);
            }

            return pages;
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