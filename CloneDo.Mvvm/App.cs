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

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

