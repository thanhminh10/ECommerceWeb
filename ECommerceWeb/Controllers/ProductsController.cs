﻿using System;
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
                Categories = _context.Category.ToList()
            };
            // Calculate the number of products for each category
            foreach (var category in viewModel.Categories)
            {
                category.ProductCount = viewModel.Products.Count(p => p.CategoryId == category.Id);
            }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageURL,Price,Quantity,CategoryId ,BrandId")] Product product , IFormFile ImageURL)
        {
            

            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)

            {
                try
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
                    
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    // Redirect to index action after successful update
                    return RedirectToAction("Index", "Products");
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", product.CategoryId);
            
            return View(product);
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
