using CZBK.BookShop.IBLL;
using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CZBK.BookShop.BLL
{
   public partial class BooksService:BaseService<Books>,IBooksService
    {
       /// <summary>
       /// 计算总页数
       /// </summary>
       /// <param name="pageSize"></param>
       /// <returns></returns>
        public int GetPageCount(int pageSize)
        {
            int recordCount = this.DbSession.BooksDal.LoadEntities(c=>true).Count();
            int pageCount =Convert.ToInt32(Math.Ceiling((double)recordCount / pageSize));
            return pageCount;
        }

       /// <summary>
       /// 生成静态页面
       /// </summary>
       /// <param name="bookId"></param>
        public void CreateHtmlPage(int bookId)
        {
            var bookInfo = this.DbSession.BooksDal.LoadEntities(b=>b.Id==bookId).FirstOrDefault();
            string template = HttpContext.Current.Request.MapPath("/Template/BookTemplate.html");
            string html = File.ReadAllText(template);
            html = html.Replace("$title", bookInfo.Title).Replace("$author", bookInfo.Author).Replace("$unitprice", bookInfo.UnitPrice.ToString("0.00")).Replace("$isbn", bookInfo.ISBN).Replace("$toc", bookInfo.TOC).Replace("$content", bookInfo.ContentDescription).Replace("$bookId",bookInfo.Id.ToString());
            string dir = "/HtmlPage/"+bookInfo.PublishDate.Year+"/"+bookInfo.PublishDate.Month+"/"+bookInfo.PublishDate.Day+"/";
            Directory.CreateDirectory(Path.GetDirectoryName(HttpContext.Current.Request.MapPath(dir)));
            File.WriteAllText(HttpContext.Current.Request.MapPath(dir) + bookInfo.Id + ".html", html, Encoding.UTF8);

        }



       /// <summary>
       /// 对用户发布的评论进行禁用词过滤
       /// </summary>
       /// <param name="msg">评论内容</param>
       /// <returns></returns>
        public bool CheckBannedWord(string msg)
        {
            //获取数据库中存储的所有的禁用词.
          var list=this.DbSession.Articel_WordsDal.LoadEntities(c=>c.IsForbid==true).Select<Articel_Words,string>(a=>a.WordPattern).ToList();//缓存中。
         string  regex=string.Join("|", list.ToArray());// aa|bb|cc
       return  Regex.IsMatch(msg, regex);//进行正则匹配.
          //foreach (var w in list)
          //{
          //    msg.Contains(w);
          //}
        }

       /// <summary>
       /// 审查词过滤
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
        public bool CheckModWord(string msg)
        {
            var list = this.DbSession.Articel_WordsDal.LoadEntities(c => c.IsMod == true).Select<Articel_Words, string>(a => a.WordPattern).ToList();//缓存中。
            string regex = string.Join("|", list.ToArray());// aa|bb|cc
            regex = regex.Replace(@"\",@"\\").Replace(@"{2}",@".{0,2}");
            return Regex.IsMatch(msg, regex);//进行正则匹配.
        }

       /// <summary>
       /// 替换词
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
        public string ReplaceWord(string msg)
        {
            var list = this.DbSession.Articel_WordsDal.LoadEntities(c => c.IsMod == false && c.IsForbid==false).ToList();//缓存中。
            foreach ( var word in list)
            {
                msg = msg.Replace(word.WordPattern, word.ReplaceWord);
            }
            return msg;
        }
    }
}
