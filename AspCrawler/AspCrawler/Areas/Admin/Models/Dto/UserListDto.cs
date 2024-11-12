using System.ComponentModel.DataAnnotations;

namespace AspCrawler.Areas.Admin.Models.Dto
{
    public class UserListDto
    {
        public string Id { get; set; }
        //[Display(Name ="نام")]
        public string Fname { get; set; }
        //[Display(Name = "نام خانوادگی")]
        public string Lname { get; set; }
        public string Username { get; set; }
        public bool EmailConfirmed { get; set; }
        public int AccessFailedCount { get; set; }
    }
}
