namespace Wireless
{
	using System.Collections.Generic;
	using Model;

	public class Manager
	{
		public /* TODO */ void GetWirelessNetworks()
		{
			var nativeHelper = new NativeHelper();


			List<WirelessInterface> interfaces = nativeHelper.GetAvailableWirelessInterfaces();


			// TODO return
		}
	}
}