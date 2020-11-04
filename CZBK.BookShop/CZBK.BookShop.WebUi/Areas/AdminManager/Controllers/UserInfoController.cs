using CZBK.BookShop.Model;
using CZBK.BookShop.Model.Enum;
using CZBK.BookShop.Model.SearchParams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class UserInfoController : Controller
    {
        //
        // GET: /AdminManager/UserInfo/
        IBLL.IUserInfoService UserInfoService { get; set; }
        IBLL.IRoleService RoleInfoService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 获取用户信息.
        public ActionResult GetUserInfo()
        {
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            int pageSize = Request["rows"] != null ? int.Parse(Request["rows"]) : 5;
            string userName = Request["name"];
            string userMail=Request["mail"];
            //对搜索的条件进行判断.
            //构建搜索的条件数据
            int totalCount = 0;
            UserInfoSearch userInfoSearchParam = new UserInfoSearch() { 
             PageIndex=pageIndex,
              PageSize=pageSize,
              TotalCount =totalCount,
              UserEmail=userMail,
              UserName=userName
            };
            var userInfoList = UserInfoService.LoadSearchUserInfo(userInfoSearchParam);
         // int totalCount;
      // var userInfoList= UserInfoService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.ID, c => true, true);//调用业务层获取数据.
        var rows = from u in userInfoList
                   select new { ID = u.ID, UserName = u.UserName, UserPass = u.UserPass, Email = u.Email, RegTime=u.RegTime};
        return Json(new { rows = rows, total = userInfoSearchParam.TotalCount }, JsonRequestBehavior.AllowGet);


        }
        #endregion

        #region 删除用户数据.
        public ActionResult DeleteUserInfo()
        {
            string strId = Request["strId"];
            string[]strIds=strId.Split(',');
            List<int> list = new List<int>();
            foreach (var id in strIds)
            {
                list.Add(Convert.ToInt32(id));
            }
            if (UserInfoService.DeleteEntities(list))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
        }
        #endregion

        #region 添加用户数据
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            userInfo.RegTime = DateTime.Now;
            UserInfoService.AddEntity(userInfo);
            return Content("ok");
        }
        #endregion

        #region 编辑用户信息
        public ActionResult EditUserInfo(UserInfo userInfo)
        {
            if (UserInfoService.UpdateEntity(userInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("no");
            }

        }
        #endregion

        #region 展示要修改的数据.
        public ActionResult ShowEditInfo()
        {
            int id = int.Parse(Request["id"]);
           ViewData.Model=UserInfoService.LoadEntities(u=>u.ID==id).FirstOrDefault();
           return View();
        }
        #endregion

        #region 展示用户已经有的角色
        public ActionResult SetUserRoleInfo()
        {
            int userId = int.Parse(Request["userId"]);
            UserInfo userInfo = UserInfoService.LoadEntities(u=>u.ID==userId).FirstOrDefault();
            ViewData["userInfo"] = userInfo;
           short delFlag=(short)DelFlag.Normal;
           var roleInfoList= RoleInfoService.LoadEntities(r => r.DelFlag == delFlag).ToList();//查询出所有的角色
            //查询该用户已经有的角色编号
           var userExtRoleIdList = (from r in userInfo.Role
                                    select r.ID).ToList();
           ViewData["roleInfoList"] = roleInfoList;
           ViewData["userExtRoleIdList"] = userExtRoleIdList;
            return View();
        }
        #endregion

        #region 完成对用户角色的分配
        [HttpPost]
        public ActionResult SetUserRoleInfo(int userId)
        {
           string[]keys=Request.Form.AllKeys;//获取表单(form)所有name属性的值.
            List<int> list = new List<int>();
            foreach (string key in keys)
            {
                if (key.StartsWith("cba_"))
                {
                    list.Add(Convert.ToInt32(key.Replace("cba_","")));
                }
            }
            if (UserInfoService.SetUserRole(userId, list))
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
