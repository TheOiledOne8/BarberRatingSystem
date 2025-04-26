using BarberRatingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using BarberRatingSystem.Models;
using Microsoft.Identity.Client;

namespace BarberRatingSystem.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("RoleId") != 2)
            {
                return RedirectToAction("Login", "Account");
            }
            

                var userCount = _context.Users.Count();
            var barberCount = _context.Barbers.Count();
            var reviewCount = _context.Reviews.Count();

            ViewBag.UserCount = userCount;
            ViewBag.BarberCount = barberCount;
            ViewBag.ReviewCount = reviewCount;

            return View();
        }

        public IActionResult Users()
        {
            if (HttpContext.Session.GetInt32("RoleId") != 2)
            {
                return RedirectToAction("Login", "Account");
            }

            var users = _context.Users
            .Select(u => new
            {
                u.Id,
                u.UserName,
                u.FirstName,
                u.LastName,
            }).ToList();

            ViewBag.Users = users;

            return View();
        }


        public IActionResult CreateUser()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();
        }

        public IActionResult CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Users");
            }

            ViewBag.Roles = _context.Roles.ToList();
            return View(user);
        }

        public IActionResult EditUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();

            }
            ViewBag.Roles = _context.Roles.ToList();
            return ViewBag(user);

        }
        [HttpPost]
        public IActionResult EditUser(User updatedUser)
        {
            var user = _context.Users.Find(updatedUser.Id);
            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = updatedUser.FirstName;
            user.LastName = updatedUser.LastName;
            user.RoleId = updatedUser.RoleId;

            _context.SaveChanges();
            return RedirectToAction("Users");

        }
        public IActionResult DeleteUser(int id)
        {

            var user = _context.Users.Find(id);
            if (user == null)

            {
                return NotFound();
            }
            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Users");


        }

        public IActionResult Barbers()
        {
            if (HttpContext.Session.GetInt32("RoleId") != 2)
            {
                return RedirectToAction("Login", "Account");

            }
            var barbers = _context.Barbers.ToList();
            return View(barbers);
        }

        public IActionResult CreateBarber()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBarber(Barber barber, IFormFile Photo)
        {
            
            if (ModelState.IsValid)
            {
                if (Photo != null && Photo.Length > 0)
                {
                    var fileName = Guid.NewGuid() + System.IO.Path.GetExtension(Photo.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Photo.CopyTo(stream);
                    }

                    barber.PhotoPath = fileName;
                    ;
                }

                _context.Barbers.Add(barber);
                _context.SaveChanges();
                return RedirectToAction("Barbers");
            }
            else return View(barber);
        }
        [HttpGet]
        public IActionResult EditBarber(int id)
        {
            var barber = _context.Barbers.Find(id);
            if (barber == null)
            {
                return NotFound();
            }
            else
            {
                return View(barber);
            }
        }

        [HttpPost]
        public IActionResult EditBarber(Barber updated, IFormFile Photo)
        {
            var barber = _context.Barbers.Find(updated.Id);
            if (barber == null)
            {
                return NotFound();

            }
            barber.Name = updated.Name;
            barber.Description = updated.Description;

            if (Photo != null && Photo.Length > 0)
            {
                var fileName = Guid.NewGuid() + System.IO.Path.GetExtension(Photo.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    Photo.CopyTo(stream);
                }
                barber.PhotoPath = fileName;
            }
            _context.SaveChanges();
            return RedirectToAction("Barbers");
        }

        public ActionResult DeleteBarber(int id)
        {
            var barber = _context.Barbers.Find(id);
            if (barber != null)
            {
                _context.Barbers.Remove(barber);
                _context.SaveChanges();
                return RedirectToAction("Barbers");
            }

            else
            {
                return NotFound();
            }
        }
        
    }
}