using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ValidateUserMail 的摘要说明
    /// </summary>
    public class ValidateUserMail : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
             string userMail=context.Request["userMail"];
             BLL.UserManager userManager = new BLL.UserManager();
             context.Response.Write(userManager.ValidateUserMail(userMail)?"ok":"no");
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