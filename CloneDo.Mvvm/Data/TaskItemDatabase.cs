using System;
using SQLite;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using CloneDoMvvm.Models;

namespace CloneDo.Mvvm
{
	public class TaskItemDatabase
	{
		protected static object locker = new object();
		protected SQLiteConnection database;

		public TaskItemDatabase ()
		{
			database = DependencyService.Get<IDatabase>().DBConnect();
			database.CreateTable<TaskItem> ();
		}

		public IEnumerable<TaskItem> GetTasksDone(bool taskDone = true) {
			lock (locker) {
				return database.Table<TaskItem>().Where(x => x.Done == taskDone).OrderBy(x => x.Date).ToList();
			}
		}

		public TaskItem GetTask(int id) {
			lock (locker) {
				return database.Table<TaskItem> ().FirstOrDefault (x => x.ID == id);
			}
		}

		public int SaveTask (TaskItem task) {
			lock (locker) {
				if (task.ID != 0) {
					database.Update (task);
					return task.ID;
				} else {
					return database.Insert (task);
				}
			}
		}

		public int DeleteTask (int id) {
			lock (locker) {
				return database.Delete<TaskItem> (id);
			}
		}

		public int DeleteAllTasks() {
			lock (locker) {
				return database.DeleteAll<TaskItem> ();
			}
		}

	}
}

