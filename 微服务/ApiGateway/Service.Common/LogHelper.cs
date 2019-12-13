using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "log4net", Watch = true)]
namespace Service.Common
{
    //日志的公共帮助类
    public static class LogHelper
    {
        private static bool openLog = true;

        #region ErrorLog

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        public static void ErrorLog(ILog log,object msg)
        {
            if (openLog)
            {
                Task.Run(() => log.Error(msg));   //异步
                // Task.Factory.StartNew(() =>log.Error(msg));//  这种异步也可以
                //log.Error(msg);    //这种也行跟你需要，性能越好，越强大，我还是使用异步方式
            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="ex"></param>
        public static void ErrorLog(ILog log, Exception ex)
        {
            if (openLog)
            {
                Task.Run(() => log.Error(ex.Message.ToString() + "/r/n" + ex.Source.ToString() + "/r/n" + ex.TargetSite.ToString() + "/r/n" + ex.StackTrace.ToString()));
            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void ErrorLog(ILog log, object msg, Exception ex)
        {
            if (openLog)
            {
                if (ex != null)
                {
                    Task.Run(() => log.Error(msg, ex));   //异步
                }
                else
                {
                    Task.Run(() => log.Error(msg));   //异步
                }
            }
        }

        #endregion

        #region InfoLog

        /// <summary>
        /// 信息记录
        /// </summary>
        /// <param name="msg"></param>
        public static void InfoLog(ILog log, object msg)
        {
            if (openLog)
            {
                Task.Run(() => log.Info(msg));   //异步
                // Task.Factory.StartNew(() =>log.Error(msg));//  这种异步也可以
                //log.Error(msg);    //这种也行跟你需要，性能越好，越强大，我还是使用异步方式
            }
        }

        /// <summary>
        /// 信息记录
        /// </summary>
        /// <param name="msg"></param>
        public static void InfoLog(ILog log, object msg, Exception ex)
        {
            if (openLog)
            {
                Task.Run(() => log.Info(msg, ex));   //异步
                // Task.Factory.StartNew(() =>log.Error(msg));//  这种异步也可以
                //log.Error(msg);    //这种也行跟你需要，性能越好，越强大，我还是使用异步方式
            }
        }

        #endregion
    }
}
