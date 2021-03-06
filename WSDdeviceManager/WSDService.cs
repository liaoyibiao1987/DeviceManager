﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WSDdeviceManager.Logger;
using WSDdeviceManager.Win32s;

namespace WSDdeviceManager
{
    public partial class WSDService : ServiceBase
    {
        HardwareClass hc;
        public WSDService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                WSDLogger.WriterDebugger("OnStart");
                hc = new HardwareClass();
                hc.AllowNotifications(this.ServiceHandle, true);
                MaintainPort();
            }
            catch (Exception e)
            {
                WSDLogger.WriterDebugger("OnStart 发生错误" + e.ToString());
            }
        }

        private void MaintainPort()
        {
            WSDLogger.WriterDebugger("Start MaintainPort");
            List<DeviceEntity> list = hc.GetHiddenDevice();
            if (list != null && list.Count > 0)
            {
                IEnumerable<string> Ematchs = list.Select(p => p.DeviceID);
                if (Ematchs != null && Ematchs.Count() > 0)
                {
                    try
                    {
                        hc.SetState(Ematchs.ToArray(), false);
                    }
                    catch (Exception es)
                    {
                        WSDLogger.WriterDebugger("MaintainPortCOM端口出错." + es.ToString());
                    }
                }
            }
            else
            {
                WSDLogger.WriterDebugger("没有发现隐藏COM端口.");
            }
            WSDLogger.WriterDebugger("End MaintainPort");
        }

        protected override void OnStop()
        {
            try
            {
                //  清理非托管资源
                hc.Dispose(this.ServiceHandle);
                hc = null;
            }
            catch (Exception e)
            {
                Debug.WriteLine("OnStop 发生错误" + e.ToString());
            }
        }
    }
}
