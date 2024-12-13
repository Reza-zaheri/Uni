using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspCrawler.Models.Dto
{
    public class UserEditDto
    {
        [Display(Name = "آیدی")]
        public string Id { get; set; }
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "نام کاربری")]
        public string UserName { get; set; }
    }
}
