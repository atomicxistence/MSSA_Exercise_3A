using System.Collections.Generic;
using TaskrLibrary.Models;

namespace TaskrLibrary.FileIO
{
    public class SqliteTransaction : IFileTransaction
    {
        public List<Task> Load()
        {
            throw new System.NotImplementedException();
        }

        public Task InsertTask(Task task)
        {
            throw new System.NotImplementedException();
        }


        public Task UpdateTask(Task task)
        {
            throw new System.NotImplementedException();
        }
    }
}