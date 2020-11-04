using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
      //  private static object obj = new object();
        public static ConcurrentQueue<Exception> ExceptionQueue = new ConcurrentQueue<Exception>();//定义队列
      
        /// <summary>
        /// 在该方法中捕获异常。
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {

            base.OnException(filterContext);
            Exception ex = filterContext.Exception;//捕获异常信息。
            //将异常信息写到队列中。
            ExceptionQueue.Enqueue(ex);
            //跳转到错误页面.
            filterContext.HttpContext.Response.Redirect("/Error.html");
           
           // lock (obj)
           // {
                //读写文件。


            //}

        }
    }
}