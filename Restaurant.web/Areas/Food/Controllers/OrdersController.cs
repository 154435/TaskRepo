using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.web.Data;
using Restaurant.web.Models;

namespace Restaurant.web.Areas.Food.Controllers
{
    [Area("Food")]
    public class OrdersController : Controller
    {
        private readonly PracticeApplicationDbContext _context;

        public OrdersController(PracticeApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Food/Orders
        public async Task<IActionResult> Index()
        {
            var practiceApplicationDbContext = _context.Orders.Include(o => o.Customers).Include(o => o.Foodmenu);
            return View(await practiceApplicationDbContext.ToListAsync());
        }

        // GET: Food/Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Foodmenu)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Food/Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", "MobileNUmber");
            ViewData["FoodId"] = new SelectList(_context.Foodmenu, "FoodId", "FoodName");
            return View();
        }

        // POST: Food/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("OrderId,OrderName,CustomerId,CustomerName,FoodId,Quantity")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                // Sanitize the data before consumption
                orders.OrderName = orders.OrderName.Trim();

                // Check for Duplicate CategoryName
                bool isDuplicateFound
                    = _context.Orders.Any(c => c.OrderName == orders.OrderName);
                if (isDuplicateFound)
                {
                    ModelState.AddModelError("orderName", "Table is Already booked please try another one..!");
                }
                else
                {
                    // Save the changes 
                    _context.Add(orders);
                    await _context.SaveChangesAsync();              // update the database
                    return RedirectToAction(nameof(Index));
                }
            }

            // Something must have gone wrong, so return the View with the model error(s).
            return View(orders);
        }


        // GET: Food/Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", orders.CustomerId);
            ViewData["FoodId"] = new SelectList(_context.Foodmenu, "FoodId", "FoodName", orders.FoodId);
            return View(orders);
        }

        // POST: Food/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderName,CustomerId,FoodId,CustomerName,Quantity")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerID", "CustomerName", orders.CustomerId);
            ViewData["FoodId"] = new SelectList(_context.Foodmenu, "FoodId", "FoodName", orders.FoodId);
            return View(orders);
        }

        // GET: Food/Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Foodmenu)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Food/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
