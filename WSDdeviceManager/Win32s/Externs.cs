using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WSDdeviceManager.Win32s
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



        public const int CR_SUCCESS = (0x00000000);
        public const int CR_DEFAULT = (0x00000001);
        public const int CR_OUT_OF_MEMORY = (0x00000002);
        public const int CR_INVALID_POINTER = (0x00000003);
        public const int CR_INVALID_FLAG = (0x00000004);
        public const int CR_INVALID_DEVNODE = (0x00000005);
        public const int CR_INVALID_DEVINST = CR_INVALID_DEVNODE;
        public const int CR_INVALID_RES_DES = (0x00000006);
        public const int CR_INVALID_LOG_CONF = (0x00000007);
        public const int CR_INVALID_ARBITRATOR = (0x00000008);
        public const int CR_INVALID_NODELIST = (0x00000009);
        public const int CR_DEVNODE_HAS_REQS = (0x0000000A);
        public const int CR_DEVINST_HAS_REQS = CR_DEVNODE_HAS_REQS;
        public const int CR_INVALID_RESOURCEID = (0x0000000B);
        public const int CR_DLVXD_NOT_FOUND = (0x0000000C);
        public const int CR_NO_SUCH_DEVNODE = (0x0000000D);
        public const int CR_NO_SUCH_DEVINST = CR_NO_SUCH_DEVNODE;
        public const int CR_NO_MORE_LOG_CONF = (0x0000000E);
        public const int CR_NO_MORE_RES_DES = (0x0000000F);
        public const int CR_ALREADY_SUCH_DEVNODE = (0x00000010);
        public const int CR_ALREADY_SUCH_DEVINST = CR_ALREADY_SUCH_DEVNODE;
        public const int CR_INVALID_RANGE_LIST = (0x00000011);
        public const int CR_INVALID_RANGE = (0x00000012);
        public const int CR_FAILURE = (0x00000013);
        public const int CR_NO_SUCH_LOGICAL_DEV = (0x00000014);
        public const int CR_CREATE_BLOCKED = (0x00000015);
        public const int CR_NOT_SYSTEM_VM = (0x00000016);
        public const int CR_REMOVE_VETOED = (0x00000017);
        public const int CR_APM_VETOED = (0x00000018);
        public const int CR_INVALID_LOAD_TYPE = (0x00000019);
        public const int CR_BUFFER_SMALL = (0x0000001A);
        public const int CR_NO_ARBITRATOR = (0x0000001B);
        public const int CR_NO_REGISTRY_HANDLE = (0x0000001C);
        public const int CR_REGISTRY_ERROR = (0x0000001D);
        public const int CR_INVALID_DEVICE_ID = (0x0000001E);
        public const int CR_INVALID_DATA = (0x0000001F);
        public const int CR_INVALID_API = (0x00000020);
        public const int CR_DEVLOADER_NOT_READY = (0x00000021);
        public const int CR_NEED_RESTART = (0x00000022);
        public const int CR_NO_MORE_HW_PROFILES = (0x00000023);
        public const int CR_DEVICE_NOT_THERE = (0x00000024);
        public const int CR_NO_SUCH_VALUE = (0x00000025);
        public const int CR_WRONG_TYPE = (0x00000026);
        public const int CR_INVALID_PRIORITY = (0x00000027);
        public const int CR_NOT_DISABLEABLE = (0x00000028);
        public const int CR_FREE_RESOURCES = (0x00000029);
        public const int CR_QUERY_VETOED = (0x0000002A);
        public const int CR_CANT_SHARE_IRQ = (0x0000002B);
        public const int CR_NO_DEPENDENT = (0x0000002C);
        public const int CR_SAME_RESOURCES = (0x0000002D);
        public const int CR_NO_SUCH_REGISTRY_KEY = (0x0000002E);
        public const int CR_INVALID_MACHINENAME = (0x0000002F);
        public const int CR_REMOTE_COMM_FAILURE = (0x00000030);
        public const int CR_MACHINE_UNAVAILABLE = (0x00000031);
        public const int CR_NO_CM_SERVICES = (0x00000032);
        public const int CR_ACCESS_DENIED = (0x00000033);
        public const int CR_CALL_NOT_IMPLEMENTED = (0x00000034);
        public const int CR_INVALID_PROPERTY = (0x00000035);
        public const int CR_DEVICE_INTERFACE_ACTIVE = (0x00000036);
        public const int CR_NO_SUCH_DEVICE_INTERFACE = (0x00000037);
        public const int CR_INVALID_REFERENCE_STRING = (0x00000038);
        public const int CR_INVALID_CONFLICT_LIST = (0x00000039);
        public const int CR_INVALID_INDEX = (0x0000003A);
        public const int CR_INVALID_STRUCTURE_SIZE = (0x0000003B);
        public const int NUM_CR_RESULTS = (0x0000003C);


        public const int DIF_SELECTDEVICE = 0x00000001;
        public const int DIF_INSTALLDEVICE = 0x00000002;
        public const int DIF_ASSIGNRESOURCES = 0x00000003;
        public const int DIF_PROPERTIES = 0x00000004;
        public const int DIF_REMOVE = 0x00000005;
        public const int DIF_FIRSTTIMESETUP = 0x00000006;
        public const int DIF_FOUNDDEVICE = 0x00000007;
        public const int DIF_SELECTCLASSDRIVERS = 0x00000008;
        public const int DIF_VALIDATECLASSDRIVERS = 0x00000009;
        public const int DIF_INSTALLCLASSDRIVERS = 0x0000000A;
        public const int DIF_CALCDISKSPACE = 0x0000000B;
        public const int DIF_DESTROYPRIVATEDATA = 0x0000000C;
        public const int DIF_VALIDATEDRIVER = 0x0000000D;
        public const int DIF_DETECT = 0x0000000F;
        public const int DIF_INSTALLWIZARD = 0x00000010;

        public const int DI_REMOVEDEVICE_GLOBAL = 0x00000001;
        public const int DI_REMOVEDEVICE_CONFIGSPECIFIC = 0x00000002;

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
        public static extern IntPtr SetupDiGetClassDevsExW(Guid ClassGuid, string Enumerator, IntPtr hwndParent, UInt32 Flags, SP_DEVINFO_DATA DeviceInfoDate, string MachineName, int Reserved);

        [DllImport("setupapi.dll")]
        public static extern bool SetupDiSelectDevice(IntPtr DeviceInfoSet, SP_DEVINFO_DATA DeviceInfoDate);


        [DllImport("setupapi.dll", SetLastError = true)]
        public static extern int CM_Get_Device_ID_Size(out int pulLen, UInt32 dnDevInst, int flags = 0);

        [DllImport("setupapi.dll")]
        public static extern int CM_Get_Device_IDA(int dnDevInst, StringBuilder Buffer, int BufferLen, int ulFlags);



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

        [StructLayout(LayoutKind.Sequential)]
        public class SP_REMOVEDEVICE_PARAMS
        {
            public SP_CLASSINSTALL_HEADER ClassInstallHeader = new SP_CLASSINSTALL_HEADER();
            public int Scope;
            public int HwProfile;
        }
    }
}
