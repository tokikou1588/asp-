using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.Model.Enum
{
    public enum DelFlag
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal=1,
        /// <summary>
        /// 锁定
        /// </summary>
        LockState=0,

        /// <summary>
        /// 逻辑删除
        /// </summary>
        LogicDelete=2,
        /// <summary>
        /// 物理删除
        /// </summary>
        PhyicDelete=3

    }
}
