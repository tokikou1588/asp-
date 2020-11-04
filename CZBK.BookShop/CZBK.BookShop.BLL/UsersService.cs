using CZBK.BookShop.IBLL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.BLL
{
    public partial class UsersService : BaseService<Users>, IUsersService
    {
        SettingsService bll = new SettingsService();
        public bool AddEntity(Users userInfo, out string msg)
        {
            //判断用户名是否已经存在!!
            if (CheckUserName(userInfo.LoginId))//用户名已经存在
            {
                msg = "注册失败!!";
                return false;
            }
            else
            {
             this.DbSession.UsersDal.AddEntity(userInfo);
                CheckEmail model = new CheckEmail();
                model.Actived = false;
                model.ActiveCode = Guid.NewGuid().ToString();
                this.DbSession.CheckEmailDal.AddEntity(model);
                this.DbSession.SaveChanges();
                //发送激活链接.
                SendMail(model.ActiveCode,userInfo.Mail);
                msg = "注册成功!!请登录邮箱激活链接!!";
                return true;
            }

        }
        public bool CheckUserName(string userName)
        {
            return this.DbSession.UsersDal.LoadEntities(u=>u.LoginId==userName).Count()>0;
        }
        #region 发送激活链接.
        public void SendMail(string activeCode, string mail)
        {
           
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress(bll.GetValue("系统邮件地址"));//源邮件地址 (发件人)
            mailMsg.To.Add(new MailAddress(mail));//目的邮件地址。可以有多个收件人
            mailMsg.Subject ="请激活链接";//发送邮件的标题 
      
            mailMsg.Body = "<a href='http://localhost:8871/Register/Active/?userId=39&activeCode=" + activeCode + "'>请单击激活</a>";
            mailMsg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(bll.GetValue("系统邮件SMTP"));//smtp.163.com，smtp.qq.com  发件人邮箱对应的SMTP服务器。
            client.Credentials = new NetworkCredential(bll.GetValue("系统邮件用户名"), bll.GetValue("系统邮件密码"));//发件人邮箱的用户名密码.
            client.Send(mailMsg);//排队发送邮件.
        }
        #endregion

        #region 判断用户名密码
        public bool LoadUserLogin(string userName, string userPwd, out string msg, out Users user)
        {
            user=this.DbSession.UsersDal.LoadEntities(u=>u.LoginId==userName).FirstOrDefault();
            if (user != null)
            {
                if (user.UserStates.Name == "正常")
                {
                    if (user.LoginPwd == userPwd)
                    {
                        msg = "登录成功!!";
                        return true;
                    }
                    else
                    {
                        msg = "用户名密码错误!!";
                        return false;
                    }
                }
                else
                {
                    msg = "此用户已经被锁定了!!";
                    return false;
                }
            }
            else
            {
                msg = "此用户不存在!!";
                return false;
            }
        }
        #endregion


        #region 找回密码
        public void FindUserPwd(Users user)
        {
            string newPwd = Guid.NewGuid().ToString().Substring(0,8);
            user.LoginPwd = newPwd;//密码一定要加密以后更新到数据库中，但是发送到用户邮箱中的密码必须是明文.
            this.DbSession.UsersDal.UpdateEntity(user);
            this.DbSession.SaveChanges();
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress(bll.GetValue("系统邮件地址"));//源邮件地址 (发件人)
            mailMsg.To.Add(new MailAddress(user.Mail));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = "新的账户如下:";//发送邮件的标题 
            StringBuilder sb = new StringBuilder();
            sb.Append("你在商城新的账户如下:");
            sb.Append("用户名:"+user.LoginId);
            sb.Append("密码:"+newPwd);
            mailMsg.Body = sb.ToString();
            mailMsg.IsBodyHtml = true;
            SmtpClient client = new SmtpClient(bll.GetValue("系统邮件SMTP"));//smtp.163.com，smtp.qq.com  发件人邮箱对应的SMTP服务器。
            client.Credentials = new NetworkCredential(bll.GetValue("系统邮件用户名"), bll.GetValue("系统邮件密码"));//发件人邮箱的用户名密码.
            client.Send(mailMsg);//排队发送邮件.
        }
        #endregion
     
    }
}
