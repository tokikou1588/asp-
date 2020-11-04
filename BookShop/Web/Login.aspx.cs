using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class Login : System.Web.UI.Page
    {
        public string Msg { get; set; }
        public string ReturnUrl { get; set; }
        BLL.UserManager userManager = new BLL.UserManager();
        protected void Page_Load(object sender, EventArgs e)
        {
            //__VIEWSTATE
            if (Request.HttpMethod == "POST")
            {
                UserLogin();
            }
            else
            {

                if (Session["userInfo"] != null)
                {
                    Response.Redirect("/UserManager/UserCenter.aspx");
                }
                //接收回传过来的URL地址
                string url = Request["returnUrl"];
                if (!string.IsNullOrEmpty(url))
                {
                    ReturnUrl = url;//将该URL地址存储到隐藏域中。
                }
                if (Request.Cookies["cp1"] != null)//接收Cookie
                {
                    string userName = Request.Cookies["cp1"].Value;
                    Model.User userInfo=userManager.GetModel(userName);//根据Cookie中存储的用户名找人
                    if (Common.WebCommon.ValidateCookieInfo(userInfo))
                    {
                        Response.Redirect("/UserManager/UserCenter.aspx");
                    }
                }
            }
        }
        private void UserLogin()
        {
            string userName = Request["username"];
            string userPwd = Request["password"];
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPwd))
            {
                string msg=string.Empty;
                Model.User userInfo=null;
             
                if (userManager.UserLogin(userName, userPwd, out userInfo, out msg))
                {
                    Session["userInfo"]=userInfo;
                    //判断一下用户是否选择了“自动登录”
                    if (!string.IsNullOrEmpty(Request["chkRember"]))
                    {
                        HttpCookie cookie1 = new HttpCookie("cp1",userName);
                        HttpCookie cookie2 = new HttpCookie("cp2", Common.WebCommon.GetMd5String(Common.WebCommon.GetMd5String(userPwd)));
                        cookie1.Expires = DateTime.Now.AddDays(7);
                        cookie2.Expires = DateTime.Now.AddDays(7);
                        Response.Cookies.Add(cookie1);
                        Response.Cookies.Add(cookie2);
                    }

                    if (!string.IsNullOrEmpty(Request["returnUrl"]))//接收隐藏域中存储的URL地址。
                    {
                        Response.Redirect(Request["returnUrl"]);
                    }
                    else
                    {
                        Response.Redirect("/UserManager/UserCenter.aspx");
                    }
                }
                else
                {
                    Msg = msg;
                }
            }
            else
            {
                Msg = "用户名或密码不能为空!!";
            }
        }
    }
}