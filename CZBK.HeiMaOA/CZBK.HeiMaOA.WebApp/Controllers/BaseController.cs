using CZBK.HeiMaOA.IBLL;
using CZBK.HeiMaOA.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        //执行控制器方法之前先执行该方法。
        //获取自定义的sessionId的值，然后从memcache中取出.
        public UserInfo LoginUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            bool isExt = false;
            if (Request.Cookies["sessionId"] != null)
            {
                string sessionId = Request.Cookies["sessionId"].Value;//获取自定义的sessionId.
                object obj= Common.MemcacheHelper.Get(sessionId);
                if (obj != null)
                {
                   LoginUser= Common.SerializerHelper.DeserializeToObject<UserInfo>(obj.ToString());
                    isExt = true;
                    if (LoginUser.UName == "itcast")
                    {
                        return;
                    }
                    //完成权限过滤.
                    string requestUrl = Request.Url.AbsolutePath.ToLower();//获取URL地址.
                    string requestHttpMethod = Request.HttpMethod;
                    IApplicationContext ctx = ContextRegistry.GetContext(); //创建容器.
                    IUserInfoService userInfoService = (IUserInfoService)ctx.GetObject("userInfoService");
                    IActionInfoService actionInfoService = (IActionInfoService)ctx.GetObject("actionInfoService");
                  var currentAction= actionInfoService.LoadEntities(a=>a.Url==requestUrl&&a.HttpMethod==requestHttpMethod).FirstOrDefault();//根据URL地址与请求方式找出具体的权限.
                  if (currentAction == null)
                  {
                      Response.Redirect("/Error.html");
                      return;
                  }
                    //通过1号线进行校验.
                var currentUserInfo= userInfoService.LoadEntities(u => u.ID == LoginUser.ID).FirstOrDefault();//登录用户
                var actions = currentUserInfo.R_UserInfo_ActionInfo.Where(r => r.ActionInfoID == currentAction.ID).FirstOrDefault();//判断登录用户是否有权限
                if (actions != null)
                {
                    if (actions.IsPass == true)
                    {
                        return;
                    }
                    else
                    {
                        Response.Redirect("/Error.html");
                        return;
                    }
                }
                    //走2号线校验.
                var currentUserRoles = currentUserInfo.RoleInfo;
                var currentUserActions =from a in currentUserRoles
                                         select a.ActionInfo;
                var count = (from a in currentUserActions
                            from b in a
                            where b.ID == currentAction.ID
                            select b).Count();
                if (count < 1)
                {
                    Response.Redirect("/Error.html");
                    return;
                }
                    //走3条线.

                             
                }
            }
            if (!isExt)//用户没有登录
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
