using BarberRatingSystem.Data;
using BarberRatingSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace BarberRatingSystem.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Add(int BarberId, string Text)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var review = new Review()
            {
                UserId = userId.Value,
                BarberId = BarberId,
                Text = Text,
                DateTime = DateTime.Now
            };

            _context.Reviews.Add(review);
            _context.SaveChanges();

            return RedirectToAction("Details", "Barber", new { id = BarberId });
        }

        public IActionResult MyReviews()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var reviews = _context.Reviews
            .Where(r => r.UserId == userId)
            .Select(r => new
            {
                r.Id,
                r.Text,
                r.DateTime,
                BarberName = r.Barber.Name

            })
            .ToList();
            ViewBag.MyReviews = reviews;
            return View();
        }

        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }
        [HttpPost]
        public IActionResult Edit(Review updatedReview)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var review = _context.Reviews.FirstOrDefault(r => r.Id == updatedReview.Id && r.UserId == userId);
            if (review == null)
            {
                return NotFound();
            }
            review.Text = updatedReview.Text;
            _context.SaveChanges();

            return RedirectToAction("MyReviews");
        }

        public IActionResult Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var review = _context.Reviews.FirstOrDefault(r => r.Id == id && r.UserId == userId);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }

            return RedirectToAction("MyReviews");
        }
    }
}
