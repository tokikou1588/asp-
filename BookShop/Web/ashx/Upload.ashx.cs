using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace BookShop.Web.ashx
{
    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {
        public string fullDir;
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string action=context.Request["action"];
            if (action == "upload")
            {
                FileUpload(context);
            }
            else if (action == "cut")
            {
                FileCut(context);
            }
            else
            {
                context.Response.Write("no:参数异常!!");
            }
           
        }

        #region 头像上传
         private void FileUpload(HttpContext context)
        {
            HttpPostedFile file = context.Request.Files["Filedata"];
            if (file.ContentLength > 0)
            {
                //判断上传文件的类型.
                string fileName = Path.GetFileName(file.FileName);//文件名。
                string fileExt = Path.GetExtension(fileName);//获取扩展名.
                if (fileExt == ".jpg")
                {
                    string newfileName = Guid.NewGuid().ToString();
                    string dir = "/ImagePath/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                    if (!Directory.Exists(context.Request.MapPath(dir)))//创建文件夹
                    {
                        Directory.CreateDirectory(context.Request.MapPath(dir));
                    }
                    string fullDir = dir + newfileName + fileExt;//构建完整路径.
                    file.SaveAs(context.Request.MapPath(fullDir));
                    //string thumbfileName="thumb_"+Guid.NewGuid().ToString();
                    //string thumPath = "/ImagePath/" + thumbfileName + ".jpg";
                    ////创建缩略图。
                    //Common.ImageClass.MakeThumbnail(context.Request.MapPath(fullDir), context.Request.MapPath(thumPath), 300, 300, "HW");

                    using (Image img = Image.FromFile(context.Request.MapPath(fullDir)))
                    {
                        context.Response.Write("ok:" + fullDir + ":" + img.Width + ":" + img.Height);
                    }
                    //file.SaveAs(context.Request.MapPath("/ImagePath/"+fileName));
                    //context.Response.Write("/ImagePath/" + fileName);

                }
                else
                {
                    context.Response.Write("no:文件格式错误!!");
                }
            }
            else
            {
                context.Response.Write("no:选择上传文件!!");
            }
        }
        #endregion
         #region 头像截取
         private void FileCut(HttpContext context)
         {
            
             int x = Convert.ToInt32(context.Request["x"]);
             int y = Convert.ToInt32(context.Request["y"]);
             int width = Convert.ToInt32(context.Request["width"]);
             int height = Convert.ToInt32(context.Request["height"]);
             string url=context.Request["url"];
             //创建画布
             using (Bitmap map = new Bitmap(width,height))
             {
                 using (Graphics g = Graphics.FromImage(map))
                 {
                     using (Image img = Image.FromFile(context.Request.MapPath(url)))
                     {
                         //将原图指定范围的图片画到画布上。
                         //1：对哪张图片进行操作。
                         //2:画多么大
                         //3:画原图的哪块区域.
                         g.DrawImage(img, new Rectangle(0,0,width,height),new Rectangle(x,y,width,height),GraphicsUnit.Pixel);
                         string cutPhotoName = "cut_" + Guid.NewGuid().ToString();
                         string dir = "/ImagePath/" + cutPhotoName + ".jpg";
                         map.Save(context.Request.MapPath(dir),System.Drawing.Imaging.ImageFormat.Jpeg);
                         //将路径存储到数据库中。
                         context.Response.Write(dir);
                     }

                 
                 }
             }
         }
         #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}