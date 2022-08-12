using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.web.Data;
using Restaurant.web.Model;

namespace Restaurant.web.Areas.Food.Controllers
{
    [Area("Food")]
    public class FoodmenusController : Controller
    {
        private readonly PracticeApplicationDbContext _context;

        public FoodmenusController(PracticeApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Food/Foodmenus
        public async Task<IActionResult> Index()
        {
            var practiceApplicationDbContext = _context.Foodmenu.Include(f => f.FoodCategory);
            return View(await practiceApplicationDbContext.ToListAsync());
        }

        // GET: Food/Foodmenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodmenu = await _context.Foodmenu
                .Include(f => f.FoodCategory)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (foodmenu == null)
            {
                return NotFound();
            }

            return View(foodmenu);
        }

        // GET: Food/Foodmenus/Create
        public IActionResult Create()
        {
            ViewData["FoodcategoryId"] = new SelectList(_context.FoodCategory, "FoodcategoryId", "FoodcategoryName");
            return View();
        }

        // POST: Food/Foodmenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FoodId,FoodName,FoodcategoryId,Confirmed,ImageUrl")] Foodmenu foodmenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(foodmenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodcategoryId"] = new SelectList(_context.FoodCategory, "FoodcategoryId", "FoodcategoryName", foodmenu.FoodcategoryId);
            return View(foodmenu);
        }

        // GET: Food/Foodmenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodmenu = await _context.Foodmenu.FindAsync(id);
            if (foodmenu == null)
            {
                return NotFound();
            }
            ViewData["FoodcategoryId"] = new SelectList(_context.FoodCategory, "FoodcategoryId", "FoodcategoryName", foodmenu.FoodcategoryId);
            return View(foodmenu);
        }

        // POST: Food/Foodmenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FoodId,FoodName,FoodcategoryId,Confirmed,ImageUrl")] Foodmenu foodmenu)
        {
            if (id != foodmenu.FoodId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(foodmenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodmenuExists(foodmenu.FoodId))
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
            ViewData["FoodcategoryId"] = new SelectList(_context.FoodCategory, "FoodcategoryId", "FoodcategoryName", foodmenu.FoodcategoryId);
            return View(foodmenu);
        }

        // GET: Food/Foodmenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodmenu = await _context.Foodmenu
                .Include(f => f.FoodCategory)
                .FirstOrDefaultAsync(m => m.FoodId == id);
            if (foodmenu == null)
            {
                return NotFound();
            }

            return View(foodmenu);
        }

        // POST: Food/Foodmenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodmenu = await _context.Foodmenu.FindAsync(id);
            _context.Foodmenu.Remove(foodmenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodmenuExists(int id)
        {
            return _context.Foodmenu.Any(e => e.FoodId == id);
        }
    }
}
