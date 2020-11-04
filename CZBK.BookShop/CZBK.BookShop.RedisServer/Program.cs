
using log4net;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CZBK.BookShop.RedisServer
{
    class Program
    {
        public static IRedisClientsManager clientManager = new PooledRedisClientManager(new string[] { "127.0.0.1:6379" });
        public static IRedisClient redisClent = clientManager.GetClient();
        static void Main(string[] args)
        {
            //string logFilePath = Server.MapPath("/Log/");
            log4net.Config.XmlConfigurator.Configure();//读取Log4Net配置信息
            ThreadPool.QueueUserWorkItem((a) =>
            {
                while (true)//线程必须一值检测队列中是否有数据。
                {
                    // if (MyExceptionAttribute.queueException.Count > 0)
                    try
                    {
                        if (redisClent.GetListCount("errorMsg") > 0)
                        {
                            ///  Exception exception = MyExceptionAttribute.queueException.Dequeue();
                            string errorMsg = redisClent.DequeueItemFromList("errorMsg");
                            // if (exception != null)
                            if (!string.IsNullOrEmpty(errorMsg))
                            {
                                //string filePath = logFilePath + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";//日志存储的完整路径.
                                //File.AppendAllText(filePath, exception.ToString(), Encoding.UTF8);
                                ILog logger = LogManager.GetLogger("errorMsg");
                                logger.Error(errorMsg);

                            }
                            else
                            {
                                Thread.Sleep(3000);
                            }
                        }
                        else
                        {
                            Thread.Sleep(3000);//如果队列中没有数据，休息避免造成CPU空转。
                        }
                    }
                    catch
                    {

                    }
                }


            });
            Console.ReadKey();
        }
    }
}
