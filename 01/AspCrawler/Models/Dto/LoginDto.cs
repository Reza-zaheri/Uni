using System.ComponentModel.DataAnnotations;

namespace AspCrawler.Models.Dto
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string UserName {  get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }=false;
        public string ReturnUrl { get; set; }
    }
}
