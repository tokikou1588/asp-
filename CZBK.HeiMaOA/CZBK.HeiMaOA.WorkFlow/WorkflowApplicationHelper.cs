using System;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CZBK.HeiMaOA.WorkFlow
{
   public class WorkflowApplicationHelper
    {
        static AutoResetEvent syncEvent = new AutoResetEvent(false);
        public static WorkflowApplication CreateWorkflowApplication(Activity activity, IDictionary<string, object> dict)
        {

            WorkflowApplication appliction = new WorkflowApplication(activity, dict);

            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(ConfigurationManager.AppSettings["workflowConnection"]);

            appliction.InstanceStore = store;//workflow存储到数据库中。

            appliction.Unloaded += OnUloaded;
            appliction.Aborted += OnAborted;
            appliction.Completed += OnCompleted;
            appliction.Idle += OnIdle;
            appliction.PersistableIdle += OnPersistableIdle;
            appliction.OnUnhandledException += OnUnhandledException;
            appliction.Run();
            return appliction;
        }
        private static UnhandledExceptionAction OnUnhandledException(WorkflowApplicationUnhandledExceptionEventArgs arg)
        {
            Console.WriteLine("异常了!!");
            syncEvent.Set();
            return UnhandledExceptionAction.Abort;
        }

        private static PersistableIdleAction OnPersistableIdle(WorkflowApplicationIdleEventArgs arg)
        {
            Console.WriteLine("持久化");
            return PersistableIdleAction.Unload;
        }

        private static void OnIdle(WorkflowApplicationIdleEventArgs obj)
        {
            syncEvent.Set();
            Console.WriteLine("工作流空闲!!");
        }

        private static void OnCompleted(WorkflowApplicationCompletedEventArgs obj)
        {
            syncEvent.Set();
            Console.WriteLine("工作流完成了!!");
        }

        private static void OnAborted(WorkflowApplicationAbortedEventArgs obj)
        {
            syncEvent.Set();
            Console.WriteLine("工作流终止了!!");
        }

        private static void OnUloaded(WorkflowApplicationEventArgs obj)
        {
            syncEvent.Set();
            Console.WriteLine("工作流卸载");
        }

        public static WorkflowApplication LoadWorkflowApplication(Activity activity, Guid guid)
        {
            WorkflowApplication appliction = new WorkflowApplication(activity);

            SqlWorkflowInstanceStore store = new SqlWorkflowInstanceStore(ConfigurationManager.AppSettings["workflowConnection"]);

            appliction.InstanceStore = store;//workflow存储到数据库中。

            appliction.Unloaded += OnUloaded;
            appliction.Aborted += OnAborted;
            appliction.Completed += OnCompleted;
            appliction.Idle += OnIdle;
            appliction.PersistableIdle += OnPersistableIdle;
            appliction.OnUnhandledException += OnUnhandledException;
            //this.textBox1.Text = appliction.Id.ToString();
            appliction.Load(guid);
            return appliction;
        }
    }
}
