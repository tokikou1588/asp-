using ServiceStack.Redis;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CZBK.HeiMaOA.SpringNetDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IApplicationContext ctx = ContextRegistry.GetContext(); //创建容器.
            IUserInfoService lister = (IUserInfoService)ctx.GetObject("UserInfoService");
            MessageBox.Show(lister.ShowMsg());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<ActionInfo> actionlist = new List<ActionInfo>();
            actionlist.Add(new ActionInfo { ID = 1 });
            actionlist.Add(new ActionInfo { ID = 2 });
            actionlist.Add(new ActionInfo { ID = 1 });
            actionlist.Add(new ActionInfo { ID = 3 });
            actionlist.Add(new ActionInfo { ID = 1 });
          var list=actionlist.Except(actionlist.Distinct(new ActionEqualCompare()));
          foreach (var item in list)
            {
                Console.WriteLine(item.ID);
            }
           // Console.ReadKey();
        }
        public class ActionInfo
        {
            public int ID { get; set; }
        }
        public class ActionEqualCompare : IEqualityComparer<ActionInfo>
        {
            public bool Equals(ActionInfo x, ActionInfo y)
            {
                return x.ID.Equals(y.ID);
            }

            public int GetHashCode(ActionInfo obj)
            {
                return obj.GetHashCode();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var client = new RedisClient("127.0.0.1", 6379);
            client.Set<string>("pwd", "1111");
            string pwd = client.Get<string>("pwd");
            MessageBox.Show(pwd.ToString());

            client.SetEntryInHash("user", "name", "aa");
            client.SetEntryInHash("user", "sex", "nv");


        }
    }
}
