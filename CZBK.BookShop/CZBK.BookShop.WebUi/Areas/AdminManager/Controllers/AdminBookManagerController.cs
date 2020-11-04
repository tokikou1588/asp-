using CZBK.BookShop.Model;
using CZBK.BookShop.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Areas.AdminManager.Controllers
{
    public class AdminBookManagerController : Controller
    {
        //
        // GET: /AdminManager/AdminBookManager/
        IBLL.IBooksService BookService { get; set; }
        public ActionResult Index()
        {
            return View();
        }
        #region 展示商品列表.
        public ActionResult GetBookInfo()
        {
            int pageIndex = Request["page"] == null ? 1 : int.Parse(Request["page"]);
            int pageSize = Request["rows"] == null ? 5 : int.Parse(Request["rows"]);
            int totalCount;
            var temp = BookService.LoadPageEntities<int>(pageIndex, pageSize, out totalCount, c => c.Id, c => true, true);
            var rows = from t in temp
                       select new { Title = t.Title, Author = t.Author, ID = t.Id, UnitPrice = t.UnitPrice, ISBN = t.ISBN};
            return Json(new { rows = rows, total = totalCount }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加商品生成静态页面
        public ActionResult AddBookInfo()
        {
            //1.添加书(保存到数据库中)
            //2.然后再生成静态页.
          
            //测试代码：生成前10本书静态页面
            var bookList = BookService.LoadEntities(c => true).OrderBy<Books, int>(b => b.Id).Skip<Books>(0).Take<Books>(10);
            foreach (var book in bookList)
            {
                BookService.CreateHtmlPage(book.Id);
            }
            //3.将数据添加到索引库
            //Model.Books model = new Model.Books();
            //model.AurhorDescription = "老杨是谁呢??";
            //model.Author = "老杨";
            //model.CategoryId = 1;
            //model.Clicks = 1;
            //model.ContentDescription ="21天精通C#高级编程";
            //model.EditorComment = "adfsadfsadf";
            //model.ISBN = "9900000022";
            //model.PublishDate = DateTime.Now;
            //model.PublisherId = 72;
            //model.Title ="C#高级编程";
            //model.TOC = "aaaaaaaaaaaaaaaa";
            //model.UnitPrice = 22.3m;
            //model.WordsCount = 1234;
            ////BLL.BookManager bll = new BLL.BookManager();
            ////int id = bll.Add(model);
            //model.Id=999900;//表示刚插入记录的编号
            //IndexManager.GetInstance().AddQueue(model.Id,model.Title, model.ContentDescription, model.PublishDate.ToString());
          return Content("ok");
        }
        #endregion

        #region 展示添加视图
        public ActionResult ShowAddInfo()
        {
            return View();
        }
        #endregion

    }
}
