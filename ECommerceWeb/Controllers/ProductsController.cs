using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ECommerceWeb.Data;
using ECommerceWeb.Models;
using ECommerceWeb.ViewModels;
using Newtonsoft.Json;

namespace ECommerceWeb.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ECommerceWebContext _context;

        public ProductsController(ECommerceWebContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
          
            var viewModel = new ProductCategoryViewModel

            {
                Products = _context.Product.ToList(),
                Categories = _context.Category.ToList(),
               
            };

           

            // Calculate the number of products for each category
            foreach (var category in viewModel.Categories)
            {
                category.ProductCount = viewModel.Products.Count(p => p.CategoryId == category.Id);
            }

            foreach (var brand in _context.Brand)
            {
                brand.ProductCount = viewModel.Products.Count(p => p.BrandId == brand.Id);
            }

            ViewData["Brand"] = _context.Brand.ToList();

            return View(viewModel);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
         
            ViewData["CategoryName"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["BrandName"] = new SelectList(_context.Brand, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,	ImageURL,Price,Quantity,CategoryId,BrandId")] Product product, IFormFile ImageURL)
        {
            if (ModelState.IsValid)
            {
                if (ImageURL != null && ImageURL.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot", "assets", "product_img", ImageURL.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageURL.CopyToAsync(stream);
                    }
                    product.ImageURL = $"/assets/product_img/{ImageURL.FileName}";
                }


                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> UploadImages(int id, [FromForm] IFormFile image1, [FromForm] IFormFile image2)
        {
            var product = await _context.Product.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            if (image1 == null || image2 == null)
            {
                return BadRequest("Please provide two images to upload.");
            }

            string uploadsFolder = Path.Combine("wwwroot", "assets", "product_img");
            string uniqueFileName1 = Guid.NewGuid().ToString() + "_" + image1.FileName;
            string filePath1 = Path.Combine(uploadsFolder, uniqueFileName1);

            string uniqueFileName2 = Guid.NewGuid().ToString() + "_" + image2.FileName;
            string filePath2 = Path.Combine(uploadsFolder, uniqueFileName2);

            using (var stream1 = new FileStream(filePath1, FileMode.Create))
            {
                await image1.CopyToAsync(stream1);
            }

            using (var stream2 = new FileStream(filePath2, FileMode.Create))
            {
                await image2.CopyToAsync(stream2);
            }

            product.ImageURL_02 = $"/assets/product_img/{uniqueFileName1}";
            product.ImageURL_03 = $"/assets/product_img/{uniqueFileName2}";

            _context.SaveChanges();

            return Ok(new { message = "Images uploaded successfully." });
        }



        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
 
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            ViewData["CategoryName"] = new SelectList(_context.Category, "Id", "Name",product.CategoryId);
            ViewData["BrandName"] = new SelectList(_context.Brand, "Id", "Name", product.BrandId);
           
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile ImageURL)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            // Set ModelState.IsValid to true to bypass validation
            ModelState.Clear();

            try
            {
                // Retrieve the existing product from the database
                var existingProduct = await _context.Product.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                // Update properties from the incoming product if they are not null
                if (product.Name != null)
                {
                    existingProduct.Name = product.Name;
                }
                if (product.Description != null)
                {
                    existingProduct.Description = product.Description;
                }
                if (product.Price != 0)
                {
                    existingProduct.Price = product.Price;
                }
                if (product.Quantity.HasValue)
                {
                    existingProduct.Quantity = product.Quantity;
                }
                if (product.CategoryId != 0)
                {
                    existingProduct.CategoryId = product.CategoryId;
                }
                if (product.BrandId.HasValue)
                {
                    existingProduct.BrandId = product.BrandId;
                }

                // Update ImageURL if provided
                if (ImageURL != null && ImageURL.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot", "assets", "product_img", ImageURL.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageURL.CopyToAsync(stream);
                    }
                    existingProduct.ImageURL = $"/assets/product_img/{ImageURL.FileName}";
                }

                // Update the existing product in the database
                _context.Update(existingProduct);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Product updated successfully.", productId = existingProduct.Id });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ECommerceWebContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
