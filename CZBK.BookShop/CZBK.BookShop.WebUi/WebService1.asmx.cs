using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace CZBK.BookShop.WebUi
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public int Add(int a, int b)
        {
            return a + b;
        }
        [WebMethod(Description="获取用户信息")]
        public string GetUserInfoList()
        {
            IBLL.IUserInfoService UserInfoService = new BLL.UserInfoService();
            List<UserInfo>list= UserInfoService.LoadEntities(u => true).ToList();
           return Common.SerializeHelper.SerializeToString(list);
        }
    }
}
