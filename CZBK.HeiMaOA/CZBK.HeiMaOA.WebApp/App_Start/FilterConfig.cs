using CZBK.HeiMaOA.WebApp.Models;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
           // filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionAttribute());//使用自定义异常处理的过滤器.
        }
    }
}