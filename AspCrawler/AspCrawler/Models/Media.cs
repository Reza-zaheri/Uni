using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspCrawler.Models
{
    public class Media
    {
        [Key]
        public int Id { get; set; }
        [MaxLength]
        public string Data_url { get; set; }
        [MaxLength(30)]
        public string Type { get; set; }
        public string Header { get; set; }
        public DateTime DateTime { get; set; }
        public  int? Id_P { get; set; }


    }
}
