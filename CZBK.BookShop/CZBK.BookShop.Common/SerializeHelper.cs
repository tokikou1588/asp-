using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.Common
{
   public class SerializeHelper
    {
       /// <summary>
       /// 对数据进行序列化
       /// </summary>
       /// <param name="obj"></param>
       /// <returns></returns>
       public static string SerializeToString(object obj)
       {
          return JsonConvert.SerializeObject(obj);
       }
       /// <summary>
       /// 反序列化
       /// </summary>
       /// <typeparam name="T"></typeparam>
       /// <param name="str"></param>
       /// <returns></returns>
       public static T DeserializeToObject<T>(string str)
       {
           return JsonConvert.DeserializeObject<T>(str);
       }
    }
}
