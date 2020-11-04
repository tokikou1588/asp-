using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi.Controllers
{
    public class RoleInfoController : Controller
    {
        //
        // GET: /RoleInfo/ 
        IBLL.IRoleInfoService RoleInfoService { get; set; }
        IBLL.IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取角色列表数据.
        public ActionResult GetRoleInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            short delFlag=(short)DelFlagEnum.Normal;
          var roleInfoList=RoleInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);
          var temp = from r in roleInfoList
                     select new
                     {
                         ID = r.ID,
                         RoleName = r.RoleName,
                         Remark = r.Remark,
                         SubTime = r.SubTime,
                         Sort = r.Sort
                     };
          return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region 展示添加页面
        public ActionResult ShowAddRole()
        {
            return View();
        }
        #endregion

        #region 完成角色信息的保存.
        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.ModifiedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;
            roleInfo.DelFlag = (short)DelFlagEnum.Normal;
            RoleInfoService.AddEntity(roleInfo);
            return Content("ok");
        }
        #endregion

        #region 展示角色具有权限信息
        public ActionResult ShowRoleAction()
        {
            int roleId = int.Parse(Request["roleId"]);
            var roleInfo = RoleInfoService.LoadEntities(r=>r.ID==roleId).FirstOrDefault();
            ViewBag.RoleInfo = roleInfo;
            var actionInfoList=ActionInfoService.LoadEntities(a=>a.DelFlag==0).ToList();//所有的权限
            //角色具有的权限
            var actionIdList = (from a in roleInfo.ActionInfo
                                select a.ID).ToList();//
            ViewBag.ActionInfoList = actionInfoList;
            ViewBag.ActionIdList = actionIdList;
            return View();
        }
        #endregion
        #region 完成角色权限的分配
        public ActionResult SetRoleActionInfo()
        {
            int roleId = int.Parse(Request["roleId"]);
            string[]allKeys=Request.Form.AllKeys;
            List<int> list = new List<int>();
            foreach (string key in allKeys)
            {
                if (key.StartsWith("cba_"))
                {
                    list.Add(Convert.ToInt32(key.Replace("cba_","")));
                }
            }
            return Content(RoleInfoService.SetRoleActionInfo(roleId, list)?"ok":"no");
        }
        #endregion

    }
}
