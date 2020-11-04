using CZBK.HeiMaOA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.IDAL
{
    /// <summary>
    /// 由于每一个接口中，都需要定义CURD，那么造成重复了。所以我们这里可以封装一下。
    /// </summary>
    public partial interface IUserInfoDal : IBaseDal<UserInfo>
    {
     //定义自己特有的数据操作的方法。
    }

  
}
