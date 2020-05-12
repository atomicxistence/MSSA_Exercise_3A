using System.Collections.Generic;
using TaskrLibrary.Models;

namespace TaskrLibrary.FileIO
{
    public interface IFileTransaction
    {
        List<Task> Load();
        Task InsertTask(Task task);
        Task UpdateTask(Task task);
    }
}