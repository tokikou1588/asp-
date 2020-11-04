using CZBK.ItcastOA.Model;
using CZBK.ItcastOA.Model.Enum;
using CZBK.ItcastOA.Model.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /UserInfo/  Spring.Net

        IBLL.IUserInfoService UserInfoService {get;set;}
        IBLL.IRoleInfoService RoleInfoService { get; set; }
        public ActionResult Index()
        {
           
            return View();
        }
        #region 加载用户数据
        public ActionResult GetUserInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;//当前页码。
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;//每页显示记录数。
            //接收的搜索条件
            string uName = Request["name"];
            string uRemark=Request["remark"];
        
         
        int totalCount=0;
            //构建搜索条件了。
            UserInfoSearch userInfoSearch = new UserInfoSearch() {
            UName=uName,
            URemark=uRemark,
            PageIndex=pageIndex,
            PageSize=pageSize,
            TotalCount=totalCount
            
            };

         short delFlag=(short)DelFlagEnum.Normal;
           //var userInfoList=UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, u => u.DelFlag == delFlag, u => u.ID, true);
            var userInfoList = UserInfoService.LoadSearchEntities(userInfoSearch,delFlag);//搜索

           var temp = from u in userInfoList
                      select new { ID=u.ID,UserName=u.UName,UserPwd=u.UPwd,SubTime=u.SubTime,Remark=u.Remark};
           return Json(new {rows=temp,total=userInfoSearch.TotalCount },JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 删除用户
        public ActionResult DeleteUser()
        {
            string strId=Request["strId"];//1,2
            string[]strIds=strId.Split(',');
            List<int> list = new List<int>();
            foreach (string id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            return Content(UserInfoService.DeleteEntities(list) ? "ok" : "no");

        }
        #endregion

        #region 添加用户
        public ActionResult AddUser(UserInfo userInfo)
        {
            short delFlag = (short)DelFlagEnum.Normal;
            userInfo.DelFlag = delFlag;
            userInfo.ModifiedOn = DateTime.Now;
            userInfo.SubTime = DateTime.Now;
            UserInfoService.AddEntity(userInfo);
            return Content("ok");

        }
        #endregion
        #region 展示要修改的数据
        public ActionResult ShowEdit()
        {
            int id = int.Parse(Request["id"]);
           var userInfo= UserInfoService.LoadEntities(u=>u.ID==id).FirstOrDefault();
           if (userInfo != null)
           {
               return Json(new {Msg="ok",UserInfo=userInfo}, JsonRequestBehavior.AllowGet);
           }
           else
           {
               return Json(new { Msg="no"});
           }
        }
        #endregion
        #region 完成用户更新
        public ActionResult EidtUser(UserInfo userInfo)
        {
            userInfo.ModifiedOn = DateTime.Now;
            return Content(UserInfoService.EditEntity(userInfo) ? "ok" : "no");
        }
        #endregion
        #region 为用户分配角色
        public ActionResult SetUserRoleInfo()
        {
            //接收UserId.
            int userId = int.Parse(Request["uid"]);
            var userInfo = UserInfoService.LoadEntities(u=>u.ID==userId).FirstOrDefault();//获取要分配角色的用户.
            //要分配角色的用户以前具有哪些角色。
            var userRoleIdList = (from r in userInfo.RoleInfo
                                  select r.ID).ToList();

            //获取所有的角色。
             short delFlag=(short)DelFlagEnum.Normal;
             var roleInfoList = RoleInfoService.LoadEntities(r => r.DelFlag == delFlag).ToList();
             ViewBag.UserRoleIdList = userRoleIdList;
             ViewBag.RoleInfoList = roleInfoList;
             ViewBag.UserInfo = userInfo;
             return View();
        }
        public ActionResult SetUserRole()
        {
            string[] roleNames = Request.Form.AllKeys;//获取所有表单元素的name属性的值。
            int userId = Convert.ToInt32(Request["userId"]);//给该用户分配角色
            List<int> list = new List<int>();
            foreach (string name in roleNames)
            {
                if (name.StartsWith("cba_"))
                {
                    string id=name.Replace("cba_", "");
                    list.Add(Convert.ToInt32(id));
                }
            }
            return UserInfoService.SetUserRoleInfo(userId, list) ? Content("ok") : Content("no");

        }
        #endregion

    }
}
