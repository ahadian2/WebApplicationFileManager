using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebApplicationFileManager.Models
{
    [Table(name: "T_FM")]
    public class FM
    {
        [Key]
        [Display(Name = "شماره همتای فایل")]
        public int FileId { get; set; }
        [Display(Name = "تارنمای فایل")]
        [Required]
        [MaxLength(100)]
        public string FileUrl { get; set; }
        [Display(Name = "نام فایل")]
        [MaxLength(100)]
        public string FileName { get; set; }
        [Display(Name = "توضیحات فایل")]
        [MaxLength(400)]
        [DataType(DataType.MultilineText)]
        public string FileDescription { get; set; }
        [Display(Name = "سایز فایل")]
        [Required]
        public int FileSize { get; set; }
        [Display(Name = "تعداد دانلود فایل")]
        public int FileDownload { get; set; }
        [Display(Name = "فرمت فایل")]
        [Required]
        [MaxLength(10)]
        public string FileExeption { get; set; }
        [Display(Name = "تاریخ ثبت فایل")]
        [Required]
        public DateTime FileRegisterDate { get; set; }
    }
}