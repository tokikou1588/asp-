using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        IBLL.IUsersService UserService { get; set; }
        public ActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request["returnUrl"]))//判断能否接收到回传的参数
            {
                ViewData["url"] = Request["returnUrl"];//将接收到的URL参数地址写到隐藏域中。
            }
            //判断Cookie中的数据是否正确,如果正确直接进入登录状态.
            CheckCookieInfo();
            return View();
        }
        #region 判断用户登录.
        public ActionResult UserLogin()
        {
            string userName = Request["txtLoginId"];
            string userPwd = Request["txtLoginPwd"];
            string msg = string.Empty;
            Model.Users user = null;
            bool b=UserService.LoadUserLogin(userName,userPwd,out msg, out user);
            if (b)
            {
                //Session["userInfo"] = user;
                string sessionId = Guid.NewGuid().ToString();//Memcache的key。
                Common.MemcacheHelper.Set(sessionId,Common.SerializeHelper.SerializeToString(user), DateTime.Now.AddMinutes(20));
                Response.Cookies["sessionId"].Value = sessionId;//将自创的SessionId以Cookie发送到浏览器端，存放在浏览器内存中。
                //判断用户是否选择了"记住我"
                if (!string.IsNullOrEmpty(Request["checkMe"]))
                {
                    HttpCookie cookie1 = new HttpCookie("cp1",user.LoginId);
                    HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.Md5String(Common.WebCommon.Md5String(user.LoginPwd)));
                    cookie1.Expires = DateTime.Now.AddDays(3);
                    cookie2.Expires = DateTime.Now.AddDays(3);
                    Response.Cookies.Add(cookie1);
                    Response.Cookies.Add(cookie2);
                }
                if (!string.IsNullOrEmpty(Request["url"]))//接收隐藏域中的值。
                {
                    return Redirect(Request["url"]);
                }
                else
                {
                return RedirectToAction("ShowMsg", "Register", new { msg=msg,url="/UserInfoManager/Index",txt="个人信息管理"});
                }
            }
            else
            {
                ViewData["msg"] = msg;
                return View("Index");
            }
        }
        #endregion

        #region 判断一下Cookie的数据

        public void CheckCookieInfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string name = Request.Cookies["cp1"].Value;
                string pwd = Request.Cookies["cp2"].Value;
                //判断Cookie中存储的用户名是否正确.
              var user= UserService.LoadEntities(u => u.LoginId == name).FirstOrDefault();
              if (user != null)
              {
                  //注册时，密码一定要加密以后存储到数据库中。(如果注册时用户名是经过两次MD5运算以后，写到用户表中的，那么这里直接比较)
                  if (pwd == Common.WebCommon.Md5String(Common.WebCommon.Md5String(user.LoginPwd)))
                  {
                    //  Session["userInfo"] = user;
                      string sessionId = Guid.NewGuid().ToString();//Memcache的key。
                Common.MemcacheHelper.Set(sessionId,Common.SerializeHelper.SerializeToString(user), DateTime.Now.AddMinutes(20));
                Response.Cookies["sessionId"].Value = sessionId;
                      if (!string.IsNullOrEmpty(Request["returnUrl"]))//地址栏中的URL地址参数
                      {
                          Response.Redirect(Request["returnUrl"]);
                      }
                      else
                      {
                          Response.Redirect("/UserInfoManager/Index/");
                      }
                  }
                
              }
              Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
              Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion

        #region 退出登录
        public ActionResult LogOut()
        {
            if (Request.Cookies["sessionId"] != null)
            {
                string sessionId = Request.Cookies["sessionId"].Value;
                Common.MemcacheHelper.Delete(sessionId);
                Response.Cookies["sessionId"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
            return Redirect(Request["currentUrl"]);
        }
        #endregion

        #region 找回密码
        public ActionResult FindPassword()
        {
            return View();
        }
        public ActionResult FindPwd()
        {
            string txtName = Request["txtName"];
            string txtMail=Request["txtMail"];
            //判断用户名是否正确.
           var userInfo=UserService.LoadEntities(u=>u.LoginId==txtName).FirstOrDefault();
           if (userInfo != null)
           {
               if (userInfo.Mail == txtMail)
               {
                   UserService.FindUserPwd(userInfo);
                   return Content("ok:密码已经找回.");
               }
               else
               {
                   return Content("no:邮箱错误!!");
               }
           }
           else
           {
               return Content("no:用户名错误!!");
           }
        }
        #endregion
    }
}
