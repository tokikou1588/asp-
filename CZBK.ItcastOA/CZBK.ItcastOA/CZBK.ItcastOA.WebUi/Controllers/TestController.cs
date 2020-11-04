using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CZBK.ItcastOA.WebUi.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            int a = 2;
            int b = 0;
            int c = a / b;
            return Content("ok");
        }
        //public ActionResult ShowTest()
        //{
        //    Thread myThread = new Thread(Start);
        //    myThread.IsBackground = true;
        //    myThread.Start();
        //    return Content("ok");
        //}

        //private void Start()
        //{
        //   // Common.WebCommon.Show();
        //    HttpContext.Response.Write("ssss");
        //}

    }
}
