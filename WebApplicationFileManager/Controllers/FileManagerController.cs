using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationFileManager.Models;

namespace WebApplicationFileManager.Controllers
{
    public class FileManagerController : Controller
    {

        private MyDbContext db = new MyDbContext();
        private string[] AllowedFormat = { "jpg", "png", "gif", "txt", "zip" };

        private string FileAddress = "File/FimeManager";

        private string ImageAddressthumbnail = "Images-thumb";

        private int MaxContentLength = 5;
        public JsonResult Upload()
        {
            FM MyFile = new FM();
            TIT_FileUploadAjax fu = new TIT_FileUploadAjax();
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                bool x = fu.Upload(file, MaxContentLength, AllowedFormat, FileAddress, ImageAddressthumbnail + "/" + FileAddress);
                if (x)
                {
                    MyFile.FileName = null;
                    MyFile.FileDescription = null;
                    MyFile.FileDownload = 0;
                    MyFile.FileRegisterDate = DateTime.Now;
                    MyFile.FileExeption = fu.Exeption;
                    MyFile.FileSize = fu.ContentLength;
                    MyFile.FileUrl = fu.UrlUpload;
                    db.FM.Add(MyFile);
                    db.SaveChanges();
                    return Json(new { uploaded = true, Message = fu.Message, file = fu.UrlUpload, JsonRequestBehavior.AllowGet });
                }
                else
                {
                    return Json(new { uploaded = false, Message = fu.Message, JsonRequestBehavior.AllowGet });
                }
            }
            return Json(new { Message = "اتمام بارگزاری", JsonRequestBehavior.AllowGet });
        }
        public JsonResult LoadFiles(int PageId, int Take = 30)
        {
            int Count = db.FM.Count();
            int Skip = (PageId - 1) * Take;
            int PageCount = Count / Take;
            if (Count % Take != 0)
            {
                PageCount++;
            }
            var file = db.FM.OrderByDescending(t => t.FileRegisterDate).Skip(Skip).Take(Take).ToList();
            return Json(new { success = true, file, PageCount = PageCount, PageId = PageId + 1, JsonRequestBehavior.AllowGet });
        }
        public JsonResult LoadGallery(int PageId, int Take = 30)
        {
            int Count = db.FM.Where(t => t.FileExeption == "jpg" || t.FileExeption == "png" || t.FileExeption == "gif").Count();
            int Skip = (PageId - 1) * Take;
            int PageCount = Count / Take;
            if (Count % Take != 0)
            {
                PageCount++;
            }
            var file = db.FM.OrderByDescending(t => t.FileRegisterDate).
            Where(t => t.FileExeption == "jpg" || t.FileExeption == "png" || t.FileExeption == "gif").
            Skip(Skip).Take(Take).ToList();
            return Json(new { success = true, file, PageCount = PageCount, PageId = PageId + 1, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        public JsonResult GetFile(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, JsonRequestBehavior.AllowGet });
            }
            FM myFile = db.FM.Find(id);
            if (myFile == null)
            {
                return Json(new { success = false, JsonRequestBehavior.AllowGet });
            }
            return Json(new { success = true, myFile, JsonRequestBehavior.AllowGet });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RemoveFile(int id)
        {
            try
            {
                FM myFile = db.FM.Find(id);
                string Address = System.Web.HttpContext.Current.Server.MapPath(myFile.FileUrl);
                bool Exist = System.IO.File.Exists(Address);
                if (Exist)
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(Address);
                }
                string Address2 = System.Web.HttpContext.Current.Server.MapPath("/" + ImageAddressthumbnail + myFile.FileUrl);
                bool Exist2 = System.IO.File.Exists(Address2);
                if (Exist2)
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    System.IO.File.Delete(Address2);
                }
                db.FM.Remove(myFile);
                db.SaveChanges();
                return Json(new { success = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex, JsonRequestBehavior.AllowGet });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateFile([Bind(Include = "FileId,FileUrl,FileName,FileDescription,FileSize,FileDownload,FileExeption")] FM myFile)
        {
            myFile.FileRegisterDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(myFile).State = EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, JsonRequestBehavior.AllowGet });
            }
            return Json(new { success = false, JsonRequestBehavior.AllowGet });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}