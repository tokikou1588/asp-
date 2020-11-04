using CZBK.HeiMaOA.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.HeiMaOA.WebApp.Controllers
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
            return Content(c.ToString());
            //return View();
        }
        public ActionResult AddBook()
        {
          Model.Books  model = new Model.Books();
            model.AurhorDescription = "jlkfdjf";
            model.Author = "蒋坤2222";
            model.CategoryId = 1;
            model.Clicks = 1;
            model.ContentDescription ="面向JK编程";
            model.EditorComment = "adfsadfsadf";
            model.ISBN = "18890099";
            model.PublishDate = DateTime.Now;
            model.PublisherId = 72;
            model.Title ="jk入门到精通";
            model.TOC = "aaaaaaaaaadfsdfaaaaaa";
            model.UnitPrice = 22.3m;
            model.WordsCount = 1234;
           // BLL.BookManager bll = new BLL.BookManager();
           //int id= bll.Add(model);
            int id=2222;
            IndexManager.GetInstance().AddOrEditQueue(id.ToString(), model.Title,model.ContentDescription);
            return Content("ok");

        }

    }
}
