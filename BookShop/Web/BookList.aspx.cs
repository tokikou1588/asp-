using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookShop.Web
{
    public partial class BookList : System.Web.UI.Page
    {
        public List<Model.Book> ListBook { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod == "GET")
            {
                BindBookList();
            }
        }
        /// <summary>
        /// 绑定商品的列表
        /// </summary>
        private void BindBookList()
        {
            int pageSize=10;
            int pageIndex = 1;
            if (!int.TryParse(Request["pageIndex"], out pageIndex))
            {
                pageIndex = 1;
            }
            BLL.BookManager bookManager = new BLL.BookManager();
           int pageCount= bookManager.GetPageCount(pageSize);//获取总页数
           PageCount = pageCount;
            //判断当前页码的取值范围。
           pageIndex = pageIndex < 1 ? 1 : pageIndex;
           pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
           PageIndex = pageIndex;
            ListBook= bookManager.GetPageList(pageIndex,pageSize);
        }
        /// <summary>
        /// 截取字符串（内容简介）
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public string CutString(string str, int length)
        {
            return str.Length > length ? str.Substring(0, length) + "........" : str;
        }
        public string GetDir(DateTime time)
        {
            return "/StaticPage/" + time.Year + "/" + time.Month + "/" + time.Day + "/";
        }
    }
}