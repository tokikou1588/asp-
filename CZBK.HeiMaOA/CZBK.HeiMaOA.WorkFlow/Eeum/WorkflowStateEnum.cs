using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.WorkFlow.Eeum
{
   public enum WorkflowStateEnum
    {
       /// <summary>
       /// 表示通过
       /// </summary>
       IsPass=0,
       /// <summary>
       /// 禁止
       /// </summary>
       Reject=1,
       
       /// <summary>
       /// 驳回
       /// </summary>
       Rebut=2,
           /// <summary>
           /// 流程继续
           /// </summary>
       Continue=3

    }
}
