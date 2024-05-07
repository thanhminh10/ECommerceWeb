using ECommerceWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Models
{
    public class Search :ViewComponent
    {

        private readonly ECommerceWebContext _context;
        public Search(ECommerceWebContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            return View(_context.Category.ToList());
        }
    }
}
