using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using PanGu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CZBK.BookShop.Common
{
   public class WebCommon
    {
        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static String GetStreamMD5(Stream stream)
        {
            string strResult = "";
            string strHashData = "";
            byte[] arrbytHashValue;
            System.Security.Cryptography.MD5CryptoServiceProvider oMD5Hasher =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            arrbytHashValue = oMD5Hasher.ComputeHash(stream); //计算指定Stream 对象的哈希值
            //由以连字符分隔的十六进制对构成的String，其中每一对表示value 中对应的元素；例如“F-2C-4A”
            strHashData = System.BitConverter.ToString(arrbytHashValue);
            //替换-
            strHashData = strHashData.Replace("-", "");
            strResult = strHashData;
            return strResult;
        }
       /// <summary>
       /// 跳转到登录页面，同时将访问的页面的URL地址传过去。
       /// </summary>
        public static void GoPage()
        {

            HttpContext.Current.Response.Redirect("/Login/Index/?returnUrl="+HttpContext.Current.Request.Url.ToString());
        }
       /// <summary>
       /// 对字符串进行MD5运算
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public static string Md5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            byte[]md5Buffer=md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in md5Buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
       /// <summary>
       /// 对字符串进行截取
       /// </summary>
       /// <param name="str"></param>
       /// <param name="length"></param>
       /// <returns></returns>
        public static string CutString(string str, int length)
        {
            if (str.Length > length)
            {
                return str.Substring(0, length) + ".............";
            }
            else
            {
                return str;
            }
        }
       /// <summary>
       /// 获取文件路径
       /// </summary>
       /// <param name="time"></param>
       /// <returns></returns>
        public static string GetDir(DateTime time)
        {
            return "/HtmlPage/"+time.Year+"/"+time.Month+"/"+time.Day+"/";
        }
       /// <summary>
       /// 对时间差进行处理，转成字符串
       /// </summary>
        /// <param name="time">3天5小时20分钟-->   time.TotalHours=</param>
       /// <returns></returns>
        public static string GetTimeSpan(TimeSpan time)
        {
          
            if (time.TotalDays >= 365)
            {
                return Math.Floor(time.TotalDays / 365) + "年前";
            }
            else if (time.TotalDays >= 30)
            {
                return Math.Floor(time.TotalDays/30)+"月前";
            }
            else if (time.TotalHours >= 24)
            {
                return Math.Floor(time.TotalDays)+"天前";
            }
            else if (time.TotalHours >= 1)
            {
                return Math.Floor(time.TotalHours) + "小时前";
            }
            else if (time.TotalMinutes >= 1)
            {
                return Math.Floor(time.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚:";
            }
        }
       /// <summary>
       /// 将UBB替换词HTML
       /// </summary>
       /// <param name="argString"></param>
       /// <returns></returns>
        public static string DecodeUBBToHtml(string argString)
        {
            string tString = argString;
            if (tString != "")
            {
                Regex tRegex;
                bool tState = true;
                tString = tString.Replace("&", "&amp;");
                tString = tString.Replace(">", "&gt;");
                tString = tString.Replace("<", "&lt;");
                tString = tString.Replace("\"", "&quot;");
                tString = Regex.Replace(tString, @"\[br\]", "<br />", RegexOptions.IgnoreCase);
                string[,] tRegexAry = {
          {@"\[p\]([^\[]*?)\[\/p\]", "$1<br />"},
          {@"\[b\]([^\[]*?)\[\/b\]", "<b>$1</b>"},
          {@"\[i\]([^\[]*?)\[\/i\]", "<i>$1</i>"},
          {@"\[u\]([^\[]*?)\[\/u\]", "<u>$1</u>"},
          {@"\[ol\]([^\[]*?)\[\/ol\]", "<ol>$1</ol>"},
          {@"\[ul\]([^\[]*?)\[\/ul\]", "<ul>$1</ul>"},
          {@"\[li\]([^\[]*?)\[\/li\]", "<li>$1</li>"},
          {@"\[code\]([^\[]*?)\[\/code\]", "<div class=\"ubb_code\">$1</div>"},
          {@"\[quote\]([^\[]*?)\[\/quote\]", "<div class=\"ubb_quote\">$1</div>"},
          {@"\[color=([^\]]*)\]([^\[]*?)\[\/color\]", "<font style=\"color: $1\">$2</font>"},
          {@"\[hilitecolor=([^\]]*)\]([^\[]*?)\[\/hilitecolor\]", "<font style=\"background-color: $1\">$2</font>"},
          {@"\[align=([^\]]*)\]([^\[]*?)\[\/align\]", "<div style=\"text-align: $1\">$2</div>"},
          {@"\[url=([^\]]*)\]([^\[]*?)\[\/url\]", "<a href=\"$1\">$2</a>"},
          {@"\[img\]([^\[]*?)\[\/img\]", "<img src=\"$1\" />"}
        };
                while (tState)
                {
                    tState = false;
                    for (int ti = 0; ti < tRegexAry.GetLength(0); ti++)
                    {
                        tRegex = new Regex(tRegexAry[ti, 0], RegexOptions.IgnoreCase);
                        if (tRegex.Match(tString).Success)
                        {
                            tState = true;
                            tString = Regex.Replace(tString, tRegexAry[ti, 0], tRegexAry[ti, 1], RegexOptions.IgnoreCase);
                        }
                    }
                }
            }
            return tString;
        }
       /// <summary>
       /// 对输入的搜索条件按照盘古进行分词
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
        public static string[] PanGuSplitWord(string str)
        {
            List<string> list = new List<string>();
            Analyzer analyzer = new PanGuAnalyzer();
            TokenStream tokenStream = analyzer.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
            while ((token = tokenStream.Next()) != null)
            {
              list.Add(token.TermText());
            }
            return list.ToArray();
        }


        // /创建HTMLFormatter,参数为高亮单词的前后缀
        public static string CreateHightLight(string keywords, string Content)
        {
            PanGu.HighLight.SimpleHTMLFormatter simpleHTMLFormatter =
             new PanGu.HighLight.SimpleHTMLFormatter("<font color=\"red\">", "</font>");
            //创建Highlighter ，输入HTMLFormatter 和盘古分词对象Semgent
            PanGu.HighLight.Highlighter highlighter =
            new PanGu.HighLight.Highlighter(simpleHTMLFormatter,
            new Segment());
            //设置每个摘要段的字符数
            highlighter.FragmentSize = 150;
            //获取最匹配的摘要段
            return highlighter.GetBestFragment(keywords, Content);

        }


    }
}
