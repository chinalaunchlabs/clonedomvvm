using System;
using Xamarin.Forms;

namespace CloneDo.Mvvm
{
	public class SwipeCell : ViewCell
	{
	
		public string TaskName {
			get { return (string)base.GetValue (TaskNameProperty); }
			set { base.SetValue (TaskNameProperty, value); }
		}

		public static readonly BindableProperty TaskNameProperty =
			BindableProperty.Create<SwipeCell, string> (p => p.TaskName, "");

	}
}

