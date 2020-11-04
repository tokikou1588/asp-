using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.QuartzServer
{
    public class IndexJob:IJob
    {
        /// <summary>
        /// 具体的任务
        /// </summary>
        /// <param name="context"></param>
        IBLL.IkeyWordsRankService KeyWordService = new BLL.keyWordsRankService();
        public void Execute(JobExecutionContext context)
        {
            KeyWordService.DeleteAll();
            KeyWordService.InsertKeyWordRank();
        }
    }
}
