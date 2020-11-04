using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminLoginController : Controller
    {
        //
        // GET: /AdminManager/AdminLogin/
        IBLL.IUserInfoService UserInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            string userName = Request["userName"];
            string userPwd=Request["userPwd"];
          var userInfo=  UserInfoService.LoadEntities(u=>u.UserName==userName &&u.UserPass==userPwd).FirstOrDefault();
          if (userInfo != null)
          {
              Session["adminInfo"] = userInfo;
              return Content("ok");
          }
          else
          {
              return Content("no");
          }
        }
    }
}
