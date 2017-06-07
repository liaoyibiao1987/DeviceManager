using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Hardware
{
    public class HardwareClass
    {
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
                    Guid myGUID = System.Guid.Empty;
                    Externs.    (out myGUID);
                    IntPtr hDevInfo = Externs.SetupDiGetClassDevsA(ref myGUID, 0, IntPtr.Zero, Externs.DIGCF_ALLCLASSES | Externs.DIGCF_PRESENT);
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
                        bool resName = Externs.SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, Externs.SPDRP_DEVICEDESC, 0, DeviceName, Externs.MAX_DEV_LEN, IntPtr.Zero);
                        //while (Externs.SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, Externs.SPDRP_DEVICEDESC, 0, DeviceName, Externs.MAX_DEV_LEN, IntPtr.Zero) == false)
                        //{
                        //    int iss = 0;
                        //    //Skip
                        //}
                        HWList.Add(DeviceName.ToString());
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
                Guid myGUID = System.Guid.Empty;
                IntPtr hDevInfo = Externs.SetupDiGetClassDevs(ref myGUID, 0, IntPtr.Zero, Externs.DIGCF_ALLCLASSES | Externs.DIGCF_PRESENT);
                if (hDevInfo.ToInt32() == Externs.INVALID_HANDLE_VALUE)
                {
                    return false;
                }
                Externs.SP_DEVINFO_DATA DeviceInfoData;
                DeviceInfoData = new Externs.SP_DEVINFO_DATA();
                DeviceInfoData.cbSize = 28;
                DeviceInfoData.devInst = 0;
                DeviceInfoData.classGuid = System.Guid.Empty;
                DeviceInfoData.reserved = 0;
                UInt32 i;
                StringBuilder DeviceName = new StringBuilder("");
                DeviceName.Capacity = Externs.MAX_DEV_LEN;
                for (i = 0; Externs.SetupDiEnumDeviceInfo(hDevInfo, i, DeviceInfoData); i++)
                {
                    while (!Externs.SetupDiGetDeviceRegistryProperty(hDevInfo, DeviceInfoData, Externs.SPDRP_DEVICEDESC, 0, DeviceName, Externs.MAX_DEV_LEN, IntPtr.Zero))
                    {
                    }
                    bool bMatch = true;
                    foreach (string search in match)
                    {
                        if (!DeviceName.ToString().ToLower().Contains(search.ToLower()))
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
                Externs.SetupDiDestroyDeviceInfoList(hDevInfo);
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
