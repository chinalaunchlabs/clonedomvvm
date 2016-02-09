using System;
using CloneDo.Mvvm.Models;
using System.Collections.ObjectModel;
using CloneDo.Mvvm.Factories;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Generic;
using CloneDo.Mvvm.Services;

namespace CloneDo.Mvvm.ViewModels
{
	public class TodoListViewModel : BaseViewModel
	{

		public string AppName {
			get { return "CloneDo"; }
		}

		private List<TaskCellViewModel> _todoList = new List<TaskCellViewModel> ();
		/// <summary>
		/// List of tasks to be done.
		/// </summary>
		/// <value>The todo list.</value>
		public List<TaskCellViewModel> TodoList {
			get { return _todoList; }
			private set {
				if (_todoList == value)
					return;
				_todoList = value;
				OnPropertyChanged ();
			}
		}

		private TodoClient _client;
		public TodoListViewModel ()
		{

			_client = new TodoClient ();
			
			// Initialize
			LoadTasks ();

			// Commands
			this.NewTaskCommand = new Command (() => {
				System.Diagnostics.Debug.WriteLine("Making new task");
				TaskItem newTask = new TaskItem();
				TaskItemViewModel vm = new TaskItemViewModel(newTask);
				Navigation.PushAsync(ViewFactory.CreatePage(vm));
			});

			this.ReloadCommand = new Command (() => {
				LoadTasks();
			});

			// Messages
			MessagingCenter.Subscribe<TaskItemViewModel, TaskItem> (this, "TaskAdded", (sender, task) => {
				System.Diagnostics.Debug.WriteLine("Task '{0}' was saved", task.Name);
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskItemViewModel, TaskItem> (this, "TaskDeleted", (sender, task) => {
				System.Diagnostics.Debug.WriteLine("Task '{0}' was deleted", task.Name);
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskCellViewModel, TaskItem> (this, "TaskSetDone", (sender, task) => {
				System.Diagnostics.Debug.WriteLine("Task '{0}' doneness was set/unset", task.Name);
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskCellViewModel, TaskItem> (this, "TaskTapped", (sender, task) => {
				System.Diagnostics.Debug.WriteLine("Task '{0}' was tapped", task.Name);

				TaskTapped(task.ID);
			});
		}

		private async void TaskTapped(int id) {
			// fetch task
			TaskItem fetchedTask = await _client.FetchTask(id);
			TaskItemViewModel viewModel = new TaskItemViewModel (fetchedTask);
			Navigation.PushAsync (ViewFactory.CreatePage (viewModel));
		}
	
		/// <summary>
		/// Populates the list with tasks from the database. Called to force a change in the UI.
		/// </summary>
//		private void LoadTasks() {
//			var tasks = App.Database.GetTasksDone ();
//			List<TaskCellViewModel> doneCollection = new List<TaskCellViewModel> ();
//			foreach (TaskItem t in tasks) {
//				doneCollection.Add (new TaskCellViewModel (t)); 
//			}
//
//			tasks = App.Database.GetTasksDone (false);
//			List<TaskCellViewModel> todoCollection = new List<TaskCellViewModel> ();
//			todoCollection.Clear ();
//			foreach (TaskItem t in tasks) {
//				todoCollection.Add (new TaskCellViewModel (t)); 
//			}
//			TodoList = todoCollection;
//			TodoList.AddRange(doneCollection);
//		}

		private async void LoadTasks() {
			List<TaskItem> tasks = await _client.FetchAllTasksAsync ();
			List<TaskCellViewModel> taskCells = new List<TaskCellViewModel> ();
			foreach (var task in tasks) {
				taskCells.Add (new TaskCellViewModel (task));
			}
			TodoList = taskCells;
		}

		// Command interfaces
		public ICommand NewTaskCommand {
			get;
			protected set;
		}

		public ICommand ReloadCommand {
			get;
			protected set;
		}
	
	}
}

