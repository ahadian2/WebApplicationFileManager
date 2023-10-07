using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplicationFileManager.Models
{
    public class MyDbContext:DbContext
    {
        public DbSet<FM> FM { get; set; }
        public DbSet<Blog> Blog { get; set; }
    }
}