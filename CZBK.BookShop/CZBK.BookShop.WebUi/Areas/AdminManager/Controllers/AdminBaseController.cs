using CZBK.BookShop.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminBaseController : Controller
    {
        //
        // GET: /AdminManager/AdminBase/

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (Session["adminInfo"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/AdminManager/AdminLogin/Index");
            }
            //判断权限.
            UserInfo userInfo = (UserInfo)Session["adminInfo"];
            string url = Request.Url.AbsolutePath;
            int method = Request.HttpMethod=="get"?2:1;
            short httpMethod = (short)method;
            IApplicationContext ctx = ContextRegistry.GetContext();
                    IBLL.IActionInfoService ActionInfoService = (IBLL.IActionInfoService)ctx.GetObject("ActionInfoService");
                    var actionInfo = ActionInfoService.LoadEntities(a => a.Url == url && a.HttpMethod == httpMethod).FirstOrDefault();
                    if (actionInfo == null)
                    {
                        //filterContext.HttpContext.Response.Redirect("/error.html");
                      //  throw new Exception("没有此权限");
                      return;
                    }

            //判断用户是否具有该权限.
            var count=(from r in userInfo.Role 
                      from g in r.ActionGroup
                      from a in g.ActionInfo
                      where  a.ID==actionInfo.ID
                           select a
                           ).Count();
            if(count<1)
            {
                     filterContext.HttpContext.Response.Redirect("/error.html");
                        return;
            }

        }
    }
}
