using CZBK.HeiMaOA.WebApp.Models;
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

namespace CZBK.HeiMaOA.WebApp
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : SpringMvcApplication//System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IndexManager.GetInstance().CreateThread();//开启线程，扫描队列获取图书的信息写到Lucene.Net中。
            log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string fileLogPath = Server.MapPath("/Log/");//指定用来保存错误日志文件的文件夹路径.

        
            //开启一个线程扫描日志队列
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)//不断扫描日志队列.
                {
                    //  if (MyExceptionAttribute.exceptionQueue.Count() > 0)//判断队列中是否有数据
                    if (MyExceptionAttribute.redisClent.GetListCount("errorMsg") > 0)
                    {
                        //Exception ex= MyExceptionAttribute.exceptionQueue.Dequeue();//出队,
                        string errorMsg = MyExceptionAttribute.redisClent.DequeueItemFromList("errorMsg");
                        if (!string.IsNullOrEmpty(errorMsg))
                        {
                            //string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                            //File.AppendAllText(fileLogPath + fileName, ex.ToString(), Encoding.Default);//将异常写到文件中。

                            ILog logger = LogManager.GetLogger("errorMsg");
                            logger.Error(errorMsg);//将异常信息写到磁盘上.
                        }
                        else
                        {
                            Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Thread.Sleep(3000);//如果队列中没有数据，让当前线程休息，避免造成CUP空转.
                    }
                }


            }, fileLogPath);
        }
    }
}