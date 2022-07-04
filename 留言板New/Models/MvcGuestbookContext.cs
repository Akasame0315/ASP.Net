using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcGuestbook.Models
{
    public class MvcGuestbookContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MvcGuestbookContext() : base("name=MvcGuestbookContext")
        {
        }

        public System.Data.Entity.DbSet<MvcGuestbook.Models.Guestbook> Guestbooks { get; set; }
    }
}
