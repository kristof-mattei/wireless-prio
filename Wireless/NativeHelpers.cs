namespace Wireless
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using Model;

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
				uint result = Native.WlanOpenHandle(2, IntPtr.Zero, out negotiatedVersion, out this._handle);

				if (NativeConstants.ERROR_SUCCESS != result)
					throw new Win32Exception((int) result);
			}


			return this._handle;
		}

		public List<Interface> GetAvailableWirelessInterfaces()
		{
			IntPtr handle;

			uint result = Native.WlanEnumInterfaces(this.GetHandle(), IntPtr.Zero, out handle);

			if (NativeConstants.ERROR_SUCCESS != result)
				throw new Win32Exception((int)result);

			Native.WLAN_INTERFACE_INFO_LIST wlanInterfaceInfoList = new Native.WLAN_INTERFACE_INFO_LIST(handle);

			// convert the 'native' type to something we can pass on to the front
			IEnumerable<Interface> collection = wlanInterfaceInfoList.InterfaceInfo.Select(e => new Interface(e.InterfaceGuid, e.strInterfaceDescription));


			return new List<Interface>(collection);
		}

		public List<Profile> GetProfilesForWirelessInterface(Guid interfaceGuid)
		{
			IntPtr handle;

			uint result = Native.WlanGetProfileList(this.GetHandle(), interfaceGuid, IntPtr.Zero, out handle);

			if (NativeConstants.ERROR_SUCCESS != result)
				throw new Win32Exception((int)result);


		} 
	}
}