using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// FindPwd 的摘要说明
    /// </summary>
    public class FindPwd : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
             string mail=context.Request["mail"];
            //判断邮箱是否存在.
             BLL.UserManager userManager = new BLL.UserManager();
             Model.User userInfo = userManager.GetUserByMail(mail);
             if (userInfo != null)
             {
                 userManager.GetUserPwd(userInfo);
                 context.Response.Write("yes:密码已经发送到邮箱");
             }
             else
             {
                 context.Response.Write("no:邮箱不存在!!");
             }
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