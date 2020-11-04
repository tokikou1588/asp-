using CZBK.BookShop.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminActionGroupController : Controller
    {
        //
        // GET: /AdminManager/AdminActionGroup/
        IBLL.IActionGroupService ActionGroupService { get; set; }
        IBLL.IRoleService RoleInfoService { get; set; }
        IBLL.IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 加载权限组
        public ActionResult GetActionGroupInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);
            int totalCount;
            var temp = ActionGroupService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.ID, c => true, true);
            var rows = from t in temp
                       select new { GroupName = t.GroupName, Sort = t.Sort, GroupType = t.GroupType, ID = t.ID};
            return Json(new { rows = rows, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 为权限组分配角色.
        public ActionResult SetGroupRoleInfo()
        {
            int groupId = int.Parse(Request["grouId"]);
            var groupInfo = ActionGroupService.LoadEntities(a=>a.ID==groupId).FirstOrDefault();
            ViewData["groupInfo"] = groupInfo;
            short delFlag=(short)DelFlag.Normal;
          var roleInfoList= RoleInfoService.LoadEntities(r => r.DelFlag == delFlag).ToList();
          var groupExtRoleIdList = (from a in groupInfo.Role
                                    select a.ID).ToList();
          ViewData["roleInfoList"] = roleInfoList;
          ViewData["groupExtRoleIdList"] = groupExtRoleIdList;
          return View();
        }
        public ActionResult SetGroupRole()
        {
           string[]keys= Request.Form.AllKeys;
           int groupId = int.Parse(Request["groupId"]);
            List<int> roleIdList = new List<int>();
            foreach (string key in keys)
            {
                if (key.StartsWith("cba_"))
                {
                    roleIdList.Add(Convert.ToInt32(key.Replace("cba_","")));
                }
            }
            if (ActionGroupService.SetActionGroupRoleInfo(groupId, roleIdList))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 给权限分组
        public ActionResult SetActionInfoGroup()
        {
            int groupId = int.Parse(Request["groupId"]);
            var groupInfo = ActionGroupService.LoadEntities(g=>g.ID==groupId).FirstOrDefault();
            ViewData["groupInfo"] = groupInfo;
            //获取所有的权限
            short delFlag = (short)DelFlag.Normal;
           var actionInfoList= ActionInfoService.LoadEntities(a => a.DelFalg == delFlag).ToList();
            //获取权限已有的权限.
           var actionInfoExtIdList = (from a in groupInfo.ActionInfo
                                      select a.ID).ToList();
           ViewData["actionInfoList"] = actionInfoList;
           ViewData["actionInfoExtIdList"] = actionInfoExtIdList;
           return View();
        }
        public ActionResult SetActionGroupInfo(int groupId)
        {
            string[] keys = Request.Form.AllKeys;//获取表单(form)所有name属性的值.
            List<int> list = new List<int>();
            foreach (string key in keys)
            {
                if (key.StartsWith("cba_"))
                {
                    list.Add(Convert.ToInt32(key.Replace("cba_", "")));
                }
            }
            if (ActionGroupService.SetActionInfo(groupId, list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
    }
}
