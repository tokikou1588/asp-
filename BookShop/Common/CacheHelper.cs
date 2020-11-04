using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
   public class CacheHelper
    {
       /// <summary>
       /// 向缓存中添加数据
       /// </summary>
       /// <param name="key">键</param>
       /// <param name="value">值</param>
       public static void Set(string key, object value)
       {
           
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           cache[key]=value;
          
           
       }
       /// <summary>
       /// 取值
       /// </summary>
       /// <param name="key">键</param>
       public static object Get(string key)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           return cache[key];
          
       }
       public static void Remove(string key)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           cache.Remove(key);
       }
    }
}
