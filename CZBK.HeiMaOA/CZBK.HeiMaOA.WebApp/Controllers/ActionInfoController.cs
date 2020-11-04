﻿using CZBK.HeiMaOA.Model;
using CZBK.HeiMaOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
{
    public class ActionInfoController : BaseController
    {
        //
        // GET: /ActionInfo/
        IBLL.IActionInfoService actionInfoService { get; set; }
        IBLL.IRoleInfoService roleInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取权限的信息
        public ActionResult GetActionInfo()
        {
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            int totalCount;
            short delFlag = (short)DeleteEnumType.Normal;
            var actionInfoList = actionInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, r => r.DelFlag == delFlag, r => r.ID, true);
            var rows = from r in actionInfoList
                       select new { ID = r.ID, ActionInfoName = r.ActionInfoName, Sort = r.Sort, Remark = r.Remark, SubTime = r.SubTime, HttpMethod = r.HttpMethod, ActionTypeEnum = r.ActionTypeEnum, Url = r.Url };
            return Json(new { rows = rows, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加权限信息
        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            actionInfo.DelFlag = 0;
            actionInfo.ModifiedOn = DateTime.Now.ToString();
            actionInfo.SubTime = DateTime.Now;
            actionInfo.Url = actionInfo.Url.ToLower();
            actionInfo.HttpMethod = actionInfo.HttpMethod;
            actionInfoService.AddEntity(actionInfo);
            return Content("ok");
        }
        #endregion

        #region 获取文件数据
        public ActionResult GetMenuIcon()
        {
            HttpPostedFileBase file=Request.Files[0];
            string fileName = System.IO.Path.GetFileName(file.FileName);
            string fileExt = System.IO.Path.GetExtension(fileName);
            if (fileExt == ".jpg")
            {
                string newfileName = Guid.NewGuid().ToString() + fileExt;
                file.SaveAs(Server.MapPath("/MenuIcon/" + newfileName));
                return Content("ok:/MenuIcon/" + newfileName);
                
            }
            else
            {
                return Content("no:");
            }
       
        }
        #endregion

        #region 修改权限信息.
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
           ViewData.Model=actionInfoService.LoadEntities(a=>a.ID==id).FirstOrDefault();
           return View();
        }
        public ActionResult EditActionInfo(ActionInfo actionInfo)
        {
            actionInfo.ModifiedOn = DateTime.Now.ToString();
            actionInfoService.UpdateEntity(actionInfo);
            return Content("ok");

        }
        #endregion

        #region 给权限分配角色信息
        public ActionResult SetActionRole()
        {
            int id = int.Parse(Request["id"]);//权限编号
          var actionInfo=actionInfoService.LoadEntities(a => a.ID == id).FirstOrDefault();//找权限
          ViewBag.ActionInfo = actionInfo;
          short DelFlag = (short)DeleteEnumType.Normal;
          ViewBag.AllRoles = roleInfoService.LoadEntities(r=>r.DelFlag==DelFlag).ToList();
          ViewBag.AllExtRoleIds = (from r in actionInfo.RoleInfo
                                   select r.ID).ToList();//查询出当前权限已经有的角色编号.
          return View();
        }
        [HttpPost]
        public ActionResult SetActionRole(FormCollection collection)
        {
            int actionId = int.Parse(Request["actionId"]);
           string[]AllKeys= Request.Form.AllKeys;
           List<int> list = new List<int>();
           foreach (string key in AllKeys)
           {
               if (key.StartsWith("cba_"))
               {
                   string k = key.Replace("cba_","");
                   list.Add(int.Parse(k));
               }
           }
           actionInfoService.SetActionRoleInfo(actionId, list);
           return Content("ok");
        }
        #endregion

    }
}
