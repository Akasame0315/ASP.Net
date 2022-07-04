using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication3.Models
{
    public class Guestbook
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("EmailAddress")]
        public string Email { get; set; }
        public string Comment { get; set; }
    }
}