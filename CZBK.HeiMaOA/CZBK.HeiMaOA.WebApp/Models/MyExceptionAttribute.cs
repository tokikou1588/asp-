using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 控制器方法中出现异常，会调用该方法捕获异常。
        /// </summary>
        /// <param name="filterContext"></param>
        //public static Queue<Exception> exceptionQueue = new Queue<Exception>();

        public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
        public static IRedisClient redisClent = clientManager.GetClient();

        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
           // exceptionQueue.Enqueue(filterContext.Exception);//将捕获的异常信息写到队列中。
            redisClent.EnqueueItemOnList("errorMsg", filterContext.Exception.ToString());
        
            filterContext.HttpContext.Response.Redirect("/Error.html");//跳转到错误页面.
         
           
        }
    }
}