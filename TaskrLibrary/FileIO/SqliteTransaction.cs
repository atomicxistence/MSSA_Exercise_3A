using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Microsoft.Data.Sqlite;
using TaskrLibrary.Models;

namespace TaskrLibrary.FileIO
{
    public class SqliteTransaction : IFileTransaction
    {
        private readonly string connectionString = "Data Source=Taskr.db;Version=3";

        public List<Task> Load()
        {
            using (IDbConnection db = new SqliteConnection(connectionString))
            {
                var tasks = db.Query<Task>("SELECT * FROM Task");
                return tasks.ToList();
            }
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