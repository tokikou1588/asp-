using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.Common
{
   public class PageBar
    {
       public static string CreatePageBar(int pageIndex,int pageCount)
       {
           if (pageCount == 1)
           {
               return string.Empty;
           }
           int start = pageIndex - 5;//页面显示10个数字页码.
           if (start < 1)
           {
               start = 1;
           }
           int end = start + 9;
           if (end > pageCount)
           {
               end = pageCount;
               start = end - 9 > 0 ? end - 9 : 1;
           }
           StringBuilder sb = new StringBuilder();
           if (pageIndex > 1)
           {
               sb.Append(string.Format("<a href=?pageIndex={0}>上一页</a>", pageIndex - 1));
           }
           for (var i = start; i <= end; i++)
           {
               if (i == pageIndex)
               {
                   sb.Append(i);
               }
               else
               {
                   sb.Append(string.Format("<a href=?pageIndex={0}>{0}</a>",i));
               }
           }
           if (pageIndex < pageCount)
           {
               sb.Append(string.Format("<a href=?pageIndex={0}>下一页</a>", pageIndex + 1));
           }
           return sb.ToString();
          
       }
    }
}
