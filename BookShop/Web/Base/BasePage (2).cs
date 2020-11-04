using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Web.Base
{
  public  class BasePage:System.Web.UI.Page
    {
      protected override void OnInit(EventArgs e)
      {
          base.OnInit(e);
          if (Session["userInfo"] == null)
          {
              //判断Cookie
              if (Request.Cookies["cp1"] != null)
              {
                  string userName = Request.Cookies["cp1"].Value;
                  BLL.UserManager userManager = new BLL.UserManager();
                  Model.User userInfo = userManager.GetModel(userName);
                  if (!Common.WebCommon.ValidateCookieInfo(userInfo))
                  {
                      Common.WebCommon.GoPage();
                  }
              }
              else
              {
                  Common.WebCommon.GoPage();
              }
              //Response.Redirect("/Login.aspx");
             // 
          }
      }
    }
}
