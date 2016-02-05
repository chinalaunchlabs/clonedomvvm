using System;
using CloneDoMvvm.Models;
using System.Collections.ObjectModel;
using CloneDo.Mvvm.Factories;
using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.Generic;

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

		public TodoListViewModel ()
		{
			
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
				System.Diagnostics.Debug.WriteLine("Task '{0}' was saved", task.Task);
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskItemViewModel, TaskItem> (this, "TaskDeleted", (sender, task) => {
				System.Diagnostics.Debug.WriteLine("Task '{0}' was deleted", task.Task);
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskCellViewModel, TaskItem> (this, "TaskSetDone", (sender, task) => {
				System.Diagnostics.Debug.WriteLine("Task '{0}' doneness was set/unset", task.Task);
				LoadTasks();
			});

			MessagingCenter.Subscribe<TaskCellViewModel, TaskItem> (this, "TaskTapped", (sender, task) => {
				System.Diagnostics.Debug.WriteLine("Task '{0}' was tapped", task.Task);
				TaskItemViewModel viewModel = new TaskItemViewModel (task);
				Navigation.PushAsync (ViewFactory.CreatePage (viewModel));
			});
		}
	
		/// <summary>
		/// Populates the list with tasks from the database. Called to force a change in the UI.
		/// </summary>
		private void LoadTasks() {
			var tasks = App.Database.GetTasksDone ();
			List<TaskCellViewModel> doneCollection = new List<TaskCellViewModel> ();
			foreach (TaskItem t in tasks) {
				doneCollection.Add (new TaskCellViewModel (t)); 
			}

			tasks = App.Database.GetTasksDone (false);
			List<TaskCellViewModel> todoCollection = new List<TaskCellViewModel> ();
			todoCollection.Clear ();
			foreach (TaskItem t in tasks) {
				todoCollection.Add (new TaskCellViewModel (t)); 
			}
			TodoList = todoCollection;
			TodoList.AddRange(doneCollection);
		}

		private object _selectedItem;
		public object SelectedTaskItem {
			get { return _selectedItem; }
			set {
				if (_selectedItem == value)
					return;

				_selectedItem = value;
				if (_selectedItem != null) {
					System.Diagnostics.Debug.WriteLine ("Task '" + ((TaskCellViewModel)_selectedItem).TaskName + "' was selected.");

					TaskItemViewModel viewModel = new TaskItemViewModel (((TaskCellViewModel)_selectedItem).TaskItem);
					_selectedItem = null;

					Navigation.PushAsync (ViewFactory.CreatePage (viewModel));
				
				}
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
	
	}
}

