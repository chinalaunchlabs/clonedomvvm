using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CloneDo.Mvvm.Services;

namespace CloneDo.Mvvm.ViewModels
{
	public class BaseViewModel: INotifyPropertyChanged
	{

		public CustomNavigation Navigation { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) {
				handler (this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

