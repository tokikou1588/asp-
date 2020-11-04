using BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookShop.BLL
{
   public class Articel_WordsManager
    {
       DAL.Articel_WordsService dal = new DAL.Articel_WordsService();
       /// <summary>
       /// 添加敏感词
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public bool Add(Articel_Words model)
       {
           return dal.Add(model) > 0;
       }
       /// <summary>
       /// 过滤禁用词
       /// </summary>
       /// <param name="msg">用户的评论</param>
       /// <returns></returns>
       public bool CheckForbid(string msg)
       {
           List<string> list = null;
           object obj = Common.CacheHelper.Get("forbid");
           if (obj == null)
           {
               list = dal.GetBannd();//获取所有的禁用词。
               Common.CacheHelper.Set("forbid", list);
           }
           else
           {
               list=obj as List<string>;
               
           }
           string regex = string.Join("|", list.ToArray()); //aa|bb|cc|

           return Regex.IsMatch(msg, regex);//对用户输入的评论进行正则匹配。
       }
       /// <summary>
       /// 对审查词进行过滤
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
       public bool CheckMod(string msg)
       {
           List<string> list = dal.GetMod();//获取所有的禁用词。
           string regex = string.Join("|", list.ToArray()); //aa|bb|cc|
           regex = regex.Replace(@"\", @"\\").Replace("{2}",".{0,2}");
           return Regex.IsMatch(msg, regex);//对用户输入的评论进行正则匹配。
       }
       /// <summary>
       /// 替换词过滤
       /// </summary>
       /// <param name="msg"></param>
       /// <returns></returns>
       public string ReplaceWord(string msg)
       {
           List<Model.Articel_Words> list = dal.GetReplaceWord();
           foreach (Model.Articel_Words model in list)
           {
               msg = msg.Replace(model.WordPattern, model.ReplaceWord);
           }
           return msg;
       }
    }
}
