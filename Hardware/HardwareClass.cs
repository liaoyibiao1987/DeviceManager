using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static Hardware.Externs;

namespace Hardware
{
    public class HardwareClass
    {
        public static readonly Guid GUID_DEVCLASS_PORTS = new Guid("{0x4d36e978, 0xe325, 0x11ce, {0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18}}");


        public string[] DevClassList()
        {
            List<string> HWList = new List<string>();
            try
            {
                UInt32 RequiredSize = 0;
                Guid guid = Guid.Empty;
                Guid[] guids = new Guid[1];
                bool res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);
                if (res == false)
                {
                    guids = new Guid[RequiredSize];
                    res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);

                    if (!res || RequiredSize == 0)
                    {
                        Debug.WriteLine("类型不正确");
                    }
                }
                if (res == true)
                {
                    List<DeviceEntity> list = new List<Hardware.DeviceEntity>();
                    Guid myGUID = System.Guid.Empty;
                    IntPtr hDevInfo = SetupDiGetClassDevs(guids[0], 0, IntPtr.Zero, DIGCF_NOSET);
                    Debug.WriteLine("枚举设备 : " + Externs.GetLastError() + "---" + Marshal.GetLastWin32Error() + hDevInfo.ToInt64());
                    if (hDevInfo.ToInt64() == Externs.INVALID_HANDLE_VALUE)
                    {
                        throw new Exception("Invalid Handle");
                    }
                    else
                    {
                        Externs.SP_DEVINFO_DATA DeviceInfoData;
                        DeviceInfoData = new Externs.SP_DEVINFO_DATA();
                        DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);
                        DeviceInfoData.devInst = 0;
                        DeviceInfoData.classGuid = System.Guid.Empty;
                        DeviceInfoData.reserved = 0;

                        StringBuilder DeviceID = new StringBuilder("", 254);
                        for (uint i = 0; Externs.SetupDiEnumDeviceInfo(hDevInfo, i, DeviceInfoData); i++)
                        {
                            if (CM_Get_Device_IDA(DeviceInfoData.devInst, DeviceID, 254, 0) == CR_SUCCESS)
                            {
                                HWList.Add(DeviceID.ToString());
                            }
                        }
                    }

                    Externs.SetupDiDestroyDeviceInfoList(hDevInfo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("枚举设备列表出错", ex);
            }
            return HWList.ToArray();
        }

        #region 属性
        /// <summary>
        /// 返回所有硬件信息列表
        /// </summary>
        /// <returns></returns>
        public string[] List
        {
            get
            {
                List<string> HWList = new List<string>();
                try
                {
                    //Guid myGUID = System.Guid.Empty;
                    Guid myGUID = new Guid("86e0d1e0-8089-11d0-9ce4-08003e301f73");

                    Externs.HidD_GetHidGuid(out myGUID);
                    //Guid usbCOM = new Guid("4d36e978-e325-11ce-bfc1-08002be10318");
                    // IntPtr hDevInfo = Externs.SetupDiGetClassDevsA(ref myGUID, 0, IntPtr.Zero, Externs.DIGCF_ALLCLASSES | Externs.DIGCF_PRESENT);

                    //IntPtr hDevInfo = Externs.SetupDiGetClassDevs(myGUID, 0, IntPtr.Zero, Externs.DIGCF_ALLCLASSES | Externs.DIGCF_PRESENT | 0x00000008 | 0x00000010);
                    IntPtr hDevInfo = Externs.SetupDiGetClassDevs(myGUID, 0, IntPtr.Zero, Externs.DIGCF_NOSET);
                    if (hDevInfo.ToInt64() == Externs.INVALID_HANDLE_VALUE)
                    {
                        throw new Exception("Invalid Handle");
                    }
                    else
                    {

                    }

                    Debug.WriteLine("枚举设备 : " + Externs.GetLastError() + "---" + Marshal.GetLastWin32Error() + hDevInfo.ToInt64());
                    if (hDevInfo.ToInt64() == Externs.INVALID_HANDLE_VALUE)
                    {
                        throw new Exception("Invalid Handle");
                    }
                    Externs.SP_DEVINFO_DATA DeviceInfoData;
                    DeviceInfoData = new Externs.SP_DEVINFO_DATA();
                    if (Environment.Is64BitOperatingSystem)
                        DeviceInfoData.cbSize = 32;//(16,4,4,4)  
                    else
                        DeviceInfoData.cbSize = 28;
                    DeviceInfoData.devInst = 0;
                    DeviceInfoData.classGuid = System.Guid.Empty;
                    DeviceInfoData.reserved = 0;
                    UInt32 i;
                    StringBuilder DeviceName = new StringBuilder("");
                    DeviceName.Capacity = Externs.MAX_DEV_LEN;
                    for (i = 0; Externs.SetupDiEnumDeviceInfo(hDevInfo, i, DeviceInfoData); i++)
                    {
                        //bool resName = Externs.SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, Externs.SPDRP_HARDWAREID, 0, DeviceName, Externs.MAX_DEV_LEN, IntPtr.Zero);
                        //while (Externs.SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, Externs.SPDRP_DEVICEDESC, 0, DeviceName, Externs.MAX_DEV_LEN, IntPtr.Zero) == false)
                        //{
                        //    int iss = 0;
                        //    //Skip
                        //}
                        bool resName = SetupDiGetDeviceRegistryPropertyA(hDevInfo, DeviceInfoData, SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resName)
                        {
                        }
                        else
                        {
                            HWList.Add(DeviceName.ToString());
                        }

                    }
                    Externs.SetupDiDestroyDeviceInfoList(hDevInfo);
                }
                catch (Exception ex)
                {
                    throw new Exception("枚举设备列表出错", ex);
                }
                return HWList.ToArray();
            }
        }


