using CZBK.BookShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.BLL
{
   public partial class SettingsService:BaseService<Settings>,IBLL.ISettingsService
    {
       /// <summary>
       /// 根据配置项配置获取对应的值
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
        public string GetValue(string name)
        {
           // object obj=Common.CacheHelper.Get("setting_"+name);
            object obj = Common.MemcacheHelper.Get("setting_" + name);
            if (obj == null)
            {
               string value= this.DbSession.SettingsDal.LoadEntities(c => c.Name == name).FirstOrDefault().Value;
               Common.MemcacheHelper.Set("setting_" + name, value);
               return value;
            }
            return obj.ToString();
        }
    }
}
