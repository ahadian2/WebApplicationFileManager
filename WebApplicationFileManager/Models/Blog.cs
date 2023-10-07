using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace WebApplicationFileManager.Models
{
    [Table(name: "T-Post")]
    public class Blog
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
        public string PostGallery { get; set; }
        [Display(Name = "کروسل ")]
        [AllowHtml]
        public string PostCarousel { get; set; }
    }
}