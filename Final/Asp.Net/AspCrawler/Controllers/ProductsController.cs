using AspCrawler.Models;
using AspCrawler.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AspCrawler.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Context _context;

        public ProductsController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var products = await (from p in _context.Products
                                  join c in _context.Categories on p.CatId equals c.Id
                                  select new
                                  {
                                      p.Id,
                                      p.Name,
                                      p.Price,
                                      p.DateTime,
                                      Category = c.Title 
                                  }).ToListAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            var categories = _context.Categories.Select(c => new { c.Id, c.Title }).ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Country = model.Country,
                    unit_p = model.unit_p,
                    unit_s = model.unit_s,
                    CatId = model.CategoryId 
                };

                _context.Add(product);
                await _context.SaveChangesAsync(); 

                return RedirectToAction(nameof(Index));
            }

            var categories = _context.Categories.Select(c => new { c.Id, c.Title }).ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Title");
            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Products
                                        .Where(p => p.Id == id)
                                        .Select(p => new ProductViewModel
                                        {
                                            Name = p.Name,
                                            Price = p.Price,
                                            Country = p.Country,
                                            unit_p=p.unit_p,
                                            unit_s=p.unit_s,
                                            CategoryId = (int)p.CatId
                                        })
                                        .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }
            var categories = _context.Categories.Select(c => new { c.Id, c.Title }).ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Title", product.CategoryId);

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (id == 0 || model == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(id);

                if (product == null)
                {
                    return NotFound();
                }

                product.Name = model.Name;
                product.Price = model.Price;
                product.CatId = model.CategoryId;

                _context.Update(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var categories = _context.Categories.Select(c => new { c.Id, c.Title }).ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Title", model.CategoryId);
            return View(model);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await (from p in _context.Products
                                 join c in _context.Categories on p.CatId equals c.Id
                                 where p.Id == id
                                 select new
                                 {
                                     p.Id,
                                     p.Name,
                                     p.Price,
                                     CategoryTitle = c.Title
                                 }).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
