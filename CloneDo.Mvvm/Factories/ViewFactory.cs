using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using CloneDo.Mvvm.ViewModels;
using CloneDo.Mvvm.Services;

namespace CloneDo.Mvvm.Factories 
{
	/// <summary>
	/// Takes care of mapping ViewModels to corresponding View types.
	/// </summary>
	static class ViewFactory
	{
		static Dictionary<Type,Type> map = new Dictionary<Type, Type>();
		public static void Register<TViewModel, TView>()
			where TViewModel: BaseViewModel
			where TView: Page 
		{
			System.Diagnostics.Debug.WriteLine ("Register: " + typeof(TViewModel) + " with " + typeof(TView));
			map [typeof(TViewModel)] = typeof(TView);
		}

		/// <summary>
		/// Creates a page of a type corresponding to the viewmodel type.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="viewModelType">View model type.</param>
		public static Page CreatePage(Type viewModelType) {
			Type viewType = map [viewModelType];

			var page = (Page)Activator.CreateInstance(viewType);
			var viewModel = (BaseViewModel)Activator.CreateInstance (viewModelType);

			// this is where the view and viewmodel are coupled
			viewModel.Navigation = new CustomNavigation(page);
			page.BindingContext = viewModel;

			return page;
		}

		/// <summary>
		/// Creates a Page given the ViewModel.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="viewModel">View model.</param>
		public static Page CreatePage(BaseViewModel viewModel) {
			Type viewType = map [viewModel.GetType ()];

			var page = (Page)Activator.CreateInstance (viewType);

			// view and viewmodel coupling
			viewModel.Navigation = new CustomNavigation (page);
			System.Diagnostics.Debug.WriteLine ("page == null: " + page == null);
			System.Diagnostics.Debug.WriteLine ("viewmodel == null: " + viewModel == null);
			page.BindingContext = viewModel;

			return page;
		}

		/// <summary>
		/// Creates a Page given the ViewModel.
		/// </summary>
		/// <returns>The page.</returns>
		/// <param name="viewModel">View model.</param>
		public static Page CreatePage<TViewModel>()
			where TViewModel: BaseViewModel 
		{
			var viewModelType = typeof(TViewModel);
			return CreatePage (viewModelType);
		}
	}
}

