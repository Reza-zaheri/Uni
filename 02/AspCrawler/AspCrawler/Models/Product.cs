using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace AspCrawler.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public string Price { get; set; }
        [MaxLength]
        public string? Pic_url { get; set; }
        public DateTime? DateTime { get; set; }
        [AllowNull]
        public string unit_p {  get; set; }
        [AllowNull]
        public string unit_s { get; set; }
        [AllowNull]
        public string Country { get; set; }
        [AllowNull]
        public int? CatId { get; set; }
    }
}
