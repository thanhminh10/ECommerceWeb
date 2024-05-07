using ECommerceWeb.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceWeb.Models
{
    public class Navbar: ViewComponent
    {
        private readonly ECommerceWebContext _context;


        public Navbar(ECommerceWebContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Category.ToList());
        }
    }
}
