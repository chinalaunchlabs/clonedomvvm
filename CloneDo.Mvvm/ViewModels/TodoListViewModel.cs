using System;
using CloneDoMvvm.Models;
using System.Collections.ObjectModel;
using CloneDo.Mvvm.Factories;

namespace CloneDo.Mvvm.ViewModels
{
	public class TodoListViewModel : BaseViewModel
	{
		ObservableCollection<TaskCellViewModel> _todoList = new ObservableCollection<TaskCellViewModel>();
		public ObservableCollection<TaskCellViewModel> TodoList {
			get { return _todoList; }
			set {
				if (_todoList == value)
					return;
				_todoList = value;
				OnPropertyChanged ();
			}
		}

		public TodoListViewModel ()
		{
			// TODO: Clean this up later.
			// Hardcoded values for testing.
			App.Database.DeleteAllTasks();
			App.Database.SaveTask (new TaskItem { Task="Buy apples", Description="", Done=true });
			App.Database.SaveTask (new TaskItem { Task="Sell dem apples", Description="", Done=true });
			App.Database.SaveTask (new TaskItem { Task="Roll over", Description="", Done=true });
			App.Database.SaveTask (new TaskItem { Task="Cry", Description="", Done=true });
			App.Database.SaveTask (new TaskItem { Task="Repeat last two tasks ad infinitum", Description="", Done=true });

			var tasks = App.Database.GetTasksDone ();
			foreach (TaskItem t in tasks) {
				System.Diagnostics.Debug.WriteLine ("Adding {0} to list", t.Task);
				_todoList.Add (new TaskCellViewModel (t)); 
			}
		}
	
		protected object _selectedItem;
		public object SelectedTaskItem {
			get { return _selectedItem; }
			set {
				if (_selectedItem == value)
					return;

				_selectedItem = value;
				OnPropertyChanged ();

				if (_selectedItem != null) {
					System.Diagnostics.Debug.WriteLine ("Task '" + ((TaskCellViewModel)_selectedItem).TaskName + "' was selected.");

					TaskItemViewModel viewModel = new TaskItemViewModel (((TaskCellViewModel)_selectedItem).TaskItem);
					_selectedItem = null;

					Navigation.PushAsync (ViewFactory.CreatePage (viewModel));
				
				}
			}
		}
	
	}
}

