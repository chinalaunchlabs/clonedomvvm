using System;
using CloneDo.Mvvm.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace CloneDo.Mvvm.ViewModels
{
	public class TaskItemViewModel: BaseViewModel
	{
		TaskItem task;

		public TaskItemViewModel (TaskItem taskItem)
		{
			task = taskItem;

			this.SaveCommand = new Command (async (temp) => {
//				App.Database.SaveTask(task);
				bool success = await App.Client.SaveTask(task);
				if (success) Navigation.PopAsync();

				// Broadcast this message for other viewmodel
				MessagingCenter.Send<TaskItemViewModel, TaskItem>(this, "TaskAdded", task);
			});

			this.DeleteCommand = new Command (async (temp) => {
//				App.Database.DeleteTask(task.ID);
				bool success = await App.Client.DeleteTask(task.ID);
				if (success) Navigation.PopAsync();

				MessagingCenter.Send<TaskItemViewModel, TaskItem>(this, "TaskDeleted", task);
			});
				
			this.ResetCommand = new Command (() => {
				this.Task = "";
				this.Description = "";
				this.DueDate = DateTime.Today;
				this.Done = false;
			});
		}


		public string Title {
			get {
				if (task.ID > 0)
					return String.Format ("Edit: {0}", Task);
				else
					return "New Task";
			}
		}

		/// <summary>
		/// Gets or sets the name of the todo item.
		/// </summary>
		/// <value>The task.</value>
		public string Task {
			get { return task.Name; }
			set {
				if (task.Name == value)
					return;
				task.Name = value;
				OnPropertyChanged ("Task");
				OnPropertyChanged ("CanSave");
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
				OnPropertyChanged ("Description");
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
				OnPropertyChanged ("Done");
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
				OnPropertyChanged ("DueDate");
			}
		}

		// Command interfaces
		public ICommand SaveCommand {
			get; protected set;
		}

		public ICommand DeleteCommand {
			get; protected set;
		}

		public ICommand ResetCommand {
			get; protected set;
		}

		public bool CanSave {
			get { return task.Name.Length > 0; }
		}

		public bool CanDelete {
			get { return task.ID > 0; }
		}
			
	}
}

