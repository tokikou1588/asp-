using BookShop.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// UserReg 的摘要说明
    /// </summary>
    public class UserReg : IHttpHandler,System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// 注册时表单中的数据提交过来。
        /// 自己完成服务端的数据验证
        /// </summary>
        /// <param name="context"></param>
        BLL.UserManager userManager = new BLL.UserManager();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            if (!CheckUserCode(context))
            {
                context.Response.Write("no:验证码错误!!");
                context.Response.End();
            }
            //if (userManager.GetModel(context.Request["txtUserName"]) != null)
            //{
            //    context.Response.Write("no:用户名已经存在!!");
            //    context.Response.End();
            //}
            context.Session["vCode"] = null;//验证码清空
            string msg = string.Empty;
            context.Response.Write(UserRegister(context,out msg)?"ok:"+msg:"no:"+msg);
            //发送激活链接.
        }
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="context"></param>
        private bool UserRegister(HttpContext context,out string msg)
        {
            Model.User userInfo = new Model.User();
            userInfo.Address = context.Request["txtUserAddress"];
            userInfo.LoginId = context.Request["txtUserName"];
            userInfo.LoginPwd =Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(context.Request["txtUserPwd"]));
            userInfo.Mail = context.Request["txtUserEmail"];
            userInfo.Name = context.Request["txtRealUserName"];
            userInfo.Phone = context.Request["txtUserPhone"];
            userInfo.UserState.Id =Convert.ToInt32(UserStateEnum.NormalState);
          
            int i=userManager.Add(userInfo, out msg);
            if (i > 0)
            {
                context.Session["userInfo"]=userInfo;
                return true;
            }
            return false;
        }

        //验证码校验
        private bool CheckUserCode(HttpContext context)
        {
            string vcode = context.Request["txtValidateCode"];
           return Common.WebCommon.ValidateCode(vcode);
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