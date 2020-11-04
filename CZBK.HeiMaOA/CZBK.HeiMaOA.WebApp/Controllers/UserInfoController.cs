using CZBK.HeiMaOA.Model;
using CZBK.HeiMaOA.Model.Enum;
using CZBK.HeiMaOA.Model.SearchParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/
        //OAEntities db;
        IBLL.IUserInfoService userInfoService { get; set; }
        IBLL.IRoleInfoService roleInfoService { get; set; }
        IBLL.IActionInfoService actionInfoService { get; set; }
        IBLL.IR_UserInfo_ActionInfoService r_UserInfo_ActionInfoService { get; set; }
        public ActionResult Index()
        {
            
            return View();
        }
        #region 获取用户信息
        public ActionResult GetUserInfo()
        {
            int pageIndex = int.Parse(Request["page"]);//当前页码.
            int pageSize = int.Parse(Request["rows"]);//当前每页显示记录数.
            int totalCount=0;
            string name = Request["name"];//接收用户名
            string remark = Request["remark"];//接收备注信息
            UserInfoFilter userInfoFilter = new UserInfoFilter()//构建用户搜索过来的条件
            {
                UName = name,
                URmark = remark,
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount
            };
         var userInfoList =userInfoService.LoadSearchUserInfo(userInfoFilter);//过滤用户的搜索条件
            //short deleteType = (short)DeleteEnumType.Normal;
          //  var userInfoList = userInfoService.LoadPageEntities<string>(pageIndex, pageSize, out totalCount, c => c.DelFlag == deleteType, c => c.Sort, true);//调用业务层.
            var temp = from u in userInfoList
                       select new { ID = u.ID, UName = u.UName, UPwd = u.UPwd, Remark = u.Remark, SubTime = u.SubTime, DelFlag=u.DelFlag,Sort=u.Sort };
            return Json(new { rows = temp, total = userInfoFilter.TotalCount }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 删除用户信息.
        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[]strIds=strId.Split(',');
            List<int> list = new List<int>();
            foreach (var id in strIds)
            {
                list.Add(int.Parse(id));
            }
            if (userInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion
        #region 添加用户信息
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            userInfo.SubTime = DateTime.Now;
            userInfo.ModifiedOn = DateTime.Now;
            userInfo.Sort = "0";
            userInfo.DelFlag = 0;
            userInfoService.AddEntity(userInfo);
            return Content("ok");
        }
        #endregion

        #region 修改数据.
        public ActionResult EditUserInfo(UserInfo userInfo)
        {

            userInfo.ModifiedOn = DateTime.Now;
            if (userInfoService.UpdateEntity(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }
        #endregion

        #region 展示修改数据.
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
           var userInfo= userInfoService.LoadEntities(u=>u.ID==id).FirstOrDefault();//获取要修改的数据.
           ViewData.Model = userInfo;
           return View();
        }
        #endregion
        #region 为用户分配角色.
        public ActionResult SetUserRoleInfo()
        {
            int id = int.Parse(Request["id"]);
            var userInfo = userInfoService.LoadEntities(c=>c.ID==id).FirstOrDefault();//查询出当前用户信息
            ViewBag.UserInfo = userInfo;
            short DelFlag =(short)DeleteEnumType.Normal;
            ViewBag.AllRoles = roleInfoService.LoadEntities(r=>r.DelFlag==DelFlag).ToList();//查出所有的角色信息
            ViewBag.ExtAllRoleIds =(from r in userInfo.RoleInfo
                                    select r.ID).ToList();//获取当前用户已经有的角色的编号
            return View();

        }
        [HttpPost]
        public ActionResult SetUserRoleInfo(FormCollection collection)
        {
            int userId = int.Parse(Request["userId"]);
           string[]AllKeys= Request.Form.AllKeys;//获取所有的表单的name属性的值.
           List<int> list = new List<int>();
           foreach (string key in AllKeys)
           {
               if (key.StartsWith("cba_"))
               {
                  string roleId=key.Replace("cba_", "");
                  list.Add(int.Parse(roleId));
               }
           }
           userInfoService.SetUserRole(userId, list);//给当前用户分配角色
           return Content("ok");
        }
        #endregion

        #region 为用户分配权限
        public ActionResult SetUserActionInfo()
        {
            int userId = int.Parse(Request["id"]);
            var userInfo=userInfoService.LoadEntities(u=>u.ID==userId).FirstOrDefault();
            ViewData.Model = userInfo;
            ViewBag.UserInfo = userInfo;
            ViewBag.AllActions = actionInfoService.LoadEntities(a=>a.DelFlag==0).ToList();//找出所有的权限
            ViewBag.AllExtActions = userInfo.R_UserInfo_ActionInfo.ToList();//找出当前用户所有的权限（包含允许，禁止）
            return View();
        }
        public ActionResult SetActionForUser()
        {
            int userId = int.Parse(Request["userId"]);//用户编号
            int actionId = int.Parse(Request["actionId"]);//权限编号
            string value = Request["value"];//表示允许或拒绝。
            bool isPass = value == "true" ? true : false;
            if (userInfoService.SetUserAction(userId, actionId, isPass))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 删除用户的权限
        public ActionResult ClearActionUser()
        {
            int userId = int.Parse(Request["userId"]);
            int actionId = int.Parse(Request["actionId"]);
         var actionInfo= r_UserInfo_ActionInfoService.LoadEntities(r=>r.UserInfoID==userId&&r.ActionInfoID==actionId).FirstOrDefault();
         if (actionInfo != null)
         {
             r_UserInfo_ActionInfoService.DeleteEntity(actionInfo);
         }
         return Content("ok");

        }
        #endregion
      
    }
}
