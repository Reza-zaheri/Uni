using System.ComponentModel.DataAnnotations;

namespace AspCrawler.Models
{
    public class Fluctuation
    {
        [Key] 
        public int Id { get; set; }
        public int price {  get; set; }
        public string description { get; set; }
        public int? IdP {  get; set; }
    }
}
