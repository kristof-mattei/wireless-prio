namespace Wireless.Native
{
	using System.Runtime.InteropServices;

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct WLAN_PROFILE_INFO
	{
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string strProfileName;

		public uint dwFlags;
	}
}