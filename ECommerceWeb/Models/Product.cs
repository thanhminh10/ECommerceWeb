using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWeb.Models
{
    public class Product
    {
        public int Id { get; set; } 
        public string? Name { get; set; }

        public string? Description { get; set; }


        public string? ImageURL { get; set; }


        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; } 


        public int Quantity { get; set; }


        [ForeignKey("Category")]
        public int CategoryId { get; set; }


        public virtual Category? Category { get; set; }


    }
}
