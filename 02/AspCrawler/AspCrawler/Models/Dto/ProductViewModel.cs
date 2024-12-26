using System.ComponentModel.DataAnnotations;

namespace AspCrawler.Models.Dto
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "نام محصول")]
        public string Name { get; set; }
        [Display(Name = "قیمت")]
        public string Price { get; set; }
        [Display(Name = "کشور")]
        public string Country { get; set; }
        [Display(Name = "واحد پولی")]
        public string unit_p { get; set; }
        [Display(Name = "واحد حجمی")]
        public string unit_s { get; set; }
        public int CategoryId { get; set; }
    }
}
