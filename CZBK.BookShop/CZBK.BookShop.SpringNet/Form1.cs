﻿using Memcached.ClientLibrary;
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

namespace CZBK.BookShop.SpringNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            IUserInfoService lister = (IUserInfoService)ctx.GetObject("UserInfoService");
            MessageBox.Show(lister.ShowMsg("你好"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Memcache服务端IP地址
            string[] serverlist = { "127.0.0.1:11211", "10.0.0.132:11211" };

            //初始化池
            SockIOPool pool = SockIOPool.GetInstance();
            pool.SetServers(serverlist);

            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;

            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;

            pool.MaintenanceSleep = 30;
            pool.Failover = true;

            pool.Nagle = false;
            pool.Initialize();

            // 获得客户端实例
            MemcachedClient mc = new MemcachedClient();
            mc.EnableCompression = false;

            Console.WriteLine("------------测  试-----------");
            mc.Set("test", "my value");  //存储数据到缓存服务器，这里将字符串"my ue"缓存，key 是"test"

            if (mc.KeyExists("test"))   //测试缓存存在key为test的项目
            {
                Console.WriteLine("test is Exists");
                Console.WriteLine(mc.Get("test").ToString());  //在缓存中获取key为test的目
            }
            else
            {
                Console.WriteLine("test not Exists");
            }

            Console.ReadLine();

            mc.Delete("test");  //移除缓存中key为test的项目

            if (mc.KeyExists("test"))
            {
                Console.WriteLine("test is Exists");
                Console.WriteLine(mc.Get("test").ToString());
            }
            else
            {
                Console.WriteLine("test not Exists");
            }
            Console.ReadLine();
            
            SockIOPool.GetInstance().Shutdown();  //关闭池， 关闭sockets
        }
    }
}
