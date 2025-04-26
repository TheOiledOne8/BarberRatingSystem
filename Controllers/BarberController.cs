using BarberRatingSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace BarberRatingSystem.Controllers
{
    public class BarberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BarberController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchTerm)
        {
            var barbers = _context.Barbers.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {

                barbers = barbers.Where(b => b.Name.Contains(searchTerm));
            }

            return View(barbers.ToList());
        }

        public IActionResult Details(int id)
        {
            var barber = _context.Barbers.FirstOrDefault(b => b.Id == id);
            if (barber == null)
            {
                return NotFound();
            }
            var reviews = _context.Reviews
            .Where(r => r.BarberId == id)
            .OrderByDescending(r => r.DateTime)
            .ToList();

            ViewBag.Reviews = reviews;
            return View(barber);
        }
    }

}
