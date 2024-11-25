using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace AspCrawler.Models
{
    public class Role:IdentityRole
    {
        public string Descirption { get; set; }
    }
}
