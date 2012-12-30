namespace WirelessPrio.ViewModel
{
	using System.Collections.ObjectModel;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;
	using Annotations;
	using Model;

	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<WirelessInterface> _wirelessInterfaces;

		public ObservableCollection<WirelessInterface> WirelessInterfaces
		{
			get
			{
				return this._wirelessInterfaces;
			}
			set
			{
				if (Equals(value, this._wirelessInterfaces))
				{
					return;
				}
				this._wirelessInterfaces = value;
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