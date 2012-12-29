namespace Wireless
{
	using System;
	using System.Runtime.InteropServices;

	public static class Native
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



		/// <summary>
		/// Contains an array of NIC information
		/// </summary>
		public struct WLAN_INTERFACE_INFO_LIST
		{
			/// <summary>
			/// Length of <see cref="InterfaceInfo"/> array
			/// </summary>
			public int dwNumberOfItems;

			/// <summary>
			/// This member is not used by the wireless service. Applications can use this member when processing individual interfaces.
			/// </summary>
			public int dwIndex;

			/// <summary>
			/// Array of WLAN interfaces.
			/// </summary>
			public WLAN_INTERFACE_INFO[] InterfaceInfo;

			/// <summary>
			/// Constructor for WLAN_INTERFACE_INFO_LIST.
			/// Constructor is needed because the InterfaceInfo member varies based on how many adapters are in the system.
			/// </summary>
			/// <param name="pointerToWlanInterfaceInfoList">the unmanaged pointer containing the list.</param>
			public WLAN_INTERFACE_INFO_LIST(IntPtr pointerToWlanInterfaceInfoList)
			{
				// The first 4 bytes are the number of WLAN_INTERFACE_INFO structures.
				dwNumberOfItems = Marshal.ReadInt32(pointerToWlanInterfaceInfoList, 0);

				// The next 4 bytes are the index of the current item in the unmanaged API.
				dwIndex = Marshal.ReadInt32(pointerToWlanInterfaceInfoList, 4);

				// Construct the array of WLAN_INTERFACE_INFO structures.
				InterfaceInfo = new WLAN_INTERFACE_INFO[dwNumberOfItems];

				// start pointer
				long start = pointerToWlanInterfaceInfoList.ToInt64() + 8; // we skip 8 for the first and second int in the structure

				// get the size of WLAN_INTERFACE_INFO
				int sizeOfWlanInterfaceInfo = Marshal.SizeOf(typeof (WLAN_INTERFACE_INFO));

				// we know there are dwNumberOfItems in the struct
				// so we can take each of those memory pieces and marshal them to WLAN_INTERFACE_INFO

				for (int i = 0; i <= dwNumberOfItems - 1; i++)
				{
					IntPtr pItemList = new IntPtr(start + (i * sizeOfWlanInterfaceInfo));

					InterfaceInfo[i] = (WLAN_INTERFACE_INFO) Marshal.PtrToStructure(pItemList, typeof (WLAN_INTERFACE_INFO));
				}
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WLAN_INTERFACE_INFO
		{
			/// GUID->_GUID
			public Guid InterfaceGuid;

			/// WCHAR[256]
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string strInterfaceDescription;

			/// WLAN_INTERFACE_STATE->_WLAN_INTERFACE_STATE
			public WLAN_INTERFACE_STATE isState;
		}

		/// <summary>
		/// Defines the state of the interface. e.g. connected, disconnected.
		/// </summary>
		public enum WLAN_INTERFACE_STATE
		{
			/// <summary>
			/// wlan_interface_state_not_ready -> 0
			/// </summary>
			wlan_interface_state_not_ready = 0,

			/// <summary>
			/// wlan_interface_state_connected -> 1
			/// </summary>
			wlan_interface_state_connected = 1,

			/// <summary>
			/// wlan_interface_state_ad_hoc_network_formed -> 2
			/// </summary>
			wlan_interface_state_ad_hoc_network_formed = 2,

			/// <summary>
			/// wlan_interface_state_disconnecting -> 3
			/// </summary>
			wlan_interface_state_disconnecting = 3,

			/// <summary>
			/// wlan_interface_state_disconnected -> 4
			/// </summary>
			wlan_interface_state_disconnected = 4,

			/// <summary>
			/// wlan_interface_state_associating -> 5
			/// </summary>
			wlan_interface_state_associating = 5,

			/// <summary>
			/// wlan_interface_state_discovering -> 6
			/// </summary>
			wlan_interface_state_discovering = 6,

			/// <summary>
			/// wlan_interface_state_authenticating -> 7
			/// </summary>
			wlan_interface_state_authenticating = 7,
		}

		public struct WLAN_PROFILE_INFO_LIST
		{
			public uint dwNumberOfItems;
			public uint dwIndex;
			public WLAN_PROFILE_INFO[] ProfileInfo;

			public WLAN_PROFILE_INFO_LIST(IntPtr ppProfileList)
			{
				dwNumberOfItems = (uint) Marshal.ReadInt32(ppProfileList);

				dwIndex = (uint) Marshal.ReadInt32(ppProfileList, 4);

				ProfileInfo = new WLAN_PROFILE_INFO[dwNumberOfItems];

				long start = ppProfileList.ToInt64() + 8;

				int sizeOfWlanProfileInfo = Marshal.SizeOf(typeof(WLAN_PROFILE_INFO));

				for (int i = 0; i < dwNumberOfItems; i++)
				{
					IntPtr pItemList = new IntPtr(start + i * sizeOfWlanProfileInfo);

					ProfileInfo[i] = (WLAN_PROFILE_INFO) Marshal.PtrToStructure(pItemList, typeof (WLAN_PROFILE_INFO));
				}
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct WLAN_PROFILE_INFO
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string strProfileName;

			public uint dwFlags;
		}
	}
}