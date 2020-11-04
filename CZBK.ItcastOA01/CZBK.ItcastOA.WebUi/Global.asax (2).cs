using CZBK.ItcastOA.WebUi.Models;
using log4net;
using Spring.Web.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CZBK.ItcastOA.WebUi
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : SpringMvcApplication//System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure();//获取Log4Net的配置信息

            //开始一个线程，查看异常队列
            string filePath = Server.MapPath("/Log/");
            ThreadPool.QueueUserWorkItem((a) => {

                while (true)//注意：线程不能结束。后面写到队列中的数据没法处理。
                {
                    if (MyExceptionAttribute.ExceptionQueue.Count() > 0)
                    {
                      // Exception ex= MyExceptionAttribute.ExceptionQueue.Dequeue();//从队列中取出数据.
                        Exception ex=null;
                        bool isResult = MyExceptionAttribute.ExceptionQueue.TryDequeue(out ex);
                       if (ex != null&&isResult)
                       {
                           //string fullPath = filePath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                           //File.AppendAllText(fullPath,ex.ToString());
                           ILog logger = LogManager.GetLogger("errorMsg");
                           logger.Error(ex.ToString());
                       }
                       else
                       {
                           Thread.Sleep(3000);
                       }
                    }
                    else
                    {
                        Thread.Sleep(3000);//避免造成CPU的空转。
                    }
                }
            
            
            
            },filePath);
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}