using System;
using CloneDoMvvm.Models;

namespace CloneDo.Mvvm.ViewModels
{
	public class TaskItemViewModel: BaseViewModel
	{
		TaskItem task;

		public TaskItemViewModel (TaskItem taskItem)
		{
			task = taskItem;
		}

		/// <summary>
		/// Gets or sets the name of the todo item.
		/// </summary>
		/// <value>The task.</value>
		public string Task {
			get { return task.Task; }
			set {
				if (task.Task == value)
					return;
				task.Task = value;
				OnPropertyChanged ();
			}
		}

		/// <summary>
		/// Gets or sets the description of the todo item.
		/// </summary>
		/// <value>The description.</value>
		public string Description {
			get { return task.Description; }
			set {
				if (task.Description == value)
					return;
				task.Description = value;
				OnPropertyChanged ();
			}
		}

		/// <summary>
		/// Gets or sets whether or not the task is done.
		/// </summary>
		/// <value><c>true</c> if done; otherwise, <c>false</c>.</value>
		public bool Done {
			get { return task.Done; }
			set {
				if (task.Done == value)
					return;
				task.Done = value;
				OnPropertyChanged ();
			}
		}

		/// <summary>
		/// Gets or sets the due date of the task.
		/// </summary>
		/// <value>The due date.</value>
		public DateTime DueDate {
			get { return task.Date; }
			set {
				if (task.Date == value)
					return;
				task.Date = value;
				OnPropertyChanged ();
			}
		}
	}
}

