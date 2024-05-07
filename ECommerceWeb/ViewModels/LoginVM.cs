﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="User Name is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool? RememberMe { get; set; }
    }
}
