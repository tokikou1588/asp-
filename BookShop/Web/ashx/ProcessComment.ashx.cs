using BookShop.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// ProcessComment 的摘要说明
    /// </summary>
    public class ProcessComment : IHttpHandler
    {
        BLL.BookCommentManager bll = new BLL.BookCommentManager();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action=context.Request["action"];
            if (action == "add")
            {
                AddComment(context);
            }
            else if (action == "load")
            {
                LoadComment(context);//加载评论
            }
        }
        //加载评论
        private void LoadComment(HttpContext context)
        {
            int id = Convert.ToInt32(context.Request["bookId"]);
            List<Model.BookComment>list=bll.GetModelList("BookId="+id);//根据书的编号，获取书的评论
            List<ViewModelComment> newList = new List<ViewModelComment>();//定义一个新的集合
            //遍历旧的集合，向新的集合中添加数据
            foreach (Model.BookComment bookComment in list)
            {
                ViewModelComment viewModelComment = new ViewModelComment();

                viewModelComment.Msg =Common.ubb2html.decode(bookComment.Msg);//过滤评论的内容中的UBB编码

                TimeSpan ts = DateTime.Now - bookComment.CreateDateTime;
                viewModelComment.CreateDateTime = Common.WebCommon.GetTimespan(ts);//修改时间差的格式
                newList.Add(viewModelComment);
            }

            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            context.Response.Write(js.Serialize(newList));
        }
        /// <summary>
        /// 添加评论内容
        /// </summary>
        /// <param name="context"></param>
        private void AddComment(HttpContext context)
        {
           
            int id = Convert.ToInt32(context.Request["bookId"]);
            string msg = context.Request["msg"];
            BLL.Articel_WordsManager articelWorldManger = new BLL.Articel_WordsManager();
            if (articelWorldManger.CheckForbid(msg))//对用户输入的评论内容进行禁用词过滤。
            {
                context.Response.Write("no:评论中含有禁用词!!");
            }
            else if (articelWorldManger.CheckMod(msg))//审查词过滤
            {
                //注意：如果评论中含有审查词，评论的内容需要保存到数据库中，但是不能立马显示出来，需要审查。（在数据库中添加一个状态字段）
                context.Response.Write(AddUserComment(context, id, msg) ? "ok:评论成功,需要审查!!" : "no:评论失败");
            }
            else
            {
                msg=articelWorldManger.ReplaceWord(msg);

                context.Response.Write(AddUserComment(context, id, msg) ? "ok:评论成功!!" : "no:评论失败");
            }
        }
        /// <summary>
        /// 完成评论添加
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        private bool AddUserComment(HttpContext context, int id, string msg)
        {

            Model.BookComment bookComment = new Model.BookComment();
            bookComment.BookId = id;
            bookComment.Msg = msg;
            bookComment.CreateDateTime = DateTime.Now;

         return   bll.Add(bookComment)>0;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}
  