using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.Quartz
{
   public  class IndexJob:IJob
    {

       IBLL.IKeyWordsRankService bll = new BLL.KeyWordsRankService();
        public void Execute(JobExecutionContext context)
        {
            bll.DeleteKeyWords();
            bll.InsertKeyWords();
        }
    }
}
