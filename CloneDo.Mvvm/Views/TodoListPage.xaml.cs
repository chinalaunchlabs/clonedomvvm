using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CloneDo.Mvvm.ViewModels;

namespace CloneDo.Mvvm
{
	public partial class TodoListPage : ContentPage
	{
		public TodoListPage ()
		{
			InitializeComponent ();
			todoList.ItemTemplate = new DataTemplate (typeof(TaskCell));
//			todoList.ItemTemplate = new DataTemplate (typeof(SwipeCell));
//			todoList.ItemTemplate.SetBinding (SwipeCell.TaskNameProperty, "TaskName");
//			todoList.ItemTemplate.SetBinding (SwipeCell.TaskDateProperty, "DueDate");
//			todoList.ItemTemplate.SetBinding (SwipeCell.TaskDoneProperty, "Done");

			// prevent list items from being selected
			todoList.ItemSelected += (sender, e) => {
				((ListView)sender).SelectedItem = null;
			};
//			todoList.SetBinding (ListView.SelectedItemProperty, "SelectedTaskItem");
		}
	}
}

