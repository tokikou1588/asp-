using NVelocity;
using NVelocity.App;
using NVelocity.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.BookShop.NVelocity
{
    class Program
    {
        static void Main(string[] args)
        {
            VelocityEngine vltEngine = new VelocityEngine();//声明模板引擎
            vltEngine.SetProperty(RuntimeConstants.RESOURCE_LOADER, "file");//操作文件
            vltEngine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, @"D:\传智讲课\0721班\第十一天\CZBK.BookShop\CZBK.BookShop.NVelocity");//操作文件的路径
            vltEngine.Init();//完成初始化

            VelocityContext vltContext = new VelocityContext();//指定模板中显示的内容.
            vltContext.Put("PageTitle", "图书标题");//替换模板文件中的占位符

            List<Person> persons = new List<Person>();
            persons.Add(new Person() { Name = "老王", Age = 10 });
            persons.Add(new Person() { Name = "ddd", Age = 12 });
            vltContext.Put("persons", persons);

            Template vltTemplate = vltEngine.GetTemplate("Template.html");//具体的模板文件名称
            System.IO.StringWriter vltWriter = new System.IO.StringWriter();
            vltTemplate.Merge(vltContext, vltWriter);//替换掉模板中占位符以后，把内容放到StringWriter
            Console.WriteLine(vltWriter.GetStringBuilder().ToString());
            Console.ReadKey();
        }
    }
}
