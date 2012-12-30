namespace Wireless
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using Model;
	using Native;

	public class NativeHelper
	{
		private IntPtr _handle;

		private IntPtr GetHandle()
		{
			if (IntPtr.Zero == this._handle)
			{
				// don't need this
				uint negotiatedVersion;

				// TODO set correct client version
				uint result = NativeWireless.WlanOpenHandle(2, IntPtr.Zero, out negotiatedVersion, out this._handle);

				if (NativeConstants.ERROR_SUCCESS != result)
					throw new Win32Exception((int) result);
			}


			return this._handle;
		}

		public List<WirelessInterface> GetAvailableWirelessInterfaces()
		{
			IntPtr handle;

			uint result = NativeWireless.WlanEnumInterfaces(this.GetHandle(), IntPtr.Zero, out handle);

			result.ThrowIfNotSuccess();

			WLAN_INTERFACE_INFO_LIST wlanInterfaceInfoList = new WLAN_INTERFACE_INFO_LIST(handle);

			// convert the 'native' type to something we can pass on to the front
			IEnumerable<WirelessInterface> collection = wlanInterfaceInfoList.InterfaceInfo.Select(e => new WirelessInterface(e.InterfaceGuid, e.strInterfaceDescription));


			return new List<WirelessInterface>(collection);
		}

		public List<Profile> GetProfilesForWirelessInterface(Guid interfaceGuid)
		{
			IntPtr handle;

			uint result = NativeWireless.WlanGetProfileList(this.GetHandle(), interfaceGuid, IntPtr.Zero, out handle);

			result.ThrowIfNotSuccess();



		} 
	}
}