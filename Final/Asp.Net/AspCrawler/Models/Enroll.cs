using System.ComponentModel.DataAnnotations;

namespace AspCrawler.Models
{
    public class Enroll
    {
        [Key] 
        public int Id { get; set; }
        public string Price {  get; set; }
        public DateTime Time { get; set; }
        public int? IdP {  get; set; }
    }
}
