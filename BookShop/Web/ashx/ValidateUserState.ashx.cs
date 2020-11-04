using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ValidateUserState 的摘要说明
    /// </summary>
    public class ValidateUserState : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (context.Session["userInfo"] == null)
            {
                //判断Cookie
                if (context.Request.Cookies["cp1"] != null)
                {
                    string userName = context.Request.Cookies["cp1"].Value;
                    BLL.UserManager userManager = new BLL.UserManager();
                    Model.User userInfo = userManager.GetModel(userName);
                    if (!Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        context.Response.Write("no:用户需要登录");
                    }
                    else
                    {
                        context.Response.Write("yes:登录成功!!");
                    }

                }
                else
                {
                    context.Response.Write("no:用户需要登录");
                }
            }
            else
            {
                context.Response.Write("yes:登录成功!!");
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