        /// <summary>
        /// 通过设备类型枚举设备信息
        /// </summary>
        /// <param name="DeviceIndex"></param>
        /// <param name="ClassName"></param>
        /// <param name="DeviceName"></param>
        /// <returns></returns>
        public static void EnumerateDevices(UInt32 DeviceIndex, string ClassName, StringBuilder DeviceName, StringBuilder DeviceID, StringBuilder Mfg, StringBuilder IsInstallDrivers)
        {
            UInt32 RequiredSize = 0;
            Guid guid = Guid.Empty;
            Guid[] guids = new Guid[1];
            IntPtr NewDeviceInfoSet;
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();


            bool res = SetupDiClassGuidsFromNameA(ClassName, ref guids[0], RequiredSize, ref RequiredSize);

            if (RequiredSize == 0)
            {
                //类型不正确
                DeviceName = new StringBuilder("");
                Debug.WriteLine("类型不正确");
            }

            if (res == false)
            {
                guids = new Guid[RequiredSize];
                res = SetupDiClassGuidsFromNameA(ClassName, ref guids[0], RequiredSize, ref RequiredSize);

                if (!res || RequiredSize == 0)
                {
                    //类型不正确
                    DeviceName = new StringBuilder("");
                    Debug.WriteLine("类型不正确");
                }
            }
            //通过类型获取设备信息
            NewDeviceInfoSet = SetupDiGetClassDevsA(ref guids[0], 0, IntPtr.Zero, DIGCF_PRESENT);
            if (NewDeviceInfoSet.ToInt64() == -1)
            {
                //设备不可用
                DeviceName = new StringBuilder("");
                Debug.WriteLine("设备不可用");
            }
            DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);
            DeviceInfoData.devInst = 0;
            DeviceInfoData.classGuid = System.Guid.Empty;
            DeviceInfoData.reserved = 0;

            var devs = SetupDiGetClassDevsExW(guids[0], null, IntPtr.Zero, DIGCF_NOSET, null, null, 0);

