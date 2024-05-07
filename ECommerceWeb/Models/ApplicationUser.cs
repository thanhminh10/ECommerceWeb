using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class ApplicationUser: IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? Name {  get; set; }

        public string? Address { get; set; }

    }
}
