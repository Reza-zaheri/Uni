namespace AspCrawler.Models
{
    public class ProductListViewModel
    {
        public List<pr_digi> Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
