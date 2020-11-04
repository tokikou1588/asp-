using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminSettingController : Controller
    {
        //
        // GET: /AdminManager/AdminSetting/
        IBLL.ISettingsService SettingService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 展示配置信息
        public ActionResult GetSettingInfo()
        {
            int pageIndex=Request["page"]==null?1:int.Parse(Request["page"]);
            int pageSize=Request["rows"]==null?5:int.Parse(Request["rows"]);
            int totalCount;
          var temp=  SettingService.LoadPageEntities<int>(pageIndex,pageSize,out totalCount,c=>c.Id,c=>true,true);
          var rows = from t in temp
                     select new { Name = t.Name, Value = t.Value,ID=t.Id };
          return Json(new { rows=rows,total=totalCount},JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加配置信息.
        public ActionResult AddSettingInfo(Settings settingInfo)
        {
            SettingService.AddEntity(settingInfo);
            return Content("ok");
        }
        #endregion

        #region 展示修改的配置信息.
        public ActionResult ShowEditInfo()
        {
            int id=int.Parse(Request["id"]);
         ViewData.Model=SettingService.LoadEntities(c=>c.Id==id).FirstOrDefault();
         return View();
        }
        public ActionResult EditSettingInfo(Settings settingInfo)
        {
            SettingService.UpdateEntity(settingInfo);
            //移除缓存.
            Common.CacheHelper.Delete("setting_" + settingInfo.Name);//移除缓存
            return Content("ok");
        }
        #endregion
    }
}
