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
		private List<TaskCellViewModel> _todoList = new List<TaskCellViewModel> ();

		/// <summary>
		/// Initializes a new instance of the <see cref="CloneDo.Mvvm.ViewModels.TodoListViewModel"/> class.
		/// </summary>
		public TodoListViewModel ()
		{
			
			// Initialize
			LoadTasks ();

			// Commands
			this.NewTaskCommand = new Command (() => {
				TaskItem newTask = new TaskItem();
				TaskItemViewModel vm = new TaskItemViewModel(newTask);
				Navigation.PushAsync(ViewFactory.CreatePage(vm));
			});

			this.ReloadCommand = new Command (() => {
				LoadTasks();
			});

			// Messages
			MessagingCenter.Subscribe<TaskItemViewModel, TaskItem> (this, "TaskAdded", (sender, task) => {
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskItemViewModel, TaskItem> (this, "TaskDeleted", (sender, task) => {
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskCellViewModel, TaskItem> (this, "TaskSetDone", (sender, task) => {
				System.Diagnostics.Debug.WriteLine("TodoListViewModel::Task set done.");
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskCellViewModel, TaskItem> (this, "TaskTapped", (sender, task) => {
				TaskTapped(task.ID);
			});

			MessagingCenter.Subscribe<TodoClient, TodoResponse> (this, "StatusMessage", (sender, response) => {
		
			});
		}


		public string AppName {
			get { return "CloneDo"; }
		}

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
			}
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

		private async void TaskTapped(int id) {
			// fetch task
			TaskItem fetchedTask = await App.Client.FetchTask(id);
			TaskItemViewModel viewModel = new TaskItemViewModel (fetchedTask);
			Navigation.PushAsync (ViewFactory.CreatePage (viewModel));
		}

		private async void LoadTasks() {
			System.Diagnostics.Debug.WriteLine ("TodoListViewModel::Loading tasks...");
			List<TaskItem> tasks = await App.Client.FetchAllTasks ();
			List<TaskCellViewModel> taskCells = new List<TaskCellViewModel> ();
			foreach (var task in tasks) {
				taskCells.Add (new TaskCellViewModel (task));
			}
			TodoList = taskCells;
		}
	
	}
}

