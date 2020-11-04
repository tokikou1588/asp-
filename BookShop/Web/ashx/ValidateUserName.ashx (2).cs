using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ValidateUserName 的摘要说明
    /// </summary>
    public class ValidateUserName : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string userName=context.Request["userName"];
            BLL.UserManager userManager = new BLL.UserManager();
            context.Response.Write(userManager.GetModel(userName)!=null?"ok":"no");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}