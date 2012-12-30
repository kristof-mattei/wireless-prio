namespace Wireless.Native
{
	using System;
	using System.Runtime.InteropServices;

	public static class NativeWireless
	{
		//DWORD WINAPI WlanOpenHandle(
		//  _In_        DWORD dwClientVersion,
		//  _Reserved_  PVOID pReserved,
		//  _Out_       PDWORD pdwNegotiatedVersion,
		//  _Out_       PHANDLE phClientHandle
		//)
		[DllImport("wlanapi.dll", CallingConvention = CallingConvention.Winapi, ExactSpelling = true, SetLastError = true, PreserveSig = true)]
		public static extern uint WlanOpenHandle(
			[In] uint dwClientVersion,
			[In] IntPtr pRservered,
			[Out] out uint pdwNegotiatedVersion,
			[Out] out IntPtr phClientHandle);

		//DWORD WINAPI WlanEnumInterfaces(
		//  _In_        HANDLE hClientHandle,
		//  _Reserved_  PVOID pReserved,
		//  _Out_       PWLAN_INTERFACE_INFO_LIST *ppInterfaceList
		//);
		[DllImport("wlanapi.dll", CallingConvention = CallingConvention.Winapi, ExactSpelling = true, SetLastError = true, PreserveSig = true)]
		public static extern uint WlanEnumInterfaces(
			[In] IntPtr hClientHandle,
			[In] IntPtr pRservered,
			[Out] out IntPtr ppInterfaceList);

		//DWORD WINAPI WlanGetProfileList(
		//  _In_        HANDLE hClientHandle,
		//  _In_        const GUID *pInterfaceGuid,
		//  _Reserved_  PVOID pReserved,
		//  _Out_       PWLAN_PROFILE_INFO_LIST *ppProfileList
		//);
		[DllImport("wlanapi.dll", CallingConvention = CallingConvention.Winapi, ExactSpelling = true, SetLastError = true, PreserveSig = true)]
		public static extern uint WlanGetProfileList(
			[In] IntPtr hClientHandle,
			[In] [MarshalAs(UnmanagedType.LPStruct)] Guid pInterfaceGuid,
			[In] IntPtr pReserved,
			[Out] out IntPtr ppProfileList);
	}
}