using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWeb.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } 
        public string? Name { get; set; }

        public string? Description { get; set; }


        public string? ImageURL { get; set; }


        public string? ImageURL_02 { get; set; }

        public string? ImageURL_03 { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; } 


        public int? Quantity { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }


        public virtual Category? Category { get; set; }


        [ForeignKey("Brand")]
        public int? BrandId { get; set; }


        public virtual Brand? Brand { get; set; }


        [ForeignKey("Color")]
        public int? ColorId { get; set; }


        public virtual Color? Color { get; set; }
    }
}