            for (uint i = 0; SetupDiEnumDeviceInfo(devs, i, DeviceInfoData); i++)
            {
                DeviceName.Capacity = MAX_DEV_LEN;
                DeviceID.Capacity = MAX_DEV_LEN;
                Mfg.Capacity = MAX_DEV_LEN;
                IsInstallDrivers.Capacity = MAX_DEV_LEN;
                if (!SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero))
                {
                    res = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_DEVICEDESC, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero);
                    if (!res)
                    {
                        //类型不正确
                        SetupDiDestroyDeviceInfoList(devs);
                        Debug.WriteLine("类型不正确");
                    }
                }
                Debug.WriteLine("=============" + DeviceName);
                //设备ID
                bool resHardwareID = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_HARDWAREID, 0, DeviceID, MAX_DEV_LEN, IntPtr.Zero);
                if (!resHardwareID)
                {
                    //设备ID未知
                    DeviceID.Append("未知");
                }
                //设备供应商
                bool resMfg = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_MFG, 0, Mfg, MAX_DEV_LEN, IntPtr.Zero);
                if (!resMfg)
                {
                    //设备供应商未知
                    Mfg.Append("未知");
                }
                //设备是否安装驱动
                bool resIsInstallDrivers = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_DRIVER, 0, IsInstallDrivers, MAX_DEV_LEN, IntPtr.Zero);
                if (!resIsInstallDrivers)
                {
                    //设备是否安装驱动
                    IsInstallDrivers = new StringBuilder("");
                }
            }
            SetupDiDestroyDeviceInfoList(devs);
            //释放当前设备占用内存
            SetupDiDestroyDeviceInfoList(NewDeviceInfoSet);
        }

        public static List<DeviceEntity> GetNomarlDevice()
        {
            UInt32 RequiredSize = 0;
            Guid guid = Guid.Empty;
            Guid[] guids = new Guid[1];
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();
            bool res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);
            if (res == false)
            {
                guids = new Guid[RequiredSize];
                res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);

                if (!res || RequiredSize == 0)
                {
                    Debug.WriteLine("类型不正确");
                }
            }
            if (res == true)
            {
                List<DeviceEntity> list = new List<Hardware.DeviceEntity>();
                Guid myGUID = System.Guid.Empty;
                IntPtr hDevInfo = SetupDiGetClassDevs(guids[0], 0, IntPtr.Zero, DIGCF_PRESENT);
                if (hDevInfo.ToInt64() == -1)
                {
                    Debug.WriteLine("SetupDiGetClassDevs: 获取设备错误。");
                }
                else
                {
                    DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);
                    DeviceInfoData.devInst = 0;
                    DeviceInfoData.classGuid = System.Guid.Empty;
                    DeviceInfoData.reserved = 0;
                    UInt32 i;
                    for (i = 0; SetupDiEnumDeviceInfo(hDevInfo, i, DeviceInfoData); i++)
                    {
                        DeviceEntity entity = new DeviceEntity();
                        StringBuilder DeviceName = new StringBuilder("", 254);
                        bool resName = SetupDiGetDeviceRegistryPropertyA(hDevInfo, DeviceInfoData, SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resName)
                        {
                        }
                        else
                        {
                            entity.DeviceName = DeviceName.ToString();
                        }

                        StringBuilder DeviceID = new StringBuilder("", 254);
                        bool resHardwareID = SetupDiGetDeviceRegistryPropertyA(hDevInfo, DeviceInfoData, SPDRP_HARDWAREID, 0, DeviceID, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resHardwareID)
                        {
                            //设备ID未知
                            DeviceID = new StringBuilder("");
                        }
                        else
                        {
                            entity.DeviceID = DeviceID.ToString();
                        }


                        StringBuilder Mfg = new StringBuilder("", 254);
                        bool resHardwareMfg = SetupDiGetDeviceRegistryPropertyA(hDevInfo, DeviceInfoData, SPDRP_MFG, 0, Mfg, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resHardwareMfg)
                        {
                        }
                        else
                        {
                            entity.DevicePIDVID = Mfg.ToString();
                        }
                        StringBuilder realPath = new StringBuilder("", 2047);
                        bool resHardwareRealPath = SetupDiGetDeviceRegistryPropertyA(hDevInfo, DeviceInfoData, SPDRP_LOCATION_PATHS, 0, realPath, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resHardwareRealPath)
                        {
                        }
                        else
                        {
                            entity.RealTimePath = realPath.ToString();
                        }

                        StringBuilder installState = new StringBuilder("", 64);
                        bool resHardwareInstallState = SetupDiGetDeviceRegistryPropertyA(hDevInfo, DeviceInfoData, SPDRP_INSTALL_STATE, 0, installState, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resHardwareInstallState)
                        {
                        }
                        else
                        {
                            entity.InstallState = installState.ToString();
                        }

                        list.Add(entity);

                    }
                    SetupDiDestroyDeviceInfoList(hDevInfo);
                }
                return list;
            }
            else
            {
                return null;
            }

        }

        public static List<DeviceEntity> GetAllDevice()
        {
            UInt32 RequiredSize = 0;
            Guid guid = Guid.Empty;
            Guid[] guids = new Guid[1];
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();
            bool res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);
            if (res == false)
            {
                guids = new Guid[RequiredSize];
                res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);

                if (!res || RequiredSize == 0)
                {
                    Debug.WriteLine("类型不正确");
                }
            }
            if (res == true)
            {
                List<DeviceEntity> list = new List<Hardware.DeviceEntity>();
                Guid myGUID = System.Guid.Empty;
                IntPtr hDevInfo = SetupDiGetClassDevs(guids[0], 0, IntPtr.Zero, DIGCF_NOSET);
                if (hDevInfo.ToInt64() == -1)
                {
                    Debug.WriteLine("SetupDiGetClassDevs: 获取设备错误。");
                }
                else
                {
                    DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);
                    DeviceInfoData.devInst = 0;
                    DeviceInfoData.classGuid = System.Guid.Empty;
                    DeviceInfoData.reserved = 0;
                    var devs = SetupDiGetClassDevsExW(guids[0], null, IntPtr.Zero, DIGCF_PRESENT, null, null, 0);
                    for (uint i = 0; SetupDiEnumDeviceInfo(devs, i, DeviceInfoData); i++)
                    {


                        DeviceEntity entity = new DeviceEntity();
                        StringBuilder DeviceName = new StringBuilder("", 254);
                        bool resName = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resName)
                        {
                        }
                        else
                        {
                            entity.DeviceName = DeviceName.ToString();
                        }

                        StringBuilder DeviceID = new StringBuilder("", 254);
                        if (CM_Get_Device_IDA(DeviceInfoData.devInst, DeviceID, 254, 0) == CR_SUCCESS)
                        {
                            entity.DeviceID = DeviceID.ToString();
                        }


                        StringBuilder Mfg = new StringBuilder("", 254);
                        bool resHardwareMfg = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_MFG, 0, Mfg, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resHardwareMfg)
                        {
                        }
                        else
                        {
                            entity.DevicePIDVID = Mfg.ToString();
                        }

                        StringBuilder realPath = new StringBuilder("", 2047);
                        bool resHardwareRealPath = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_LOCATION_PATHS, 0, realPath, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resHardwareRealPath)
                        {
                        }
                        else
                        {
                            entity.RealTimePath = realPath.ToString();
                        }

                        StringBuilder installState = new StringBuilder("", 64);
                        bool resHardwareInstallState = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_INSTALL_STATE, 0, installState, MAX_DEV_LEN, IntPtr.Zero);
                        if (!resHardwareInstallState)
                        {
                        }
                        else
                        {
                            entity.InstallState = installState.ToString();
                        }

                        list.Add(entity);
                    }
                    SetupDiDestroyDeviceInfoList(devs);
                }
                return list;
            }
            else
            {
                return null;
            }

        }

        public static List<DeviceEntity> GetHiddenDevice()
        {
            UInt32 RequiredSize = 0;
            Guid guid = Guid.Empty;
            Guid[] guids = new Guid[1];
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();
            bool res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);
            if (res == false)
            {
                guids = new Guid[RequiredSize];
                res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);

                if (!res || RequiredSize == 0)
                {
                    Debug.WriteLine("类型不正确");
                }
            }
            if (res == true)
            {
                List<DeviceEntity> list = new List<Hardware.DeviceEntity>();
                List<DeviceEntity> listCurrent = new List<Hardware.DeviceEntity>();

                Guid myGUID = System.Guid.Empty;
                IntPtr hDevInfo = SetupDiGetClassDevs(guids[0], 0, IntPtr.Zero, DIGCF_NOSET);
                if (hDevInfo.ToInt64() == -1)
                {
                    Debug.WriteLine("SetupDiGetClassDevs: 获取设备错误。");
                }
                else
                {
                    DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);
                    DeviceInfoData.devInst = 0;
                    DeviceInfoData.classGuid = System.Guid.Empty;
                    DeviceInfoData.reserved = 0;
                    var devs = SetupDiGetClassDevsExW(guids[0], null, IntPtr.Zero, DIGCF_NOSET, null, null, 0);
                    var devsNow = SetupDiGetClassDevsExW(guids[0], null, IntPtr.Zero, DIGCF_PRESENT, null, null, 0);
                    if (devsNow.ToInt64() != -1)
                    {
                        for (uint i = 0; SetupDiEnumDeviceInfo(devsNow, i, DeviceInfoData); i++)
                        {
                            DeviceEntity entity = new DeviceEntity();
                            StringBuilder DeviceName = new StringBuilder("", 254);
                            bool resName = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero);
                            if (!resName)
                            {
                            }
                            else
                            {
                                entity.DeviceName = DeviceName.ToString();
                            }

                            StringBuilder DeviceID = new StringBuilder("", 254);
                            if (CM_Get_Device_IDA(DeviceInfoData.devInst, DeviceID, 254, 0) == CR_SUCCESS)
                            {
                                entity.DeviceID = DeviceID.ToString();
                            }
                            listCurrent.Add(entity);
                        }
                    }

                    for (uint i = 0; SetupDiEnumDeviceInfo(devs, i, DeviceInfoData); i++)
                    {
                        DeviceEntity entity = new DeviceEntity();

                        StringBuilder DeviceID = new StringBuilder("", 254);
                        if (CM_Get_Device_IDA(DeviceInfoData.devInst, DeviceID, 254, 0) == CR_SUCCESS)
                        {
                            entity.DeviceID = DeviceID.ToString();
                        }

                        if (listCurrent.Exists(p => p.DeviceID == DeviceID.ToString()))
                        {
                            continue;
                        }
                        else
                        {
                            StringBuilder DeviceName = new StringBuilder("", 254);
                            bool resName = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_FRIENDLYNAME, 0, DeviceName, MAX_DEV_LEN, IntPtr.Zero);
                            if (!resName)
                            {
                            }
                            else
                            {
                                entity.DeviceName = DeviceName.ToString();
                            }

                            StringBuilder Mfg = new StringBuilder("", 254);
                            bool resHardwareMfg = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_MFG, 0, Mfg, MAX_DEV_LEN, IntPtr.Zero);
                            if (!resHardwareMfg)
                            {
                            }
                            else
                            {
                                entity.DevicePIDVID = Mfg.ToString();
                            }

                            StringBuilder realPath = new StringBuilder("", 2047);
                            bool resHardwareRealPath = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_LOCATION_PATHS, 0, realPath, MAX_DEV_LEN, IntPtr.Zero);
                            if (!resHardwareRealPath)
                            {
                            }
                            else
                            {
                                entity.RealTimePath = realPath.ToString();
                            }

                            StringBuilder installState = new StringBuilder("", 64);
                            bool resHardwareInstallState = SetupDiGetDeviceRegistryPropertyA(devs, DeviceInfoData, SPDRP_INSTALL_STATE, 0, installState, MAX_DEV_LEN, IntPtr.Zero);
                            if (!resHardwareInstallState)
                            {
                            }
                            else
                            {
                                entity.InstallState = installState.ToString();
                            }
                            //bool isHidde = !SetupDiSelectDevice(devsNow, DeviceInfoData);
                            entity.IsHiddenDevice = true;
                            list.Add(entity);

                        }

                    }
                    SetupDiDestroyDeviceInfoList(devs);
                    SetupDiDestroyDeviceInfoList(devsNow);
                }
                return list;
            }
            else
            {
                return null;
            }

        }

        #endregion

        #region 公共事件
        /// <summary>
        /// 清理非托管资源
        /// </summary>
        /// <param name="callback"></param>
        public void Dispose(IntPtr callback)
        {
            try
            {
                Externs.UnregisterDeviceNotification(callback);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 设置指定设备的状态
        /// </summary>
        /// <param name="match">设备名称</param>
        /// <param name="bEnable">是否启用</param>
        /// <returns></returns>
        public bool SetState(string[] match, bool bEnable)
        {
            try
            {
                UInt32 RequiredSize = 0;
                Guid guid = Guid.Empty;
                Guid[] guids = new Guid[1];
                bool res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);
                if (res == false)
                {
                    guids = new Guid[RequiredSize];
                    res = SetupDiClassGuidsFromNameA("ports", ref guids[0], RequiredSize, ref RequiredSize);

                    if (!res || RequiredSize == 0)
                    {
                        Debug.WriteLine("类型不正确");
                    }
                }
                if (res == true)
                {
                    List<DeviceEntity> list = new List<Hardware.DeviceEntity>();
                    Guid myGUID = System.Guid.Empty;
                    IntPtr hDevInfo = SetupDiGetClassDevs(guids[0], 0, IntPtr.Zero, DIGCF_NOSET);
                    Debug.WriteLine("枚举设备 : " + Externs.GetLastError() + "---" + Marshal.GetLastWin32Error() + hDevInfo.ToInt64());
                    if (hDevInfo.ToInt64() == Externs.INVALID_HANDLE_VALUE)
                    {
                        throw new Exception("Invalid Handle");
                    }
                    else
                    {
                        Externs.SP_DEVINFO_DATA DeviceInfoData;
                        DeviceInfoData = new Externs.SP_DEVINFO_DATA();
                        DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);
                        DeviceInfoData.devInst = 0;
                        DeviceInfoData.classGuid = System.Guid.Empty;
                        DeviceInfoData.reserved = 0;
                        UInt32 i;
                        StringBuilder DeviceID = new StringBuilder("", 256);
                        DeviceID.Capacity = Externs.MAX_DEV_LEN;
                        for (i = 0; Externs.SetupDiEnumDeviceInfo(hDevInfo, i, DeviceInfoData); i++)
                        {
                            //while (!Externs.SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, Externs.SPDRP_DEVICEDESC, 0, DeviceName, Externs.MAX_DEV_LEN, IntPtr.Zero))
                            //{
                            //}
                            if (CM_Get_Device_IDA(DeviceInfoData.devInst, DeviceID, 254, 0) == CR_SUCCESS)
                            {
                                bool bMatch = true;
                                foreach (string search in match)
                                {
                                    if (!DeviceID.ToString().ToLower().Contains(search.ToLower()))
                                    {
                                        bMatch = false;
                                        break;
                                    }
                                }
                                if (bMatch)
                                {
                                    OpenClose(hDevInfo, DeviceInfoData, bEnable);
                                }
                            }

                        }
                        Externs.SetupDiDestroyDeviceInfoList(hDevInfo);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("枚举设备信息出错！", ex);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 允许一个窗口或者服务接收所有硬件的通知
        /// 注:目前还没有找到一个比较好的方法来处理如果通知服务。
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="UseWindowHandle"></param>
        /// <returns></returns>
        public bool AllowNotifications(IntPtr callback, bool UseWindowHandle)
        {
            try
            {
                Externs.DEV_BROADCAST_DEVICEINTERFACE dbdi = new Externs.DEV_BROADCAST_DEVICEINTERFACE();
                dbdi.dbcc_size = Marshal.SizeOf(dbdi);
                dbdi.dbcc_reserved = 0;
                dbdi.dbcc_devicetype = Externs.DBT_DEVTYP_DEVICEINTERFACE;
                if (UseWindowHandle)
                    Externs.RegisterDeviceNotification(callback, dbdi, Externs.DEVICE_NOTIFY_ALL_INTERFACE_CLASSES | Externs.DEVICE_NOTIFY_WINDOW_HANDLE);
                else
                    Externs.RegisterDeviceNotification(callback, dbdi, Externs.DEVICE_NOTIFY_ALL_INTERFACE_CLASSES | Externs.DEVICE_NOTIFY_SERVICE_HANDLE);
                return true;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return false;
            }
        }

        #endregion

        #region 私有事件
        /// <summary>
        /// 开启或者关闭指定的设备驱动
        /// 注意：该方法目前没有检查是否需要重新启动计算机。^.^
        /// </summary>
        /// <param name="hDevInfo"></param>
        /// <param name="devInfoData"></param>
        /// <param name="bEnable"></param>
        /// <returns></returns>
        private bool OpenClose(IntPtr hDevInfo, Externs.SP_DEVINFO_DATA devInfoData, bool bEnable)
        {
            try
            {
                int szOfPcp;
                IntPtr ptrToPcp;
                int szDevInfoData;
                IntPtr ptrToDevInfoData;
                Externs.SP_PROPCHANGE_PARAMS SP_PROPCHANGE_PARAMS1 = new Externs.SP_PROPCHANGE_PARAMS();
                if (bEnable)
                {
                    SP_PROPCHANGE_PARAMS1.ClassInstallHeader.cbSize = Marshal.SizeOf(typeof(Externs.SP_CLASSINSTALL_HEADER));
                    SP_PROPCHANGE_PARAMS1.ClassInstallHeader.InstallFunction = Externs.DIF_PROPERTYCHANGE;
                    SP_PROPCHANGE_PARAMS1.StateChange = Externs.DICS_ENABLE;
                    SP_PROPCHANGE_PARAMS1.Scope = Externs.DICS_FLAG_GLOBAL;
                    SP_PROPCHANGE_PARAMS1.HwProfile = 0;

                    szOfPcp = Marshal.SizeOf(SP_PROPCHANGE_PARAMS1);
                    ptrToPcp = Marshal.AllocHGlobal(szOfPcp);
                    Marshal.StructureToPtr(SP_PROPCHANGE_PARAMS1, ptrToPcp, true);
                    szDevInfoData = Marshal.SizeOf(devInfoData);
                    ptrToDevInfoData = Marshal.AllocHGlobal(szDevInfoData);

                    if (Externs.SetupDiSetClassInstallParams(hDevInfo, ptrToDevInfoData, ptrToPcp, Marshal.SizeOf(typeof(Externs.SP_PROPCHANGE_PARAMS))))
                    {
                        Externs.SetupDiCallClassInstaller(Externs.DIF_PROPERTYCHANGE, hDevInfo, ptrToDevInfoData);
                    }
                    SP_PROPCHANGE_PARAMS1.ClassInstallHeader.cbSize = Marshal.SizeOf(typeof(Externs.SP_CLASSINSTALL_HEADER));
                    SP_PROPCHANGE_PARAMS1.ClassInstallHeader.InstallFunction = Externs.DIF_PROPERTYCHANGE;
                    SP_PROPCHANGE_PARAMS1.StateChange = Externs.DICS_ENABLE;
                    SP_PROPCHANGE_PARAMS1.Scope = Externs.DICS_FLAG_CONFIGSPECIFIC;
                    SP_PROPCHANGE_PARAMS1.HwProfile = 0;
                }
                else
                {
                    SP_PROPCHANGE_PARAMS1.ClassInstallHeader.cbSize = Marshal.SizeOf(typeof(Externs.SP_CLASSINSTALL_HEADER));
                    SP_PROPCHANGE_PARAMS1.ClassInstallHeader.InstallFunction = Externs.DIF_PROPERTYCHANGE;
                    SP_PROPCHANGE_PARAMS1.StateChange = Externs.DICS_DISABLE;
                    SP_PROPCHANGE_PARAMS1.Scope = Externs.DICS_FLAG_CONFIGSPECIFIC;
                    SP_PROPCHANGE_PARAMS1.HwProfile = 0;
                }
                szOfPcp = Marshal.SizeOf(SP_PROPCHANGE_PARAMS1);
                ptrToPcp = Marshal.AllocHGlobal(szOfPcp);
                Marshal.StructureToPtr(SP_PROPCHANGE_PARAMS1, ptrToPcp, true);
                szDevInfoData = Marshal.SizeOf(devInfoData);
                ptrToDevInfoData = Marshal.AllocHGlobal(szDevInfoData);
                Marshal.StructureToPtr(devInfoData, ptrToDevInfoData, true);

                bool rslt1 = Externs.SetupDiSetClassInstallParams(hDevInfo, ptrToDevInfoData, ptrToPcp, Marshal.SizeOf(typeof(Externs.SP_PROPCHANGE_PARAMS)));
                bool rstl2 = Externs.SetupDiCallClassInstaller(Externs.DIF_PROPERTYCHANGE, hDevInfo, ptrToDevInfoData);
                if ((!rslt1) || (!rstl2))
                {
                    throw new Exception("不能更改设备状态。");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
    }
}
