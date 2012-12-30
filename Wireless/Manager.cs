namespace Wireless
{
	using System.Collections.Generic;
	using System.Linq;
	using Model;

	public class Manager
	{
		public /* TODO */ void GetWirelessNetworks()
		{
			var nativeHelper = new NativeHelper();


			List<WirelessInterface> interfaces = nativeHelper.GetAvailableWirelessInterfaces();


			List<Profile> sldkjflsdjf = nativeHelper.GetProfilesForWirelessInterface(interfaces.First().InterfaceGuid);

			// TODO return
		}
	}
}