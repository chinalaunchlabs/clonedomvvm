using System;

using Xamarin.Forms;
using CloneDo.Mvvm.Factories;
using CloneDo.Mvvm.ViewModels;

namespace CloneDo.Mvvm
{
	public class App : Application
	{

		static TaskItemDatabase _database;
		public static TaskItemDatabase Database {
			get {
				_database = _database ?? new TaskItemDatabase ();
				return _database;
			}
		}
			
		void Register() {
			ViewFactory.Register<TodoListViewModel, TodoListPage> ();
			ViewFactory.Register<TaskItemViewModel, TaskItemPage> ();
		}

		public App ()
		{
			Register ();
			MainPage = new NavigationPage (ViewFactory.CreatePage<TodoListViewModel> ());
		}
	}
}

