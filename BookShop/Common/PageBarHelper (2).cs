using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 思考1：添加“上一页”与"下一页".（要求：如果用户访问到第一页，“上一页”不显示，如果访问到最后一页，“下一页”不显示。）
    /// 思考2：当浏览的最后几页时，发现了页面上显示的数字页码不够10个了？？？？
    /// </summary>
   public class PageBarHelper
    {
       /// <summary>
       /// 获取数字页码
       /// </summary>
       /// <param name="pageIndex">当前页码</param>
       /// <param name="pageCount">总页数</param>
       public static string GetPageBar(int pageIndex,int pageCount)
       {

           if(pageCount==1)
           {
               return string.Empty;
           }
           
           //计算起始位置
           int start = pageIndex - 5;//要求页面显示10个数字
           if (start < 1)
           {
               start = 1;
           }
           int end = start + 9;//终止位置
           if (end > pageCount)
           {
               end = pageCount;
               //重新计算start的值。
               start = end - 9>0?end-9:1;//注意总页数小于9.

           }
           //将指定范围的数据遍历
           StringBuilder sb = new StringBuilder();
           if (pageIndex > 1)
           {
               sb.Append(string.Format("<a href='?pageIndex={0}'>上一页</a>", pageIndex - 1));
           }
           for (int i = start; i <= end; i++)
           {
               if (pageIndex == i)
               {
                   sb.Append(i);//如果循环的数字的与当前页码一致，不要加超链接
               }
               else
               {
                   sb.Append(string.Format("<a href='?pageIndex={0}'>{0}</a>",i));//给循环的数字加上超链。
               }
           }
           if (pageIndex <pageCount)
           {
               sb.Append(string.Format("<a href='?pageIndex={0}'>下一页</a>", pageIndex +1));
           }
           return sb.ToString();
       }
    }
}
