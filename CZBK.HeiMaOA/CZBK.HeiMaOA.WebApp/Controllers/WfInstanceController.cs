using CZBK.HeiMaOA.Model;
using CZBK.HeiMaOA.WorkFlow;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
{
    public class WfInstanceController : BaseController
    {
        //
        // GET: /WfInstance/
        IBLL.IWF_TempService wf_TempService { get; set; }
        IBLL.IWF_InstanceService wf_InstanceService { get; set; }
        IBLL.IUserInfoService userInfoService { get; set; }
        IBLL.IWF_StepInfoService setpInfoService { get; set; }
        #region 模板列表
          public ActionResult Index()
        {
              short DelFlag=(short)CZBK.HeiMaOA.Model.Enum.DeleteEnumType.Normal;
           ViewData.Model= wf_TempService.LoadEntities(c=>c.DelFlag==DelFlag).ToList();
            return View();
        }
        #endregion
        #region 发起流程
          public ActionResult StartWorkflow()
          {
              int id = int.Parse(Request["id"]);//流程模板的编号.
              var currentTemp = wf_TempService.LoadEntities(w=>w.ID==id).FirstOrDefault();//当前模板
              ViewBag.Temp = currentTemp;
              var userInfoList = from u in userInfoService.LoadEntities(u => u.DelFlag == 0).ToList() select new SelectListItem() {
               Selected=false,
                Text=u.UName,
                Value=u.ID.ToString()
              };
              ViewData["FlowTo"] = userInfoList;//如果ViewData中的名字与DropDownList名字一致会自动填充
              return View();
          }

        [HttpPost]
        [ValidateInput(false)]
          public ActionResult StartWorkflow(int id, WF_Instance wf_Instance)
          {
              //发起流程，参数Id,模板编号。wf_Instance：流程内容
            //先将具体的流程内容（流程实例）插入到流程实例表中。
              wf_Instance.ApplicationId = Guid.Empty;
              wf_Instance.Result = 0;
              wf_Instance.StartedBy = LoginUser.ID;
              wf_Instance.Status = 0;
              wf_Instance.SubTime = DateTime.Now;
              wf_Instance.WF_TempID = id;
              wf_InstanceService.AddEntity(wf_Instance);
            //开始进行工作流程.
          var dirct=  new  Dictionary<string,object>(){{"TempBookMarkName","总监审批"}};
         var application= WorkflowApplicationHelper.CreateWorkflowApplication(new FincalActivity(), dirct);//调用工作流程
         wf_Instance.ApplicationId = application.Id;
         wf_InstanceService.UpdateEntity(wf_Instance);//将启动的工作流的编号更新到流程实例表中.
            //创建第一个步骤.
         WF_StepInfo setpInfo = new WF_StepInfo();
         setpInfo.ChildStepID = 0;
         setpInfo.Comment ="开始财务审批";
         setpInfo.DelFlag = 0;
         setpInfo.IsEndStep = false;
         setpInfo.IsProcessed = true;
         setpInfo.IsStartStep = true;
         setpInfo.ParentStepID = -1;
         setpInfo.ProcessBy = LoginUser.ID;
         setpInfo.ProcessTime = DateTime.Now;
         setpInfo.Remark = "开始进行财务审批了!!";
         setpInfo.SetpName = "开始节点";
         setpInfo.StepResult = 0;
         setpInfo.SubTime = DateTime.Now;
         setpInfo.Title = "开始财务审批";
         setpInfo.WF_InstanceID = wf_Instance.ID;
         setpInfoService.AddEntity(setpInfo);

            //初始总监审批步骤.
         WF_StepInfo masterSetpInfo = new WF_StepInfo();
         masterSetpInfo.ChildStepID = 0;
         masterSetpInfo.Comment =string.Empty;
         masterSetpInfo.DelFlag = 0;
         masterSetpInfo.IsEndStep = false;
         masterSetpInfo.IsProcessed = false;
         masterSetpInfo.IsStartStep = false;
         masterSetpInfo.ParentStepID = setpInfo.ID;
         masterSetpInfo.ProcessBy =int.Parse(Request["FlowTo"]);
         masterSetpInfo.ProcessTime = DateTime.Now;
         masterSetpInfo.Remark = string.Empty;
         masterSetpInfo.SetpName = "总监审批";
         masterSetpInfo.StepResult = 0;
         masterSetpInfo.SubTime = DateTime.Now;
         masterSetpInfo.Title = string.Empty;
         masterSetpInfo.WF_InstanceID = wf_Instance.ID;
         setpInfoService.AddEntity(masterSetpInfo);
         return Redirect("/WfInstance/StartWorkflow?id=" + id);
          }
        #endregion

        #region 我的审批.
        public ActionResult GetMyWorkflows()
        {
            int userId = LoginUser.ID;//登录用户编号.
           
            var setpList=setpInfoService.LoadEntities(s=>s.ProcessBy==userId&&s.IsProcessed==false&&s.DelFlag==0).ToList();

            var allInstanceList =( from s in setpList
                                  select s.WF_Instance).ToList();

            return View(allInstanceList);
        }
        #endregion

        #region 开始进行审批，查看审批内容.
        public ActionResult CheckWF()
        {
            int id =int.Parse(Request["id"]);//流程的编号.
           var instance= wf_InstanceService.LoadEntities(w=>w.ID==id).FirstOrDefault();
           ViewBag.Instance = instance;
           var setp = setpInfoService.LoadEntities(s=>s.WF_InstanceID==id).ToList();
           ViewBag.Steps = setp;
           var userInfoList = from u in userInfoService.LoadEntities(u => u.DelFlag == 0).ToList()
                              select new SelectListItem()
                              {
                                  Selected = false,
                                  Text = u.UName,
                                  Value = u.ID.ToString()
                              };
           ViewData["ProcessBy"] = userInfoList;//如果ViewData中的名字与DropDownList名字一致会自动填充
           return View();
        }
        #endregion

    }
}
