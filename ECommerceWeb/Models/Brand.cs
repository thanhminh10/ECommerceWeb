using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        public int Name { get; set; }

        public string? Description { get; set; }

        public int? BrandCount { get; set; }
    }
}
