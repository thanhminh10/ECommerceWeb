using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class User
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }


        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Role { get; set; }
    }
}
