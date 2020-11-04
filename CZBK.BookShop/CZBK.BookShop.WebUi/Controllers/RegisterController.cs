using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CZBK.BookShop.Common;
using CZBK.BookShop.Model;
using CZBK.BookShop.Model.Enum;
using System.Net.Mail;
namespace CZBK.BookShop.WebUi.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        IBLL.IUsersService UserService { get; set; }
        IBLL.ICheckEmailService CheckEmailService { get;set; }
        public ActionResult Index()
        {

            return View();
        }
        #region 完成用户注册
        public ActionResult Register(Users userInfo)
        {
            if (CheckValidateCode())//判断验证码
            {
                userInfo.UserStateId = (int)DelFlag.Normal;
                string msg = string.Empty;
                if (UserService.AddEntity(userInfo, out msg))
                {
                    //给Session赋值.
                    return Content("ok:" + msg);
                }
                else
                {
                    return Content("no:" + msg);
                }
            }
            else
            {
                return Content("no:验证码错误!!");
            }
        }
        #endregion

        #region 展示验证码
        public ActionResult ShowValidateCode()
        {
            ValidateCode validateCode = new ValidateCode();
            string code = validateCode.CreateValidateCode(4);
            Session["validateCode"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);
            return File(buffer, "image/jpeg");
        }
        #endregion
        #region 判断验证码
        private bool CheckValidateCode()
        {
            bool isExt = false;
            if (Session["validateCode"] != null)
            {
                string txtCode = Request["txtCode"];
                string sysCode = Session["validateCode"].ToString();
                if (sysCode.Equals(txtCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    isExt = true;
                    Session.Remove("validateCode");
                }
            }
            return isExt;
        }
        #endregion

        #region 展示提示信息.
        public ActionResult ShowMsg()
        {
            string msg = !string.IsNullOrEmpty(Request["msg"]) ? Request["msg"] : "暂无信息";
            string txt = !string.IsNullOrEmpty(Request["txt"]) ? Request["txt"] : "列表页面";
            string url = !string.IsNullOrEmpty(Request["url"]) ? Request["url"] : "/BookList/Index";
            ViewBag.Msg = msg;
            ViewBag.Txt = txt;
            ViewBag.Url = url;
            return View();
        }
        #endregion

        #region 激活用户注册信息.
        public ActionResult Active()
        {
            int userId = Convert.ToInt32(Request["userId"]);
           var model= CheckEmailService.LoadEntities(a=>a.Id==userId).FirstOrDefault();
           if (model != null)
           {
               if (model.ActiveCode == Request["activeCode"])
               {
                   model.Actived = true;
                   CheckEmailService.UpdateEntity(model);
                   return Content("用户激活成功!!");
               }
               else
               {
                   return Content("激活码错误!!");
               }
           }
           else
           {
               return Content("此用户不存在");
           }
        }
        #endregion

    }
}
