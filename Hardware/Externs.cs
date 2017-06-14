using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Hardware
{
    /// <summary>
    /// 下列所需函数可参考MSDN中与驱动程序相关的API函数
    /// </summary>
    public class Externs
    {
        public const int DIGCF_NOSET = 0x00000000;
        public const int DIGCF_DEFAULT = 0x00000001;
        public const int DIGCF_PRESENT = 0x00000002;
        public const int DIGCF_ALLCLASSES = 0x00000004;
        public const int DIGCF_PROFILE = 0x00000008;
        public const int DIGCF_DEVICEINTERFACE = 0x00000010;

        public const int INVALID_HANDLE_VALUE = -1;
        public const int MAX_DEV_LEN = 1000;
        public const int DEVICE_NOTIFY_WINDOW_HANDLE = (0x00000000);
        public const int DEVICE_NOTIFY_SERVICE_HANDLE = (0x00000001);
        public const int DEVICE_NOTIFY_ALL_INTERFACE_CLASSES = (0x00000004);
        public const int DBT_DEVTYP_DEVICEINTERFACE = (0x00000005);
        public const int DBT_DEVNODES_CHANGED = (0x0007);
        public const int WM_DEVICECHANGE = (0x0219);
        public const int DIF_PROPERTYCHANGE = (0x00000012);
        public const int DICS_FLAG_GLOBAL = (0x00000001);
        public const int DICS_FLAG_CONFIGSPECIFIC = (0x00000002);
        public const int DICS_ENABLE = (0x00000001);
        public const int DICS_DISABLE = (0x00000002);

        public const int SPDRP_DEVICEDESC = (0x00000000);// DeviceDesc (R/W)
        public const int SPDRP_HARDWAREID = (0x00000001);// HardwareID (R/W)
        public const int SPDRP_COMPATIBLEIDS = (0x00000002);// CompatibleIDs (R/W)
        public const int SPDRP_UNUSED0 = (0x00000003);// unused
        public const int SPDRP_SERVICE = (0x00000004);// Service (R/W)
        public const int SPDRP_UNUSED1 = (0x00000005);// unused
        public const int SPDRP_UNUSED2 = (0x00000006);// unused
        public const int SPDRP_CLASS = (0x00000007);// Class (R--tied to ClassGUID)
        public const int SPDRP_CLASSGUID = (0x00000008);// ClassGUID (R/W)
        public const int SPDRP_DRIVER = (0x00000009);// Driver (R/W)
        public const int SPDRP_CONFIGFLAGS = (0x0000000A);// ConfigFlags (R/W)
        public const int SPDRP_MFG = (0x0000000B);// Mfg (R/W)
        public const int SPDRP_FRIENDLYNAME = (0x0000000C);// FriendlyName (R/W)
        public const int SPDRP_LOCATION_INFORMATION = (0x0000000D);// LocationInformation (R/W)
        public const int SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = (0x0000000E);// PhysicalDeviceObjectName (R)
        public const int SPDRP_CAPABILITIES = (0x0000000F);// Capabilities (R)
        public const int SPDRP_UI_NUMBER = (0x00000010);// UiNumber (R)
        public const int SPDRP_UPPERFILTERS = (0x00000011);// UpperFilters (R/W)
        public const int SPDRP_LOWERFILTERS = (0x00000012);// LowerFilters (R/W)
        public const int SPDRP_BUSTYPEGUID = (0x00000013);// BusTypeGUID (R)
        public const int SPDRP_LEGACYBUSTYPE = (0x00000014);// LegacyBusType (R)
        public const int SPDRP_BUSNUMBER = (0x00000015);// BusNumber (R)
        public const int SPDRP_ENUMERATOR_NAME = (0x00000016);// Enumerator Name (R)
        public const int SPDRP_SECURITY = (0x00000017);// Security (R/W, binary form)
        public const int SPDRP_SECURITY_SDS = (0x00000018);// Security=(W, SDS form)
        public const int SPDRP_DEVTYPE = (0x00000019);// Device Type (R/W)
        public const int SPDRP_EXCLUSIVE = (0x0000001A);// Device is exclusive-access (R/W)
        public const int SPDRP_CHARACTERISTICS = (0x0000001B);// Device Characteristics (R/W)
        public const int SPDRP_ADDRESS = (0x0000001C);// Device Address (R)
        public const int SPDRP_UI_NUMBER_DESC_FORMAT = (0X0000001D);// UiNumberDescFormat (R/W)
        public const int SPDRP_DEVICE_POWER_DATA = (0x0000001E);// Device Power Data (R)
        public const int SPDRP_REMOVAL_POLICY = (0x0000001F);// Removal Policy (R)
        public const int SPDRP_REMOVAL_POLICY_HW_DEFAULT = (0x00000020);// Hardware Removal Policy (R)
        public const int SPDRP_REMOVAL_POLICY_OVERRIDE = (0x00000021);// Removal Policy Override (RW)
        public const int SPDRP_INSTALL_STATE = (0x00000022);// Device Install State (R)
        public const int SPDRP_LOCATION_PATHS = (0x00000023);// Device Location Paths (R)
        public const int SPDRP_BASE_CONTAINERID = (0x00000024);// Base ContainerID (R)

        public const int SPDRP_MAXIMUM_PROPERTY = (0x00000025);// Upper bound on ordinals

        /// <summary>
        /// 注册设备或者设备类型，在指定的窗口返回相关的信息
        /// </summary>
        /// <param name="hRecipient"></param>
        /// <param name="NotificationFilter"></param>
        /// <param name="Flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, DEV_BROADCAST_DEVICEINTERFACE NotificationFilter, UInt32 Flags);

        /// <summary>
        /// 通过名柄，关闭指定设备的信息。
        /// </summary>
        /// <param name="hHandle"></param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint UnregisterDeviceNotification(IntPtr hHandle);

        /// <summary>
        /// 获取一个指定类别或全部类别的所有已安装设备的信息
        /// </summary>
        /// <param name="gClass"></param>
        /// <param name="iEnumerator"></param>
        /// <param name="hParent"></param>
        /// <param name="nFlags"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(ref Guid gClass, UInt32 iEnumerator, IntPtr hParent, UInt32 nFlags);

        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern IntPtr SetupDiGetClassDevs(Guid gClass, UInt32 iEnumerator, IntPtr hParent, UInt32 nFlags);

        [DllImport("setupapi.dll")]
        public static extern IntPtr SetupDiGetClassDevsA(ref Guid ClassGuid, UInt32 Enumerator, IntPtr hwndParent, UInt32 Flags);

        /// <summary>
        /// 销毁一个设备信息集合，并且释放所有关联的内存
        /// </summary>
        /// <param name="lpInfoSet"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern int SetupDiDestroyDeviceInfoList(IntPtr lpInfoSet);

        /// <summary>
        /// 枚举指定设备信息集合的成员，并将数据放在SP_DEVINFO_DATA中
        /// </summary>
        /// <param name="lpInfoSet"></param>
        /// <param name="dwIndex"></param>
        /// <param name="devInfoData"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiEnumDeviceInfo(IntPtr lpInfoSet, UInt32 dwIndex, SP_DEVINFO_DATA devInfoData);

        /// <summary>
        /// 获取指定设备的属性
        /// </summary>
        /// <param name="lpInfoSet"></param>
        /// <param name="DeviceInfoData"></param>
        /// <param name="Property"></param>
        /// <param name="PropertyRegDataType"></param>
        /// <param name="PropertyBuffer"></param>
        /// <param name="PropertyBufferSize"></param>
        /// <param name="RequiredSize"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern bool SetupDiGetDeviceRegistryProperty(IntPtr lpInfoSet, SP_DEVINFO_DATA DeviceInfoData, UInt32 Property, UInt32 PropertyRegDataType, StringBuilder PropertyBuffer, UInt32 PropertyBufferSize, IntPtr RequiredSize);

        /// <summary>
        /// 停用设备
        /// </summary>
        /// <param name="DeviceInfoSet"></param>
        /// <param name="DeviceInfoData"></param>
        /// <param name="ClassInstallParams"></param>
        /// <param name="ClassInstallParamsSize"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetupDiSetClassInstallParams(IntPtr DeviceInfoSet, IntPtr DeviceInfoData, IntPtr ClassInstallParams, int ClassInstallParamsSize);

        /// <summary>
        /// 启用设备
        /// </summary>
        /// <param name="InstallFunction"></param>
        /// <param name="DeviceInfoSet"></param>
        /// <param name="DeviceInfoData"></param>
        /// <returns></returns>
        [DllImport("setupapi.dll", CharSet = CharSet.Auto)]
        public static extern Boolean SetupDiCallClassInstaller(UInt32 InstallFunction, IntPtr DeviceInfoSet, IntPtr DeviceInfoData);

        [DllImport("setupapi.dll")]
        public static extern Boolean SetupDiClassGuidsFromNameA(string ClassN, ref Guid guids, UInt32 ClassNameSize, ref UInt32 ReqSize);

        /// <summary>
        /// Gets the GUID that Windows uses to represent HID class devices
        /// </summary>
        /// <param name="gHid">An out parameter to take the Guid</param>
        [DllImport("hid.dll", SetLastError = true)]
        public static extern void HidD_GetHidGuid(out Guid gHid);

        [DllImport("setupapi.dll")]
        public static extern Boolean SetupDiGetDeviceRegistryPropertyA(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoData, UInt32 Property, UInt32 PropertyRegDataType, StringBuilder PropertyBuffer, UInt32 PropertyBufferSize, IntPtr RequiredSize);

        [DllImport("setupapi.dll")]
        public static extern IntPtr SetupDiGetClassDevsExW(Guid ClassGuid, string Enumerator, IntPtr hwndParent, UInt32 Flags, SP_DEVINFO_DATA DeviceInfoSet, string MachineName, int Reserved);


        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();
        /// <summary>
        /// RegisterDeviceNotification所需参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DEV_BROADCAST_HANDLE
        {
            public int dbch_size;
            public int dbch_devicetype;
            public int dbch_reserved;
            public IntPtr dbch_handle;
            public IntPtr dbch_hdevnotify;
            public Guid dbch_eventguid;
            public long dbch_nameoffset;
            public byte dbch_data;
            public byte dbch_data1;
        }

        // WM_DEVICECHANGE message
        [StructLayout(LayoutKind.Sequential)]
        public class DEV_BROADCAST_DEVICEINTERFACE
        {
            public int dbcc_size;
            public int dbcc_devicetype;
            public int dbcc_reserved;
        }

        /// <summary>
        /// 设备信息数据
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SP_DEVINFO_DATA
        {
            public int cbSize;
            public Guid classGuid;
            public int devInst;
            public ulong reserved;
        };

        /// <summary>
        /// 属性变更参数
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SP_PROPCHANGE_PARAMS
        {
            public SP_CLASSINSTALL_HEADER ClassInstallHeader = new SP_CLASSINSTALL_HEADER();
            public int StateChange;
            public int Scope;
            public int HwProfile;
        };

        /// <summary>
        /// 设备安装
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class SP_DEVINSTALL_PARAMS
        {
            public int cbSize;
            public int Flags;
            public int FlagsEx;
            public IntPtr hwndParent;
            public IntPtr InstallMsgHandler;
            public IntPtr InstallMsgHandlerContext;
            public IntPtr FileQueue;
            public IntPtr ClassInstallReserved;
            public int Reserved;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string DriverPath;
        };

        [StructLayout(LayoutKind.Sequential)]
        public class SP_CLASSINSTALL_HEADER
        {
            public int cbSize;
            public int InstallFunction;
        };
    }
}
