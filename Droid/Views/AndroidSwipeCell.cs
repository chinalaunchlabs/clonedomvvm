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
		protected override Android.Views.View GetCellCore (Xamarin.Forms.Cell item, Android.Views.View convertView, ViewGroup parent, Context context) {
			var x = (SwipeCell)item;

			var view = convertView;

			if (view == null) {
				// no view to re-use, create new
				view = (context as Activity).LayoutInflater.Inflate(Resource.Layout.AndroidSwipeCell, null);
			}

			view.FindViewById<TextView> (Resource.Id.TaskNameText).Text = x.TaskName;

			return view;
		}
	}
}

