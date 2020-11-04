using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi.Controllers
{
    public class ActionInfoController : Controller
    {
        //
        // GET: /ActionInfo/
        IBLL.IActionInfoService ActionInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取权限信息
        public ActionResult GetActionInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            int totalCount;
            short delFlag = (short)DelFlagEnum.Normal;
            var actionInfoList = ActionInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, a => a.DelFlag == delFlag, a => a.ID, true);
            var temp = from a in actionInfoList
                       select new
                       {
                           ID = a.ID,
                           ActionInfoName = a.ActionInfoName,
                           Remark = a.Remark,
                           SubTime = a.SubTime,
                           Sort = a.Sort,
                           ActionTypeEnum= a.ActionTypeEnum,
                           Url=a.Url,
                           HttpMethod=a.HttpMethod
                           
                       };
            return Json(new { rows = temp, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 上传文件
        public ActionResult FileUpload()
        {
            HttpPostedFileBase file=Request.Files[0];
            string fileName = Path.GetFileName(file.FileName);
            string fileExt = Path.GetExtension(fileName);
            if (fileExt == ".jpg")
            {
                string dir = "/ImageUp/"+DateTime.Now.Year+"/"+DateTime.Now.Month+"/"+DateTime.Now.Day+"/";
                Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));
                string newfileName = Guid.NewGuid().ToString();
                string fullDir = dir + newfileName + fileExt;
                file.SaveAs(Request.MapPath(fullDir));
                return Content("ok:"+fullDir);
            }
            return Content("no:上传失败!!");
        }
        #endregion

        #region 完成权限的添加
        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            actionInfo.DelFlag = 0;
            actionInfo.ModifiedOn = DateTime.Now.ToString();
            actionInfo.SubTime = DateTime.Now;
            actionInfo.Url = actionInfo.Url.ToLower();
            ActionInfoService.AddEntity(actionInfo);
            return Content("ok");
        }
        #endregion

    }
}
