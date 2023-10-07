using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WebApplicationFileManager.Models;
using WebApplicationFileManager.Models.ViewModel;

namespace WebApplicationFileManager.Controllers
{
    public class BlogController : Controller
    {
        private MyDbContext db = new MyDbContext();

        // GET: Blog
        public ActionResult Index()
        {
            return View(db.Blog.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }
        public ActionResult MyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog post = db.Blog.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            PostViewModel model = new PostViewModel();
            model.PostID = post.PostID;
            model.PostTitle = post.PostTitle;
            model.PostContent = post.PostContent;
            model.PostImage = post.PostImage;
            
            //Gallery
            List<GalleryViewModel> listGallery = new List<GalleryViewModel>();
            if (post.PostGallery != null)
            {
                JObject JsonGallery = JObject.Parse(post.PostGallery);
                if (JsonGallery != null)
                {
                    foreach (var item in JsonGallery.Values())
                    {
                        foreach (var x in item)
                        {
                            listGallery.Add(new GalleryViewModel
                            {
                                ImageName = x["gallery_img_name"].ToString(),
                                ImageDescription = x["gallery_img_description"].ToString(),
                                ImageUrl = x["gallery_img_url"].ToString(),
                                ImageThumbnailUrl = x["gallery_img_thumbnail"].ToString()
                            });
                        }
                    }
                }
            }
            model.PostGallery = listGallery;
            //End Gallery

            //Carouse
            List<CarouselViewModel> listCarouse = new List<CarouselViewModel>();
            if (post.PostCarousel != null)
            {
                JObject JsonCarousel = JObject.Parse(post.PostCarousel);
                if (JsonCarousel != null)
                {
                    foreach (var item in JsonCarousel.Values())
                    {
                        foreach (var x in item)
                        {
                            listCarouse.Add(new CarouselViewModel
                            {
                                ImageName = x["carousel_img_name"].ToString(),
                                ImageDescription = x["carousel_img_description"].ToString(),
                                ImageUrl = x["carousel_img_url"].ToString(),
                                ImageThumbnailUrl = x["carousel_img_thumbnail"].ToString(),
                                ImageLink = x["carousel_img_link"].ToString()
                            });
                        }
                    }
                }
            }
            model.PostCarousel = listCarouse;
            //End Carouse


            return View(model);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,PostTitle,PostContent,PostImage,PostGallery,PostCarousel")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,PostTitle,PostContent,PostImage,PostGallery,PostCarousel")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blog.Find(id);
            db.Blog.Remove(blog);
            db.SaveChanges();
            return RedirectToAction("Index");
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
