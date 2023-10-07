using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationFileManager.Models.ViewModel
{
    public class GalleryViewModel
    {
        public string ImageName { get; set; }
        public string ImageDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
    }
}