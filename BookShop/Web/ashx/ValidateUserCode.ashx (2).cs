using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ValidateUserCode 的摘要说明
    /// </summary>
    public class ValidateUserCode : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string vcode = context.Request["vcode"];
            //bool isSucess = false;
            //if (context.Session["vCode"] != null)
            //{
            //    string vcode = context.Request["vcode"];
            //    string sysCode = context.Session["vCode"].ToString();
            //    if (sysCode.Equals(vcode, StringComparison.InvariantCultureIgnoreCase))
            //    {
            //        isSucess = true;
            //        context.Session["vCode"]=null;//安全，节省内存。
            //    }
            //}
            context.Response.Write(Common.WebCommon.ValidateCode(vcode)?"ok":"no");

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