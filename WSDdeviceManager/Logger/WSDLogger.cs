using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace WSDdeviceManager.Logger
{
    public class WSDLogger
    {
        private static ILog Logger;
        static WSDLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger = log4net.LogManager.GetLogger("WSDService");//获取一个日志记录器
            Logger.Info(DateTime.Now.ToString() + ": WSDLogger success");//写入一条新log

        }
        public static void WriterDebugger(string str, params object[] objs)
        {
            Logger.Debug(str);
        }
        public static void WriterError(string str, params object[] objs)
        {
            Logger.Error(str);
        }
    }
}
