using System;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using CloneDo.Mvvm;
using CloneDo.Mvvm.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SwipeCell), typeof(AndroidSwipeCell))]
namespace CloneDo.Mvvm.Droid
{
	public class AndroidSwipeCell: ViewCellRenderer
	{
		SwipeCell taskItem;
		bool _touched = false;

		protected override Android.Views.View GetCellCore (Xamarin.Forms.Cell item, Android.Views.View convertView, ViewGroup parent, Context context) {
			var x = (SwipeCell)item;

			var view = convertView;

			if (view == null) {
				// no view to re-use, create new
				view = (context as Activity).LayoutInflater.Inflate(Resource.Layout.AndroidSwipeCell, null);
			}

			view.FindViewById<TextView> (Resource.Id.TaskNameText).Text = x.TaskName;
			view.FindViewById<TextView> (Resource.Id.TaskDateText).Text = x.TaskDate;
			view.FindViewById<ImageView> (Resource.Id.checkboxImg).Alpha = x.TaskDone ? 1.0f : 0.3f;
//			view.Touch += TouchHandler;
			view.FindViewById<ImageView> (Resource.Id.checkboxImg).Touch += ImageTouchHandler;


			taskItem = x;
			return view;
		}
	
		private void ImageTouchHandler(object sender, Android.Views.View.TouchEventArgs touchEventArgs) {
			var handled = false;
			if (touchEventArgs.Event.Action == MotionEventActions.Down) {
				Console.WriteLine ("{0} image was tapped.", taskItem.TaskName);
				_touched = true;
				handled = true;
			} else if (touchEventArgs.Event.Action == MotionEventActions.Up) {
				Console.WriteLine ("Touch canceled.");
				if (_touched) {
					Console.WriteLine ("AndroidSwipeCell::{0}.Done = {1}", taskItem.TaskName, !taskItem.TaskDone);
					taskItem.TaskDone = !taskItem.TaskDone;
					_touched = false;
				}

//				_touched = false;
				handled = true;
			}
			touchEventArgs.Handled = handled;
		}
//
//		private void TouchHandler(object sender, Android.Views.View.TouchEventArgs touchEventArgs) {
//			if (touchEventArgs.Event.ActionMasked == MotionEventActions.Up) {
//				Console.WriteLine ("{0} was tapped.", taskItem.TaskName);
////				Console.WriteLine ("Touch info ({0}, {1})", touchEventArgs.Event.GetX (), touchEventArgs.Event.GetY ());
//			}
//		}
	}
}

