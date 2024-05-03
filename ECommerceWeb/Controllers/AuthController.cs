using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceWeb.Data;
using ECommerceWeb.Models;

namespace ECommerceWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly ECommerceWebContext _context;

        public AuthController(ECommerceWebContext context)
        {
            _context = context;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp(User user)
        {
            Console.WriteLine(user);
            if (ModelState.IsValid)
            {
                _context.User.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)

        {
            Console.WriteLine(user.Username);
            Console.WriteLine(user.Password);

            var existingUser = _context.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (existingUser != null)
            {
                // Authentication successful
                // You can implement your authentication logic here
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(user);
        }
    }
}

