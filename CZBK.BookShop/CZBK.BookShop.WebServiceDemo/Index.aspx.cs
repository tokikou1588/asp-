using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CZBK.BookShop.WebServiceDemo
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  ServiceReference1.WebService1SoapClient client = new ServiceReference1.WebService1SoapClient();
          //// string str= client.HelloWorld();
          ////  string str = client.Add(3, 6).ToString();
          //// Response.Write(str);
          // string str= client.GetUserInfoList();
          // Response.Write(str);

            ServiceReference2.WeatherWSSoapClient client = new ServiceReference2.WeatherWSSoapClient();
           string[]strs= client.getRegionProvince();
           foreach (string s in strs)
           {
               Response.Write(s);
           }
        }
    }
}