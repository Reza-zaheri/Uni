using System.ComponentModel.DataAnnotations;

namespace AspCrawler.Models
{
    public class ProductListDto
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [Display(Name="قیمت")]
        public string price { get; set; }
        [Display(Name = "توضیحات")]
        public string Category { get; set; }
    }
}
