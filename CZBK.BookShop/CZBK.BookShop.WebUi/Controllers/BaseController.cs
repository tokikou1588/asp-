using CZBK.BookShop.Model;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public Users LoginUser { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
           // if (Session["userInfo"] == null)
            if (Request.Cookies["sessionId"]==null)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");
                if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
                {
                    string name = Request.Cookies["cp1"].Value;
                    string pwd = Request.Cookies["cp2"].Value;
                    IApplicationContext ctx = ContextRegistry.GetContext();
                    IBLL.IUsersService UserService = (IBLL.IUsersService)ctx.GetObject("UserService");
                    var user = UserService.LoadEntities(u => u.LoginId == name).FirstOrDefault();
                    if (user != null)
                    {
                        if (pwd == Common.WebCommon.Md5String(Common.WebCommon.Md5String(user.LoginPwd)))
                        {
                           // Session["userInfo"] = user;
                            string sessionId=Guid.NewGuid().ToString();
                            Common.MemcacheHelper.Set(sessionId, Common.SerializeHelper.SerializeToString(user), DateTime.Now.AddMinutes(20));
                            Response.Cookies["sessionId"].Value = sessionId;
                            GetLoginUserInfo();
                            return;
                        }
                    }
                   
                        Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
                }
 
            }
            else//表示Session中有值
            {
                GetLoginUserInfo();
                return;
            }
            Common.WebCommon.GoPage();
            
            
           
        }
        //从Memcache获取登录用户登录的信息
        private void GetLoginUserInfo()
        {
            string sessionId = Request.Cookies["sessionId"].Value;
           object obj= Common.MemcacheHelper.Get(sessionId);
           if (obj != null)
           {
               var user = Common.SerializeHelper.DeserializeToObject<Users>(obj.ToString());
               LoginUser = user;
               //模拟了Session的滑动过期时间
               Common.MemcacheHelper.Set(sessionId, Common.SerializeHelper.SerializeToString(user), DateTime.Now.AddMinutes(20));
               Response.Cookies["sessionId"].Value = sessionId;
           }
           else
           {
               Common.WebCommon.GoPage();
           }
        }
    }
}
