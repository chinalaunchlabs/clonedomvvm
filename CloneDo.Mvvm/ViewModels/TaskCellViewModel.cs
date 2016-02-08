using System;
using CloneDoMvvm.Models;
using Xamarin.Forms;
using System.Windows.Input;

namespace CloneDo.Mvvm.ViewModels
{
	public class TaskCellViewModel : BaseViewModel
	{
		TaskItem task;
		public TaskCellViewModel (TaskItem taskItem)
		{
			task = taskItem;

			this.SetDoneCommand = new Command (() => {
				Done = !Done;
				App.Database.SaveTask(task);
				MessagingCenter.Send<TaskCellViewModel, TaskItem>(this, "TaskSetDone", task);
			});

			this.TaskEditCommand = new Command (() => {
				MessagingCenter.Send<TaskCellViewModel, TaskItem>(this, "TaskTapped", task);
			});
		}

		public TaskItem TaskItem {
			get { return task; }
		}

		public string TaskName {
			get { return task.Task; }
		}

		public string DueDate {
			get { return StringifyDate(); }
		}

		public bool Done {
			get { return task.Done; } 
			set {
				if (task.Done == value)
					return;
				task.Done = value;
				System.Diagnostics.Debug.WriteLine ("Done: " + task.Done);
				OnPropertyChanged ("Done");
				App.Database.SaveTask(task);
				MessagingCenter.Send<TaskCellViewModel, TaskItem>(this, "TaskSetDone", task);

			}
		}

		public Color TaskNameColor {
			get {
				if (Done) {
					return Color.Gray;
				} else {
					if (task.Date < DateTime.Today)
						return Color.Red;
					else
						return Color.Black;
				}
			}
		}

		public double CheckOpacity {
			get {
				if (Done)
					return 1.0;
				else
					return 0.2;
			}
		}


		// Command interfaces
		public ICommand SetDoneCommand {
			get;
			private set;
		}

		public ICommand TaskEditCommand {
			get;
			private set;
		}


		private string StringifyDate() {
			var partialString = "";

			partialString = String.Format ("{0:MMM dd yyyy}", task.Date).ToLower();

			// diff - number of days after today (positive),
			// 		or number of days before today (negative)
			var diff = ((TimeSpan)(task.Date - DateTime.Today)).Days;
			if (diff >= 0) {
				if (diff == 0)
					partialString = "today";
				else if (diff >= 1 && diff <= 3)
					partialString = String.Format ("in {0} day{1}", diff, diff == 1 ? "" : "s");
				else
					partialString = "on " + partialString;
			} else {
				diff *= -1;
				if (diff == 1)
					partialString = "yesterday";
				else if (diff > 1 && diff <= 3)
					partialString = String.Format ("{0} days ago", diff);
				else
					partialString = "on " + partialString;
			}

			if (Done)
				return partialString;
			else
				return "due " + partialString;
		}
			
	}
}

