using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
   public class WebCommon
    {
       /// <summary>
       /// 验证码校验
       /// </summary>
       /// <param name="vcode">用户输入的验证码</param>
       /// <returns></returns>
       public static bool ValidateCode(string vcode)
       {
           HttpContext context=HttpContext.Current;
           bool isSucess = false;
           if (context.Session["vCode"] != null)
           {
               //string vcode = context.Request["vcode"];
               string sysCode = context.Session["vCode"].ToString();
               if (sysCode.Equals(vcode, StringComparison.InvariantCultureIgnoreCase))
               {
                   isSucess = true;
                 //  context.Session["vCode"] = null;//安全，节省内存。
               }
           }
           return isSucess;
       }
       /// <summary>
       /// 对字符串进行MD5运算
       /// </summary>
       /// <param name="str"></param>
       /// <returns></returns>
       public static string GetMd5String(string str)
       {
           MD5 md5 = MD5.Create();
           byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
           byte[]md5Buffer=md5.ComputeHash(buffer);
           StringBuilder sb = new StringBuilder();
           foreach (byte b in md5Buffer)
           {
               sb.Append(b.ToString("x2"));
           }
           md5.Clear();
           return sb.ToString();
       }
       /// <summary>
       /// 跳转到登录页面
       /// </summary>
       public static void GoPage()
       {
           HttpContext context = HttpContext.Current;
           context.Response.Redirect("/Login.aspx?returnUrl="+context.Server.UrlEncode(context.Request.Url.ToString()));
       }
       /// <summary>
       /// 判断Cookie
       /// </summary>
       public static bool ValidateCookieInfo(BookShop.Model.User userInfo)
       {
           bool isSucess=false;
           HttpContext context = HttpContext.Current;
           if (userInfo != null)
           {
               if (context.Request.Cookies["cp2"] != null)
               {
                   string userPwd = context.Request.Cookies["cp2"].Value;
                   if (userInfo.LoginPwd == userPwd)
                   {
                       context.Session["userInfo"] = userInfo;
                       isSucess = true;
                       return isSucess;
                   }
               }
           }
           context.Response.Cookies["cp1"].Expires = DateTime.Now.AddDays(-1);
           context.Response.Cookies["cp2"].Expires = DateTime.Now.AddDays(-1);
           return isSucess;
       }
       
       //5年   5小时30分钟
       /// <summary>
       /// 获取时间差
       /// </summary>
       /// <param name="ts"></param>
       /// <returns></returns>
       public static string GetTimespan(TimeSpan ts)
       {
           if (ts.TotalDays>365)
           {
               return Math.Floor(ts.TotalDays / 365) + "年前";
           }
           else if (ts.TotalDays > 30)
           {
               return Math.Floor(ts.TotalDays/30)+"月前";
           }
           else if (ts.TotalHours > 24)
           {
               return Math.Floor(ts.TotalDays)+"天前";
           }
           else if (ts.TotalHours >1)
           {
               return Math.Floor(ts.TotalHours) + "小时前";
           }
           else if (ts.TotalMinutes > 1)
           {
               return Math.Floor(ts.TotalMinutes) + "分钟前";
           }
           else
           {
               return "刚刚";
           }
       }

    }
}
