using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;


namespace AspCrawler.Models
{
    public class pr_digi
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int p_id { get; set; }
        [Required]
        [StringLength(150)]
        public string p_title { get; set; }
		[StringLength(100)]
        public string p_price { get; set; }
		[StringLength(150)]
        public string p_img { get; set; }
    }
}
