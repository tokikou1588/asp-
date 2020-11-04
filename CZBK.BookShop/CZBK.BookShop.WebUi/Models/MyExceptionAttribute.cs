using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Models
{
    public class MyExceptionAttribute : HandleErrorAttribute
    {
       // public static Queue<Exception> queueException = new Queue<Exception>();
        public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
        public static IRedisClient redisClent = clientManager.GetClient();

        //可以获取异常信息.
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
           // queueException.Enqueue(filterContext.Exception);//捕获异常信息.
            redisClent.EnqueueItemOnList("errorMsg", filterContext.Exception.ToString());
            filterContext.HttpContext.Response.Redirect("/error.html");//跳转到错误页面.
        }
    }
}