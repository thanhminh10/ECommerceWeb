using ECommerceWeb.Data;
using ECommerceWeb.Models;
using ECommerceWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ECommerceWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ECommerceWebContext _context;

        public HomeController(ILogger<HomeController> logger , ECommerceWebContext context)
        {
            _logger = logger;
            _context = context;
        }

        public  IActionResult Index()
        {

            var viewModel = new ProductCategoryViewModel

            {
                Products = _context.Product.ToList(),
                Categories = _context.Category.ToList()
            };


            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}