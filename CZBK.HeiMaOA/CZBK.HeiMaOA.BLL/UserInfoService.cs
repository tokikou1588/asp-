using CZBK.HeiMaOA.IBLL;
using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.BLL
{
   public partial class UserInfoService:BaseService<UserInfo>,IUserInfoService
    {
        //public override void SetCurrentDal()
        //{
        //    CurrentDal = this.DbSession.UserInfoDal;
        //}
        //public void SetUserRole()
        //{

        //    thin.UserInfoDal.AddEntity(UserInfo userInfo);
        //    this.DbSessions.DbSessio.RoleInfoDal.AddEntity(RoleInfo roleInfo);
        //    this.DbSession.SaveChanges();
        //}

        #region 批量删除用户数据
        public bool DeleteEntities(List<int> list)//1,3,4
        {
           var userInfoList= this.DbSession.UserInfoDal.LoadEntities(u=>list.Contains(u.ID));
           if (userInfoList != null)
           {
               foreach (var userInfo in userInfoList)
               {
                   this.DbSession.UserInfoDal.DeleteEntity(userInfo);//将删除的数据添加到EF上下文中，并且添加了删除标记.
               }
           }
           return this.DbSession.SaveChanges();//最后调用DBSession中的SaveChanges方法将数据一次性提交回数据库.
        }
        #endregion

        #region 多条件搜索用户信息
        public IQueryable<UserInfo> LoadSearchUserInfo(Model.SearchParams.UserInfoFilter userInfoFilter)
        {
            var temp = this.DbSession.UserInfoDal.LoadEntities(c=>true);
            if (!string.IsNullOrEmpty(userInfoFilter.UName))//判断用户名是否为空
            {
                temp = temp.Where<UserInfo>(u=>u.UName.Contains(userInfoFilter.UName));
            }
            if (!string.IsNullOrEmpty(userInfoFilter.URmark))
            {
                temp = temp.Where<UserInfo>(u=>u.Remark.Contains(userInfoFilter.URmark));
            }
            userInfoFilter.TotalCount = temp.Count();
            return temp.OrderBy<UserInfo, string>(u => u.Sort).Skip<UserInfo>((userInfoFilter.PageIndex - 1) * userInfoFilter.PageSize).Take<UserInfo>(userInfoFilter.PageSize);
        }
        #endregion


        #region 找回密码
        public void FindUserPwd(UserInfo userInfo)
        {
            string newPwd = Guid.NewGuid().ToString().Substring(0,8);
            //将产生的新密码加密以后替换用户的原来的旧密码.但是发送到用户邮箱中的密码必须是明文.
            userInfo.UPwd = newPwd;
            this.DbSession.UserInfoDal.UpdateEntity(userInfo);
            this.DbSession.SaveChanges();
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress("wang_itcast@126.com", "王承伟");//源邮件地址 。发件人地址.
            mailMsg.To.Add(new MailAddress("wangchengwei324@126.com", "王承伟"));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = "新的账户如下:";//发送邮件的标题 
            StringBuilder sb = new StringBuilder();
            sb.Append("你的新的账户如下:");
            sb.Append("用户名:" + userInfo.UName);
            sb.Append("密码:"+newPwd);
            mailMsg.Body =sb.ToString();//发送邮件的内容 
            SmtpClient client = new SmtpClient("smtp.126.com");//smtp.163.com，smtp.qq.com.发件人的SMTP服务器的地址.
            client.Credentials = new NetworkCredential("wang_itcast", "wangchengwei");//发件人邮箱的用户和密码.
            client.Send(mailMsg);//排队发送邮件.
        }
        #endregion


        #region 为用户分配角色
        public bool SetUserRole(int userId, List<int> RoleIdList)
        {
            var userInfo = this.DbSession.UserInfoDal.LoadEntities(u=>u.ID==userId).FirstOrDefault();
            if (userInfo != null)
            {
                userInfo.RoleInfo.Clear();//删除当前用户已经有的角色.
                foreach (int roleId in RoleIdList)
                {
                   var roleInfo= this.DbSession.RoleInfoDal.LoadEntities(r=>r.ID==roleId).FirstOrDefault();
                   //insert into UserInfoRoleInfo(UserId,RoleId) values(userId,roleId)
                    //SqlHelper.ExecuteNoneQuery(sql,new sqlpara);
                   userInfo.RoleInfo.Add(roleInfo);//根据RoleIdList集合中存储的角色编号，获取角色信息，然后给当前用户添加.
                }
            }
          return  this.DbSession.SaveChanges();
        }
        #endregion


        #region 给用户设置权限
        public bool SetUserAction(int userId, int actionId, bool value)
        {
            //根据传过来的用户编号与权限编号查询R_UserInfo_ActionInfoDal中是否有该记录
            var actionInfo = this.DbSession.R_UserInfo_ActionInfoDal.LoadEntities(r=>r.UserInfoID==userId&&r.ActionInfoID==actionId).FirstOrDefault();
            //如果没有就添加
            if (actionInfo == null)
            {
                R_UserInfo_ActionInfo r_UserInfo_ActionInfo = new R_UserInfo_ActionInfo();
                r_UserInfo_ActionInfo.IsPass = value;
                r_UserInfo_ActionInfo.ActionInfoID = actionId;
                r_UserInfo_ActionInfo.UserInfoID = userId;
                this.DbSession.R_UserInfo_ActionInfoDal.AddEntity(r_UserInfo_ActionInfo);
                //return this.DbSession.SaveChanges();
            }
            else//如果就修改
            {
                if (actionInfo.IsPass != value)
                {
                    actionInfo.IsPass = value;
                  //return  this.DbSession.SaveChanges();
                }
            }
            return this.DbSession.SaveChanges();
        }
        #endregion
       
    }
}
