using CZBK.ItcastOA.IBLL;
using CZBK.ItcastOA.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo LoginUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
          //  if (Session["userInfo"] == null)
            if (Request.Cookies["sessionId"] == null)
            {
                // filterContext.HttpContext.Response.Redirect("/Login/Index");
                if (Request.Cookies["cp1"] != null)
                {
                    string userName = Request.Cookies["cp1"].Value;//获取Cookie存储的用户名
                    //判断用户是否正确.
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    IUserInfoService userInfoService = (IUserInfoService)ctx.GetObject("UserInfoService");
                    UserInfo userInfo = userInfoService.LoadEntities(u => u.UName == userName).FirstOrDefault();
                    if (!Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        filterContext.Result = Redirect(Url.Action("Index", "Login"));//注意跳转.
                    }
                    LoginUser = userInfo;
                }
                else
                {
                    filterContext.Result = Redirect(Url.Action("Index", "Login"));//注意跳转.
                }

            }
            else//如果有值。取出来
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                object obj=Common.MemcacheHelper.Get(sessionId);//获取Memcache中的数据.
                if (obj != null)//一定要判断.
                {
                   UserInfo userInfo= Common.SerializeHelper.DeserializeToObject<UserInfo>(obj.ToString());//反序列化.
                   LoginUser = userInfo;
                    //模拟滑动过期时间。
                   Common.MemcacheHelper.Set(sessionId, obj, DateTime.Now.AddMinutes(20));

                }
                else
                {
                    filterContext.Result = Redirect(Url.Action("Index", "Login"));//注意跳转.
                }
            }
        }

    }
}
