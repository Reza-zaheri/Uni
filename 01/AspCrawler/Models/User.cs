using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AspCrawler.Models
{
    public class User:IdentityUser
    {
        [MaxLength(50)]
        public string Fname { get; set; }
        [MaxLength(60)]
        public string Lname { get; set; }
    }
}
