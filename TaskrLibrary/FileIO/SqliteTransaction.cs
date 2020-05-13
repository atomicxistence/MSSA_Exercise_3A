using System;
using System.IO;
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
        private string DbFile => $"{Environment.CurrentDirectory}{Path.PathSeparator}Taskr.db";
        private string ConnectionString => $"Data Source={DbFile}";
        private IDbConnection DbConnection => new SqliteConnection(ConnectionString);

        public List<Task> Load()
        {
            if (!File.Exists(DbFile)) CreateDb();

            using (DbConnection)
            {
                var tasks = DbConnection.Query<Task>("SELECT * FROM Task");
                return tasks.ToList();
            }
        }

        public Task InsertTask(Task task)
        {
            using (DbConnection)
            {    
                var taskWithId = DbConnection.QuerySingle<Task>(@"INSERT INTO Task (Title, CreatedOn, IsActioned, IsCompleted)
                                                                OUTPUT INSERTED.* 
                                                                VALUES (@Title, @CreatedOn, @IsActioned, @IsCompleted);", task);
                return taskWithId;
            }
        }


        public Task UpdateTask(Task task)
        {
            throw new System.NotImplementedException();
        }

        private void CreateDb()
        {
            using (DbConnection)
            {
                DbConnection.Open();
                DbConnection.Execute(
                    @"CREATE TABLE Task
                    (
                        TaskId INTEGER IDENTITY PRIMARY KEY AUTOINCREMENT,
                        Title VARCHAR(150) NOT NULL,
                        CreatedOn TEXT,
                        IsActioned INTEGER NOT NULL,
                        IsCompleted INTEGER NOT NULL
                        
                    )"
                );
            }
        }
    }
}