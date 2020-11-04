using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CZBK.BookShop.Common
{
   public class CacheHelper
    {
       /// <summary>
       /// 向缓存中存储数据
       /// </summary>
       /// <param name="key"></param>
       /// <param name="value"></param>
       public static void Set(string key, object value)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           cache[key] = value;
       }
       /// <summary>
       /// 从缓存中取数据
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
       public static object Get(string key)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
           return cache[key];
       }
       public static void Delete(string key)
       {
           System.Web.Caching.Cache cache = HttpRuntime.Cache;
            cache.Remove(key);
       }
    }
}
