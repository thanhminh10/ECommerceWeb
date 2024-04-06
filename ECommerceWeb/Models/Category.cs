﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }


    }
}