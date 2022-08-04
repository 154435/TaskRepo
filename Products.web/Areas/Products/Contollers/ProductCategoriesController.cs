using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Products.web.Data;
using Products.web.Models;

namespace Products.web.Areas.Products.Contollers
{
    [Area("Products")]
    public class ProductCategoriesController : Controller
    {
        private readonly PracticeApplicationDbContext _context;

        public ProductCategoriesController(PracticeApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products/ProductCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductCategory.ToListAsync());
        }

        // GET: Products/ProductCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategory
                .FirstOrDefaultAsync(m => m.PCId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // GET: Products/ProductCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/ProductCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName")] ProductCategory category)
        {
            if (ModelState.IsValid)
            {
                // Sanitize the data before consumption
                category.ProductCategoryName = category.ProductCategoryName.Trim();

                // Check for Duplicate CategoryName
                bool isDuplicateFound
                    = _context.ProductCategory.Any(c => c.ProductCategoryName == category.ProductCategoryName);
                if (isDuplicateFound)
                {
                    ModelState.AddModelError("CategoryName", "Duplicate! Another category with same name exists");
                }
                else
                {
                    // Save the changes 
                    _context.Add(category);
                    await _context.SaveChangesAsync();              // update the database
                    return RedirectToAction(nameof(Index));
                }
            }

            // Something must have gone wrong, so return the View with the model error(s).
            return View(category);
        }

        // GET: Products/ProductCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategory.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }
            return View(productCategory);
        }

        // POST: Products/ProductCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PCId,ProductCategoryName")] ProductCategory productCategory)
        {
            if (id != productCategory.PCId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCategoryExists(productCategory.PCId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productCategory);
        }

        // GET: Products/ProductCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productCategory = await _context.ProductCategory
                .FirstOrDefaultAsync(m => m.PCId == id);
            if (productCategory == null)
            {
                return NotFound();
            }

            return View(productCategory);
        }

        // POST: Products/ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productCategory = await _context.ProductCategory.FindAsync(id);
            _context.ProductCategory.Remove(productCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategory.Any(e => e.PCId == id);
        }
    }
}
