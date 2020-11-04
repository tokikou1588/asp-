using CZBK.BookShop.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Controllers
{
    public class BookListController : Controller
    {
        //
        // GET: /BookList/
        IBLL.IBooksService BookService { get; set; }
        IBLL.IBookCommentService BookCommentService { get; set; }
        public ActionResult Index()
        {
            int pageIndex = Request["pageIndex"] != null ? int.Parse(Request["pageIndex"]) : 1;
            int pageSize=10;
            int pageCount = BookService.GetPageCount(10);
            pageIndex = pageIndex < 1 ? 1 : pageIndex;
            pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
          var bookList=  BookService.LoadPageEntities<int>(pageIndex, pageSize, c => c.Id, c => true, true).ToList();
          ViewData["bookList"] = bookList;
          ViewData["pageIndex"] = pageIndex;
          ViewData["pageCount"] = pageCount;
            return View();
        }
        #region 添加书的评论
        [ValidateInput(false)]
        public ActionResult AddComment()
        {
            string msg = Request["msg"];//接收评论内容。
            if (BookService.CheckBannedWord(msg))//条件成立，有禁用词
            {
                return Content("输入的内容中含有禁用词");

            }
            else if (BookService.CheckModWord(msg))//有审查词
            {
                //把评论内容写到数据库中。
                return Content("输入的内容中含有审查词");
            }
            else
            {
                int bookId = int.Parse(Request["bookId"]);
                Model.BookComment comment = new Model.BookComment();
                comment.BookId = bookId;
                comment.CreateDateTime = DateTime.Now;
                comment.Msg =BookService.ReplaceWord(msg);//替换词
                BookCommentService.AddEntity(comment);
                return Content("ok");
            }
        }
        #endregion

        #region 加载评论内容
        public ActionResult LoadComment()
        {
            //ViewModel
            int bookId = int.Parse(Request["bookId"]);
          var commentList= BookCommentService.LoadEntities(b=>b.BookId==bookId).ToList();
          List<ViewModelComment> newList = new List<ViewModelComment>();
          foreach (var comment in commentList)
          {
              ViewModelComment viewModel = new ViewModelComment();
              viewModel.Msg =Common.WebCommon.DecodeUBBToHtml (comment.Msg);//将UBB编码转成HTML
              TimeSpan timeSpan = DateTime.Now - comment.CreateDateTime;
              viewModel.CreateDateTime = Common.WebCommon.GetTimeSpan(timeSpan);//处理时间
              newList.Add(viewModel);
          }
          return Content(Common.SerializeHelper.SerializeToString(newList));
        }
        #endregion

    }
}
