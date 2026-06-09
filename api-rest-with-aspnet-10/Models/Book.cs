using api_rest_with_aspnet_10.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_rest_with_aspnet_10.Models
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Required]
        [Column("title")]
        public string Title { get; set; }

        [Required]
        [Column("author")]
        public string Author { get; set; }

        [Required]
        [Column("price", TypeName = "numeric(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [Column("launch_date", TypeName = "datetime")]
        public DateTime Launch_Date { get; set; }
    }
}
