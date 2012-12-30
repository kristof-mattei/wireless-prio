﻿namespace WirelessPrio.ViewModel
{
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using Annotations;

	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<WirelessInterfaceWithProfiles> _wirelessInterfacesWithProfiles;

		public ObservableCollection<WirelessInterfaceWithProfiles> WirelessInterfacesWithProfiles
		{
			get
			{
				return this._wirelessInterfacesWithProfiles;
			}
			set
			{
				if (Equals(value, this._wirelessInterfacesWithProfiles))
				{
					return;
				}
				this._wirelessInterfacesWithProfiles = value;
				this.OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}