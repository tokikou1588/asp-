using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            if (LoginUser != null)
            {
                ViewData["userName"] = LoginUser.LoginId;
            }
            return View();
        }
        public ActionResult Test()
        {
            int a = 2;
            int b = 0;
            int c = a / b;
            return Content(c.ToString());
        }
        public ActionResult TestSeo()
        {
            return View();
        }
        public ActionResult Test2()
        {
            return Content("美女22222222222222222222");
        }
        public ActionResult ShowEdit()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GetEdit()
        {
            string msg = Request["editor1"];
                return Content(msg);
        }

    }
}
