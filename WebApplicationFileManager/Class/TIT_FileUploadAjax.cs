using Microsoft.SqlServer.Server;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationFileManager
{
    public class TIT_FileUploadAjax
    {
        public HttpPostedFileBase Files { get; set; }
        public int MaxContentLength { get; set; }
        public int ContentLength { get; set; }
        public string[] AccessFormat { get; set; }
        public string Exeption { get; set; }
        public string UploadPath { get; set; }
        public string UploadThumbnailPath { get; set; }
        public string UrlUpload { get; set; }
        public string Message { get; set; }

        public bool Upload(HttpPostedFileBase files, int maxContentLength, string[] accessFormat, string uploadPath, string uploadThumbnailPath)
        {
            this.Files = files;
            this.MaxContentLength = (maxContentLength * 1024) * 1024; ;
            this.AccessFormat = accessFormat;
            this.UploadPath = "/" + uploadPath + "/";
            this.UploadThumbnailPath = "/" + uploadThumbnailPath + "/";
            this.ContentLength=files.ContentLength;

            try
            {
                bool exists_UploadPath = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(UploadPath));
                if (!exists_UploadPath)
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(UploadPath));

                bool exists_UploadThumbnailPath = System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(UploadThumbnailPath));
                if (!exists_UploadThumbnailPath)
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(UploadThumbnailPath));


                if (Files.ContentLength > 0)
                {

                    string format = Path.GetExtension(Files.FileName).ToLower().Replace(".", "");
                    this.Exeption = Path.GetExtension(Files.FileName).ToLower().Replace(".", "");
                    string NewName = Guid.NewGuid().ToString().Replace("-", "");
                    string FileUploadPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(UploadPath), NewName + "." + Exeption);
                    string FileuploadThumbnailPath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(UploadThumbnailPath), NewName + "." + Exeption);

                    if (Files.ContentLength < MaxContentLength)
                    {
                        if (AccessFormat.Contains(Exeption))
                        {
                            files.SaveAs(FileUploadPath);
                            UrlUpload = UploadPath+ NewName + "." + Exeption;
                            if (Exeption == "jpg" || Exeption == "png" || Exeption == "gif")
                            {
                                Image image = Image.FromFile(FileUploadPath);
                                using (Image Imgthumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero))
                                {
                                    Imgthumb.Save(FileuploadThumbnailPath);
                                }
                            }

                            Message = "فایل شما با موفقیت ارسال شد";
                            return true;
                        }
                        else
                        {
                            Message = "فرمت فایل مجاز نیست. ";
                            return false;
                        }
                    }
                    else
                    {
                        Message = "حجم فایل آپلود شده مجاز نیست. ";
                        return false;
                    }
                }
                else
                {
                    Message = "تصویری برای آپلود ارسال نشده است";
                    return false;
                }
            }
            catch (Exception ex)
            {
                Message = "ERROR:" + ex.Message.ToString();
                return false;
            }
        }

        public bool Remove(string Address, string Addressthumbnail)
        {
            try
            {
                bool Exist = System.IO.File.Exists(Address);
                if (Exist)
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(Address);
                }
                bool Exist2 = System.IO.File.Exists(Addressthumbnail);
                if (Exist2)
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(Addressthumbnail);
                }
                Message = "فایل با موفقیت حذف شد.";
                return true;
            }
            catch
            {
                Message="حذف فایل انجام نشد. خطا پیش آمده است.";
                return false;

            }
        }

    }
}