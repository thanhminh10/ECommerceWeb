using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }


        public string? Name { get; set; }

        public string? Code { get; set; }
    }
}
