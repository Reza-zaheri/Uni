namespace AspCrawler.Models.Dto
{
    public class ProductDetailsViewModel
    {
        public Product Product { get; set; }
        public string CategoryTitle { get; set; }
        public List<Enroll> EnrollChanges { get; set; }
    }
}
