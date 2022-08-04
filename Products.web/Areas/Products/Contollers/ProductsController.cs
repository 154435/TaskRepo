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
    public class ProductsController : Controller
    {
        private readonly PracticeApplicationDbContext _context;

        public ProductsController(PracticeApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products/Products
        public async Task<IActionResult> Index()
        {
            var practiceApplicationDbContext = _context.Products.Include(p => p.Category).Include(p => p.DeliveryDetails);
            return View(await practiceApplicationDbContext.ToListAsync());
        }

        // GET: Products/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.DeliveryDetails)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Products/Create
        public IActionResult Create()
        {
            ViewData["PCID"] = new SelectList(_context.ProductCategory, "PCId", "ProductCategoryName");
            ViewData["CustomerID"] = new SelectList(_context.customers, "CustomerID", "CustomerAddress");
            return View();
        }

        // POST: Products/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Quantity,Deliverydate,PCID,CustomerID")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PCID"] = new SelectList(_context.ProductCategory, "PCId", "ProductCategoryName", product.PCID);
            ViewData["CustomerID"] = new SelectList(_context.customers, "CustomerID", "CustomerAddress", product.CustomerID);
            return View(product);
        }

        // GET: Products/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["PCID"] = new SelectList(_context.ProductCategory, "PCId", "ProductCategoryName", product.PCID);
            ViewData["CustomerID"] = new SelectList(_context.customers, "CustomerID", "CustomerAddress", product.CustomerID);
            return View(product);
        }

        // POST: Products/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Quantity,Deliverydate,PCID,CustomerID")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["PCID"] = new SelectList(_context.ProductCategory, "PCId", "ProductCategoryName", product.PCID);
            ViewData["CustomerID"] = new SelectList(_context.customers, "CustomerID", "CustomerAddress", product.CustomerID);
            return View(product);
        }

        // GET: Products/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.DeliveryDetails)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
