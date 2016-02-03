using System;
using CloneDoMvvm.Models;

namespace CloneDo.Mvvm.ViewModels
{
	public class TaskCellViewModel : BaseViewModel
	{
		TaskItem task;
		public TaskCellViewModel (TaskItem taskItem)
		{
			task = taskItem;
		}

		public TaskItem TaskItem {
			get { return task; }
		}

		public string TaskName {
			get { return task.Task; }
		}

		public string DueDate {
			get { return task.Date.ToString (); }
		}
	}
}

