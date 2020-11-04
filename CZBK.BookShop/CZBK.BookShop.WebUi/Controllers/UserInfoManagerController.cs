using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CZBK.BookShop.WebUi.Controllers
{
    public class UserInfoManagerController : Controller
    {
        //
        // GET: /UserInfoManager/
       
        public ActionResult Index()
        {
            return View();
        }
        #region 获取上传的文件数据
        public ActionResult FileUpload()
        {
            HttpPostedFileBase file  = Request.Files["Filedata"];//获取文件数据.
            string fileName = Path.GetFileName(file.FileName);//文件名
            string fileExt = Path.GetExtension(fileName);//获取文件扩展名.
            if (fileExt == ".jpg")
            {
                string dir = "/UploadImage/" + DateTime.Now.Year + "/" + DateTime.Now.Month + "/" + DateTime.Now.Day + "/";
                Directory.CreateDirectory(Path.GetDirectoryName(Request.MapPath(dir)));//创建目录.
                string fullDir = dir + Common.WebCommon.GetStreamMD5(file.InputStream) + fileExt;//构建了完整的路径.
                string thumbnailImagePath = "/UploadImage/" + Guid.NewGuid().ToString() + fileExt;//缩略图的路径
             
                file.SaveAs(Request.MapPath(fullDir));
                //生成缩略图.
                Common.ImageClass.MakeThumbnail(Request.MapPath(fullDir), Request.MapPath(thumbnailImagePath), 200, 300, "W");
                //添加水印.
                Common.ImageClass.LetterWatermark(Request.MapPath(thumbnailImagePath), 14, "传智播客", Color.Red, "LB");

                System.IO.File.Delete(Request.MapPath(fullDir));//删除原图.
                //根据缩略图创建Image(Image的高度与宽度就是缩略图的高度与宽度)
                using (Image img = Image.FromFile(Request.MapPath(thumbnailImagePath)))
                {
                    return Content("ok:" + thumbnailImagePath+":"+img.Width+":"+img.Height);
                }
               
            }
            else
            {
                return Content("no:文件类型错误!!");
            }
           

        }
        #endregion

        #region 开始进行图片截取.
        public ActionResult CutPhoto()
        {
            
            int x = int.Parse(Request["x"]);
            int y = int.Parse(Request["y"]);
            int width = int.Parse(Request["width"]);
            int height = int.Parse(Request["height"]);
            string imgUrl=Request["imgUrl"];
            using (Bitmap map = new Bitmap(width, height))//创建画布
            {
                //创建画笔.
                using (Graphics g = Graphics.FromImage(map))//为画布创建画笔
                {
                    using (Image img = Image.FromFile(Request.MapPath(imgUrl)))
                    {
                        //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            //第一个参数:对哪张图片进行操作
            //二：画多么大。
            //三：画(img)哪一部分

                        g.DrawImage(img, new Rectangle(0, 0, width, height), new Rectangle(x, y, width, height), GraphicsUnit.Pixel);
                        string newfileName=Guid.NewGuid().ToString();
                        map.Save(Request.MapPath("/UploadImage/" + newfileName + ".jpg"), System.Drawing.Imaging.ImageFormat.Jpeg);//保存截取的头像
                        
                        return Content("/UploadImage/" + newfileName + ".jpg");
                    }
                }
            }
        }
        #endregion
    }
}
