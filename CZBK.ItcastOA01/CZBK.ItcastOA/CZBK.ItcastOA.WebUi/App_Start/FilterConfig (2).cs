using CZBK.ItcastOA.WebUi.Models;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
          // filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionAttribute());//告诉整个应用程序捕获异常有我们自己定义的类来进行处理。
        }
    }
}