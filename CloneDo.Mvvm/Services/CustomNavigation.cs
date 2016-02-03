using System;
using Xamarin.Forms;

namespace CloneDo.Mvvm.Services
{
	/// <summary>
	/// Provides the services of the Xamarin.Forms INavigation interface
	/// to ViewModels. This still uses a Page to implement the navigation,
	/// but since the ViewModel doesn't directly interact with the Page/View,
	/// this does not violate the MVVM pattern. Daw.
	/// </summary>
	public class CustomNavigation
	{
		private Page _page;
		public CustomNavigation (Page page) {
			_page = page;
		}

		/// <summary>
		/// Asynchronously pops the most recent Page from the navigation stack.
		/// </summary>
		public void PopAsync() {
			_page.Navigation.PopAsync ();
		}

		/// <summary>
		/// Asynchronously dismisses the most recently presented Page.
		/// </summary>
		public void PopModalAsync() {
			_page.Navigation.PopModalAsync ();
		}

		/// <summary>
		/// Pops all but the root Page from the navigation stack.
		/// </summary>
		public void PopToRootAsync() {
			_page.Navigation.PopToRootAsync ();
		}

		/// <summary>
		/// Asynchronously puts a Page on top of the navigation stack.
		/// </summary>
		/// <param name="page">Page.</param>
		public void PushAsync(Page page) {
			_page.Navigation.PushAsync (page);
		}

		/// <summary>
		/// Presents a page modally.
		/// </summary>
		/// <param name="page">Page.</param>
		public void PushModalAsync(Page page) {
			_page.Navigation.PushModalAsync (page);
		}

	}
}

