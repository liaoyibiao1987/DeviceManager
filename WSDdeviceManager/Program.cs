﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WSDdeviceManager.Logger;

namespace WSDdeviceManager
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new WSDService()
            //};
            //ServiceBase.Run(ServicesToRun);
            Test ts = new Test();
            ts.Start();
        }
    }
}
