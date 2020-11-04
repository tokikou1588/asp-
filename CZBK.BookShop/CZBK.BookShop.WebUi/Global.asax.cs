using CZBK.BookShop.WebUi.Models;
using log4net;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CZBK.BookShop.WebUi
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : SpringMvcApplication //System.Web.HttpApplication
    {

       
        protected void Application_Start()
        {
            IndexManager.GetInstance().StartThread();//开始线程，扫描队列
            log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
           //Thread myThread = new Thread();
           // myThread.Priority=ThreadPriority.
           // myThread.Abort();
            //string logFilePath = Server.MapPath("/Log/");
            //ThreadPool.QueueUserWorkItem((a) => {
            //    while (true)//线程必须一值检测队列中是否有数据。
            //    {
            //       // if (MyExceptionAttribute.queueException.Count > 0)
            //        if (MyExceptionAttribute.redisClent.GetListCount("errorMsg")>0)
            //        {
            //          ///  Exception exception = MyExceptionAttribute.queueException.Dequeue();
            //            string errorMsg = MyExceptionAttribute.redisClent.DequeueItemFromList("errorMsg");
            //           // if (exception != null)
            //            if(!string.IsNullOrEmpty(errorMsg))
            //            {
            //                //string filePath = logFilePath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";//日志存储的完整路径.
            //                //File.AppendAllText(filePath, exception.ToString(), Encoding.UTF8);
            //                ILog logger = LogManager.GetLogger("errorMsg");
            //                logger.Error(errorMsg);

            //            }
            //            else
            //            {
            //                Thread.Sleep(3000);
            //            }
            //        }
            //        else
            //        {
            //            Thread.Sleep(3000);//如果队列中没有数据，休息避免造成CPU空转。
            //        }
            //    }
            
            //}, logFilePath);

        }
    }
}