using BarberRatingSystem.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Http;
using BarberRatingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberRatingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Register()
        {
            ViewBag.Roles = _context.Roles.ToList();
            return View();

        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            Role role = new Role();
            role.Name = "User";
            user.RoleId = 2;
            Console.WriteLine("OKBRO");
            if (!ModelState.IsValid)
            {
                return View(user);

            }
            if (_context.Users.Any(u => u.UserName == user.UserName))
            {
                ModelState.AddModelError("UserName", "Username already taken.");
                return View(user);
            }
            Console.WriteLine("KK");

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            Console.WriteLine("1");
            Console.WriteLine("2");
            _context.Roles.Add(role);
            _context.SaveChanges();
            _context.Users.Add(user);
            Console.WriteLine("3");
            _context.SaveChanges();
            Console.WriteLine("4");
            return RedirectToAction("Login");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login1(string Username, string Password)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == Username);
            if (user == null && BCrypt.Net.BCrypt.Verify(Password, user.Password))
            {

                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetString("UserName", user.UserName);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password!";
            return View();

        }

        [HttpPost]
        public IActionResult Login(User loginUser)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.UserName == loginUser.UserName);

            if (user != null && BCrypt.Net.BCrypt.Verify(loginUser.Password, user.Password))
            {
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetInt32("RoleId", user.RoleId);
                if (user.RoleId == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

            ViewBag.Error = "Invalid username or password!";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        public IActionResult BarberList(int id)
        {

            return RedirectToAction("BarbersList", "Account");
        }

    }

}
