using System;
using Xamarin.Forms;
using CloneDo.Mvvm.ViewModels;

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

		public string TaskDescription {
			get { return (string)base.GetValue (TaskDescriptionProperty); }
			set { base.SetValue (TaskDescriptionProperty, value); }
		}

		public static readonly BindableProperty TaskDescriptionProperty = 
			BindableProperty.Create<SwipeCell, string> (p => p.TaskDescription, "");

		public string TaskDate {
			get { return (string)base.GetValue (TaskDateProperty); }
			set { base.SetValue (TaskDateProperty, value); }
		}

		public static readonly BindableProperty TaskDateProperty = 
			BindableProperty.Create<SwipeCell, string> (p => p.TaskDate, "");

		public bool TaskDone {
			get { return (bool)base.GetValue (TaskDoneProperty); }
			set { 
				base.SetValue (TaskDoneProperty, value); 
				System.Diagnostics.Debug.WriteLine ("SwipeCell:: {0}.Done = {1}.", TaskName, value);
				((TaskCellViewModel)base.BindingContext).Done = value;
			}
		}

		public static readonly BindableProperty TaskDoneProperty =
			BindableProperty.Create<SwipeCell, bool> (p => p.TaskDone, true);

	}
}

