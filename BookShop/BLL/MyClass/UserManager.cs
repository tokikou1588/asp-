using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookShop.BLL
{
   public partial class UserManager
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BookShop.Model.User GetModel(string userName)
        {

            return dal.GetModel(userName);
        }
        /// <summary>
       /// 根据用户邮箱进行查找
       /// </summary>
       /// <param name="mail"></param>
       /// <returns></returns>
        public bool ValidateUserMail(string mail)
        {
            return dal.ValidateUserMail(mail) > 0;
        }
       /// <summary>
       /// 根据用户邮箱找用户的信息
       /// </summary>
       /// <param name="mail"></param>
       /// <returns></returns>
        public Model.User GetUserByMail(string mail)
        {
            return dal.GetModelByMail(mail);
        }
       
		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(BookShop.Model.User model,out string msg)
        {
            if (dal.GetModel(model.LoginId)!=null)
            {
                msg = "用户名已经存在!!";
                return -1;
            }
            else
            {
                msg = "注册成功!!";
             return   dal.Add(model);
            }
        }
       /// <summary>
       /// 登录
       /// </summary>
       /// <param name="userName"></param>
       /// <param name="userPwd"></param>
       /// <param name="userInfo"></param>
       /// <param name="msg"></param>
       /// <returns></returns>
        public bool UserLogin(string userName, string userPwd, out User userInfo, out string msg)
        {
            bool isSucess = false;
            userInfo=dal.GetModel(userName);
            if (userInfo != null)
            {
                if (userInfo.LoginPwd == Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(userPwd)))
                {
                    msg = "登录成功!!";
                    isSucess = true;
                }
                else
                {
                    msg = "用户名密码错误!!";
                }
            }
            else
            {
                msg = "此用户不存在!!";
            }
            return isSucess;
        }

       /// <summary>
       /// 找回用户的密码
       /// </summary>
       /// <param name="userInfo"></param>
        public void GetUserPwd(Model.User userInfo)
        {
            //产生一个新的密码，然后更新用户的旧密码，注意：发送到用户邮箱中的密码必须明文。
            string newPwd = Guid.NewGuid().ToString().Substring(0,8);
            userInfo.LoginPwd =Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(newPwd));
            dal.Update(userInfo);
            BLL.SettingsManager bll = new SettingsManager();
            MailMessage mailMsg = new MailMessage();//两个类，别混了，要引入System.Net这个Assembly
            mailMsg.From = new MailAddress(bll.GetValue("系统邮件地址"));//源邮件地址 
            mailMsg.To.Add(new MailAddress(userInfo.Mail));//目的邮件地址。可以有多个收件人
            mailMsg.Subject = "找回的账户如下：";//发送邮件的标题 
            StringBuilder sb = new StringBuilder();
            sb.Append("您在Itcast商城中新的账户如下：");
            sb.Append("账户:"+userInfo.LoginId+",密码是:"+newPwd);
            mailMsg.Body = sb.ToString();//发送邮件的内容 
            mailMsg.IsBodyHtml = true;
            //指定SMTP服务器.  (smtp)
            SmtpClient client = new SmtpClient(bll.GetValue("系统邮件SMTP"));//smtp.163.com，smtp.qq.com
            client.Credentials = new NetworkCredential(bll.GetValue("系统邮件用户名"), bll.GetValue("系统邮件密码"));//发件人邮箱的账户和密码.
            client.Send(mailMsg);//队列。


        }
    }
}
