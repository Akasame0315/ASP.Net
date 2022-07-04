using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcGuestbook.Models
{
    public class Guestbook
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("EmailAddress")]
        
        public string Email { get; set; }
        [Required]
        public string comment { get; set; }

    }
}