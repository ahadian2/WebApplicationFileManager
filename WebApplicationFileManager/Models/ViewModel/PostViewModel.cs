using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebApplicationFileManager.Models.ViewModel
{
    public class PostViewModel
    {
        [Key]
        [Required]
        [Display(Name = "شناسه پست")]
        public int PostID { get; set; }
        [Required]
        [MaxLength(500)]
        [Display(Name = "تایتل")]
        public string PostTitle { get; set; }
        [Display(Name = "محتوا")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string PostContent { get; set; }
        [Display(Name = "تصویر شاخص")]
        [MaxLength(100)]
        [AllowHtml]
        public string PostImage { get; set; }
        [Display(Name = "گالری ")]
        [AllowHtml]
        public IEnumerable<GalleryViewModel> PostGallery { get; set; }
        [Display(Name = "کروسل ")]
        [AllowHtml]
        public IEnumerable<CarouselViewModel> PostCarousel { get; set; }
    }
}