using CZBK.HeiMaOA.Model.ActionEqualityCompare;
using CZBK.HeiMaOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
{
    public class HomeController :BaseController
    {
        //
        // GET: /Home/
        IBLL.IUserInfoService userInfoService { get; set; }
        public ActionResult Index()
        {
            if (LoginUser != null)
            {
                ViewData["userName"] = LoginUser.UName;
            }
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }

        #region 找出用户对应的菜单
        public ActionResult GetMenuItmes()
        {
            //1.查询用户已经有的角色.
            var userInfo = userInfoService.LoadEntities(u=>u.ID==LoginUser.ID).FirstOrDefault();
            var userRoles = userInfo.RoleInfo;
            //2.找出对应的权限.
            short menuType=(short)ActionTypeEnum.MenuActionType;
            var userMenuItem =( from r in userRoles
                               from a in r.ActionInfo
                               where a.ActionTypeEnum == menuType
                               select a).ToList();//6
            //3.找出用户特有的权限.
            var userActions = userInfo.R_UserInfo_ActionInfo.ToList();
            //4.找出userActions允许的权限.
            var isPassUserActions = from a in userActions
                                    where a.IsPass == true && a.ActionInfo.ActionTypeEnum == menuType
                                    select a;

            var isPassActions=(from a in isPassUserActions
                              select a.ActionInfo).ToList();

            userMenuItem.AddRange(isPassActions);//合并两个集合.
            //找出禁止权限.
            var isNotPassUserActions = (from a in userActions
                                       where a.IsPass == false
                                       select a.ActionInfoID).ToList();
            //完成禁用权限的过滤
            userMenuItem = userMenuItem.Where(a => !isNotPassUserActions.Contains(a.ID)).ToList();

            //去掉重复的.
          userMenuItem.Distinct(new ActionEqualCompare());
          JsonResult jsonResult = null;
          try
          {
              var result = from u in userMenuItem
                           select new
                           {
                               icon = u.MenuIcon,
                               title = u.ActionInfoName,
                               url = u.Url
                           };


              jsonResult = Json(result, JsonRequestBehavior.AllowGet);
          }
            catch{
            
            }
          return jsonResult;

        }
        #endregion
        

    }
}
