using System;

using Xamarin.Forms;
using CloneDo.Mvvm.Factories;
using CloneDo.Mvvm.ViewModels;
using CloneDo.Mvvm.Services;

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

		static TodoClient _client;
		public static TodoClient Client {
			get {
				_client = _client ?? new TodoClient ();
				return _client;
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

