using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminActionInfoController : Controller
    {
        //
        // GET: /AdminManager/AdminActionInfo/
        IBLL.IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetActionInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);
            int totalCount;
            var temp = ActionInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.ID, c => true, true);
            var rows = from t in temp
                       select new { ActionInfoName = t.ActionInfoName, HttpMethod = t.HttpMethod, Remark = t.Remark, ID = t.ID, Url=t.Url, IsMenu=t.IsMenu };
            return Json(new { rows = rows, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        //#region 给权限分组
        //public ActionResult SetActionInfoGroup()
        //{
        //    string actionIds = Request["actionIds"];//1,5,6
        //    //筛选出所有的权限组.


        //}
        //#endregion

    }
}
