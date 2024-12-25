using Data_DAL.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace StarSecurityServices.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class LoginController : Controller
    {
        private readonly ConnectDB _connectDB;
        public LoginController(ConnectDB connectDB)
        {
            _connectDB = connectDB;

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.err = "<div class='alert alert-danger'>Username or password cannot be blank.</div>";
                return View();
            }

            var md5pass = Utilitie.GetMD5HashData(password);

            var emp = _connectDB.Employees
                .Include(e => e.Role)
                .FirstOrDefault(x => x.Username == username && x.Password == md5pass);

            if (emp != null && emp.Role != null)
            {
                var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, emp.Username),
                    new Claim("Avata", emp.Avata),
                    new Claim(ClaimTypes.Role, emp.Role.Name)
                }, "CookieAuthentication");

                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.err = "<div class='alert alert-danger'>Incorrect login information or you do not have access.</div>";
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
