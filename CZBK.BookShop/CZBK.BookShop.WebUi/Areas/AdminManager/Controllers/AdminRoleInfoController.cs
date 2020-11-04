using CZBK.BookShop.Model;
using CZBK.BookShop.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminRoleInfoController : Controller
    {
        //
        // GET: /AdminManager/AdminRoleInfo/
        IBLL.IRoleService RoleInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取角色信息
        public ActionResult GetRoleInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);
            int totalCount;
            var temp = RoleInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.ID, c => true, true);
            var rows = from t in temp
                       select new { RoleName = t.RoleName, RoleType = t.RoleType, Remark = t.Remark,ID=t.ID };
            return Json(new { rows = rows, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加角色信息
        public ActionResult AddRoleInfo(Role roleInfo)
        {
            roleInfo.SubTime = DateTime.Now;
            roleInfo.DelFlag =(short)DelFlag.Normal;
            RoleInfoService.AddEntity(roleInfo);
            return Content("ok");
        }
        #endregion

    }
}
