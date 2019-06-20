using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microcomm
{
    public static class LogUtil
    {

        

        public static ILog CreateLogger(string logName)
        {
            log4net.Config.XmlConfigurator.Configure();
            return log4net.LogManager.GetLogger(logName);
        }
    }
}
