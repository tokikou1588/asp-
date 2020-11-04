using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
{
    public class WF_TempController : BaseController
    {
        //
        // GET: /WF_Temp/
        IBLL.IWF_TempService wf_TempService { get; set; }
        public ActionResult Index()
        {
            short DelFlag=(short)CZBK.HeiMaOA.Model.Enum.DeleteEnumType.Normal;
           var wf_TempList= wf_TempService.LoadEntities(w => w.DelFlag == DelFlag).ToList();
           ViewData["list"] = wf_TempList;
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(WF_Temp wf_Temp)
        {
            wf_Temp.DelFlag = 0;
            wf_Temp.ModfiedOn = DateTime.Now;
            wf_Temp.Remark = "财务审批的流程模板";
            wf_Temp.SubBy = LoginUser.ID;
            wf_Temp.SubTime = DateTime.Now;
            wf_Temp.TempStatus = 0;
            wf_TempService.AddEntity(wf_Temp);
            return RedirectToAction("Index");
        }

    }
}
