using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminHomeController :AdminBaseController  //AdminBaseController//
    {
        //
        // GET: /AdminManager/AdminHome/

        public ActionResult Index()
        {
            return View();
        }
        #region 获取登录用户具有的菜单权限
        public ActionResult GetMenuItem()
        {
            UserInfo userInfo = (UserInfo)Session["adminInfo"];
            short groupType = 1;//菜单组
            var groupInfo = from r in userInfo.Role
                        from g in r.ActionGroup
                        where g.GroupType == groupType
                        select g;
            var MenuItem = from g in groupInfo
                           select new
                           {
                               ID = g.ID,
                               GroupName = g.GroupName,
                               Sort = g.Sort,
                               MenuItems = from m in g.ActionInfo where m.IsMenu == true select new { ID = m.ID, ActionName = m.ActionInfoName, Url = m.Url }
                           };
            return Json(MenuItem,JsonRequestBehavior.AllowGet);
       
        }
        #endregion
        

    }
}
