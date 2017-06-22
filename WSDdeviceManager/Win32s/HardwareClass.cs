using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static WSDdeviceManager.Win32s.Externs;
using WSDdeviceManager.Logger;

namespace WSDdeviceManager.Win32s
{
    public class HardwareClass
    {
        public static readonly Guid GUID_DEVCLASS_PORTS = new Guid("{0x4d36e978, 0xe325, 0x11ce, {0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18}}");

        #region 属性


        #endregion

        #region 公共事件
        public List<DeviceEntity> GetHiddenDevice()
        {
            WSDLogger.WriterDebugger("Start GetHiddenDevice");
            SP_DEVINFO_DATA DeviceInfoData = new SP_DEVINFO_DATA();
            List<DeviceEntity> list = new List<DeviceEntity>();
            List<DeviceEntity> listCurrent = new List<DeviceEntity>();
            IntPtr hDevInfo = SetupDiGetClassDevs(GUID_DEVCLASS_PORTS, 0, IntPtr.Zero, DIGCF_NOSET);
            if (hDevInfo.ToInt64() == -1)
            {
                WSDLogger.WriterDebugger("SetupDiGetClassDevs: 获取设备错误。");
            }
            else
            {
                DeviceInfoData.cbSize = Marshal.SizeOf(DeviceInfoData);
                DeviceInfoData.devInst = 0;
                DeviceInfoData.classGuid = System.Guid.Empty;
                DeviceInfoData.reserved = 0;
                var devs = SetupDiGetClassDevsExW(GUID_DEVCLASS_PORTS, null, IntPtr.Zero, DIGCF_NOSET, null, null, 0);
                var devsNow = SetupDiGetClassDevsExW(GUID_DEVCLASS_PORTS, null, IntPtr.Zero, DIGCF_PRESENT, null, null, 0);
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
                ScanForHardwareChange();
            }
            WSDLogger.WriterDebugger("End GetHiddenDevice");
            return list;
        }
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
                    List<DeviceEntity> list = new List<DeviceEntity>();
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
                                bool bMatch = false;
                                foreach (string search in match)
                                {
                                    if (DeviceID.ToString().ToLower().Contains(search.ToLower()) == true)
                                    {
                                        bMatch = true;
                                        break;
                                    }
                                }
                                if (bMatch == true)
                                {
                                    OpenClose(hDevInfo, DeviceInfoData, bEnable, true);
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
            }
            return true;
        }

        private bool ScanForHardwareChange()
        {
            int devInst = 0;
            int status;

            //得到设备管理树的根结点 
            status = CM_Locate_DevNodeW(ref devInst, string.Empty, CM_LOCATE_DEVNODE_NORMAL);
            if (status != CR_SUCCESS)
            {
                WSDLogger.WriterError("CM_Locate_DevNodeW 失败");
                return false;
            }

            //刷新 
            status = CM_Reenumerate_DevNode(devInst, CM_REENUMERATE_ASYNCHRONOUS); //CM_REENUMERATE_ASYNCHRONOUS 异步方式可以立即响应
            if (status != CR_SUCCESS)
            {
                WSDLogger.WriterError("CM_Reenumerate_DevNode 失败");
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
        private bool OpenClose(IntPtr hDevInfo, Externs.SP_DEVINFO_DATA devInfoData, bool bEnable, bool isRemove = false)
        {
            try
            {
                int szOfPcp;
                IntPtr ptrToPcp;
                int szDevInfoData;
                IntPtr ptrToDevInfoData;
                Externs.SP_PROPCHANGE_PARAMS SP_PROPCHANGE_PARAMS1 = new Externs.SP_PROPCHANGE_PARAMS();
                SP_REMOVEDEVICE_PARAMS SP_REMOVEDEVICE_PARAMS1 = new SP_REMOVEDEVICE_PARAMS();
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
                    if (isRemove == true)
                    {
                        SP_REMOVEDEVICE_PARAMS1.ClassInstallHeader.cbSize = Marshal.SizeOf(typeof(Externs.SP_CLASSINSTALL_HEADER));
                        SP_REMOVEDEVICE_PARAMS1.ClassInstallHeader.InstallFunction = Externs.DIF_REMOVE;
                        SP_REMOVEDEVICE_PARAMS1.Scope = Externs.DI_REMOVEDEVICE_GLOBAL;
                        SP_REMOVEDEVICE_PARAMS1.HwProfile = 0;
                    }
                    else
                    {
                        SP_PROPCHANGE_PARAMS1.ClassInstallHeader.InstallFunction = Externs.DIF_PROPERTYCHANGE;
                        SP_PROPCHANGE_PARAMS1.StateChange = Externs.DICS_DISABLE;
                        SP_PROPCHANGE_PARAMS1.Scope = Externs.DICS_FLAG_CONFIGSPECIFIC;
                        SP_PROPCHANGE_PARAMS1.HwProfile = 0;
                    }
                }

                szDevInfoData = Marshal.SizeOf(devInfoData);
                ptrToDevInfoData = Marshal.AllocHGlobal(szDevInfoData);
                Marshal.StructureToPtr(devInfoData, ptrToDevInfoData, true);
                bool rslt1 = false;
                bool rstl2 = false;
                if (isRemove == true && bEnable == false)
                {
                    szOfPcp = Marshal.SizeOf(SP_REMOVEDEVICE_PARAMS1);
                    ptrToPcp = Marshal.AllocHGlobal(szOfPcp);
                    Marshal.StructureToPtr(SP_REMOVEDEVICE_PARAMS1, ptrToPcp, true);
                    rslt1 = Externs.SetupDiSetClassInstallParams(hDevInfo, ptrToDevInfoData, ptrToPcp, Marshal.SizeOf(typeof(Externs.SP_REMOVEDEVICE_PARAMS)));
                    rstl2 = Externs.SetupDiCallClassInstaller(Externs.DIF_REMOVE, hDevInfo, ptrToDevInfoData);
                }
                else
                {
                    szOfPcp = Marshal.SizeOf(SP_PROPCHANGE_PARAMS1);
                    ptrToPcp = Marshal.AllocHGlobal(szOfPcp);
                    Marshal.StructureToPtr(SP_PROPCHANGE_PARAMS1, ptrToPcp, true);
                    rslt1 = Externs.SetupDiSetClassInstallParams(hDevInfo, ptrToDevInfoData, ptrToPcp, Marshal.SizeOf(typeof(Externs.SP_PROPCHANGE_PARAMS)));
                    rstl2 = Externs.SetupDiCallClassInstaller(Externs.DIF_PROPERTYCHANGE, hDevInfo, ptrToDevInfoData);
                }
                if ((!rslt1) || (!rstl2))
                {
                    throw new Exception("不能更改设备状态。");
                }
                else
                {
                    Debug.WriteLine("WSDdeviceManager succeed :" + devInfoData.devInst);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("WSDdeviceManager error :" + ex.ToString());
                return false;
            }
        }

        #endregion
    }
}
