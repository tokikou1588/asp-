using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi.Controllers
{
    public class HomeController : BaseController //Controller
    {
        //
        // GET: /Home/
        IBLL.IUserInfoService UserInfoService { get; set; }
        public ActionResult Index()
        {
            ViewBag.UserName = LoginUser.UName;
            return View();
        }
        public ActionResult HomePage()
        {
            return View();
        }
        #region 过滤登录用户所有的菜单权限
        public ActionResult GetMenu()
        {
            //1:根据用户-->角色-->权限这条线获取登录用户所有的菜单权限，放在一个集合中.
            int userId = LoginUser.ID;
            var userInfo = UserInfoService.LoadEntities(u=>u.ID==userId).FirstOrDefault();//获取登录用户信息
               //1.2 获取登录用户的所有的角色
            var loginUserRole = userInfo.RoleInfo;
              //1.3 获取角色对应的菜单权限。
            short actionType=(short)ActionTypeEnum.MenuAction;
            var loginUserAction = (from r in loginUserRole
                                   from a in r.ActionInfo
                                   where a.ActionTypeEnum == actionType
                                   select a).ToList();
            //2:根据用户-->权限这条线查找登录用户具有的菜单权限，放在一个集合中.

            //3:将两个集合合并成一个集合。
            //4:对合并后的集合进行过滤，过滤掉哪些禁用的权限。
            //5:去掉重复的权限信息。
            //6:对最后的集合生成JSON返回。（）
            return Content("ok");
        }
        #endregion
       

    }
}
