using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
       IBLL.IUserInfoService userInfoService { get; set; }
        public ActionResult Index()
        {
            CheckCookieInfo();//校验Cookie的信息
            return View();
        }
        #region 用户登录
        public ActionResult CheckLogin()
        {
            string validateCode = Session["validateCode"] == null ? string.Empty : Session["validateCode"].ToString();
            if (string.IsNullOrEmpty(validateCode))
            {
                return Content("no:验证码错误!");
            }
            Session["validateCode"] = null;
            string requestCode = Request["vCode"];
            if (!requestCode.Equals(validateCode,StringComparison.InvariantCultureIgnoreCase))
            {
                return Content("no:验证码错误!!");
            }
            string userName = Request["LoginCode"];
            string userPwd = Request["LoginPwd"];
         var userInfo =userInfoService.LoadEntities(u => u.UName == userName && u.UPwd == userPwd).FirstOrDefault();//对用户名密码进行过滤.
         if (userInfo == null)
         {
             return Content("no:用户名密码错误!!");
         }
         else
         {
             //Session["userInfo"] = userInfo;
             string sessionId = Guid.NewGuid().ToString();//自己创建的SessionId,作为Memcache的key.
             Common.MemcacheHelper.Set(sessionId,Common.SerializerHelper.SerializerToString(userInfo));//将用户的信息存储到Memcache中。
             Response.Cookies["sessionId"].Value = sessionId;//然后将自创的sessionId以Cookie的形式返回到浏览器，存储到浏览器端的内存中。
             //判断一下用户是否选择了记住我.
             if (!string.IsNullOrEmpty(Request["checkMe"]))
             {
                 HttpCookie cookie1 = new HttpCookie("cp1", userName);
                 HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.Md5String(Common.WebCommon.Md5String(userPwd)));
                 cookie1.Expires = DateTime.Now.AddDays(3);
                 cookie2.Expires = DateTime.Now.AddDays(3);
                 Response.Cookies.Add(cookie1);
                 Response.Cookies.Add(cookie2);
             }
             return Content("ok:");
         }
        }
        #endregion
        #region 展示验证码.
        public ActionResult ValidateCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();
           string code=validateCode.CreateValidateCode(4);
           Session["validateCode"] = code;
           byte[]buffer=validateCode.CreateValidateGraphic(code);
           return File(buffer, "image/jpeg");
        }
        #endregion

        #region 找回密码.
        public ActionResult FindPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FindPwd(FormCollection collection)
        {
            string txtName = Request["txtName"];
            string txtMail = Request["txtMail"];
           var userInfo= userInfoService.LoadEntities(u=>u.UName==txtName).FirstOrDefault();
           if (userInfo != null)
           {
               if (txtMail == "wangchengwei324@126.com")
               {
                   userInfoService.FindUserPwd(userInfo);
                   return Content("密码已经找回,请登录邮箱!!");
               }
               else
               {
                   return Content("邮箱错误");
               }
           }
           else
           {
               return Content("没有此人!!");
           }
        }
        #endregion

        #region 校验Cookie信息
        public void CheckCookieInfo()
        {
            if (Request.Cookies["cp1"] != null && Request.Cookies["cp2"] != null)
            {
                string cookieUserName = Request.Cookies["cp1"].Value;
                string cookieUserPwd = Request.Cookies["cp2"].Value;
               var userInfo= userInfoService.LoadEntities(u=>u.UName==cookieUserName).FirstOrDefault();
               if (userInfo != null)
               {
                   //注意：.，要将用户密码加密以后写到用户表中，如果在添加是已经进行两次MD5运算，那么这里直接比较.
                   string md5Pwd = Common.WebCommon.Md5String(Common.WebCommon.Md5String(userInfo.UPwd));
                   if (md5Pwd == cookieUserPwd)
                   {
                       string sessionId = Guid.NewGuid().ToString();//自己创建的SessionId,作为Memcache的key.
                       Common.MemcacheHelper.Set(sessionId, Common.SerializerHelper.SerializerToString(userInfo));//将用户的信息存储到Memcache中。
                       Response.Cookies["sessionId"].Value = sessionId;//然后将自创的sessionId以Cookie的形式返回到浏览器，存储到浏览器端的内存中。
                       Response.Redirect("/Home/Index");
                   }
               }
                //删除Cookie.
               Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
               Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
        }
        #endregion

        #region 退出用户登录
        public ActionResult LoginOut()
        {
            if (Request.Cookies["sessionId"] != null)
            {
                string key = Request.Cookies["sessionId"].Value;
                Common.MemcacheHelper.Delete(key);
                Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
            }
            return Redirect("/Login/Index");
        }
        #endregion

    }
}
