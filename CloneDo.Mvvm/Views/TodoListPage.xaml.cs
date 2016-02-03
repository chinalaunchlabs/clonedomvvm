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
			listView.ItemTemplate = new DataTemplate (typeof(TaskCell));
			listView.SetBinding (ListView.SelectedItemProperty, "SelectedTaskItem");
		}
	}
}

