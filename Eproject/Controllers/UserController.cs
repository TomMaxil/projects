using Eproject.Helpers;
using Eproject.Models;
using Microsoft.AspNetCore.Mvc;

namespace Eproject.Controllers
{
    public class UserController : Controller
    {
        private readonly eCommerceContext _context;

        public UserController(eCommerceContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
           
            var hash = Passwordhash.Hash(user.PasswordHash);
            user.PasswordHash = hash;

            user.UserId = Guid.NewGuid();

            _context.users.Add(user);
            _context.SaveChanges();


            if (user.Role == "Customer")
            {
                var cust = new Customer
                {
                    id = Guid.NewGuid(),
                    UserId = user.UserId,
                    FirstName = user.FullName.Split(' ')[0],
                    LastName = (user.FullName.Contains(" ") ? user.FullName.Substring(user.FullName.IndexOf(" ") + 1) : ""),
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    Street = "",
                    City = "",
                    State = "",
                    Country = "",
                    Mobile = ""
                };

                _context.tbl_customer.Add(cust);
                _context.SaveChanges();
            }

            TempData["success"] = "Registered Successfully. Please Login.";
            return RedirectToAction("Login", "User");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var hash = Passwordhash.Hash(user.PasswordHash);

            var check = _context.users.FirstOrDefault(u => u.Email == user.Email && u.PasswordHash == hash);

           
            if (check == null)
            {
                TempData["error"] = "Invalid Email/Password";
                return View();
            }
            else
            {
                HttpContext.Session.SetString("Userid", check.UserId.ToString());
                HttpContext.Session.SetString("Username", check.FullName);
                HttpContext.Session.SetString("Useremail", check.Email);
                HttpContext.Session.SetString("Userphone", check.PhoneNumber);
                HttpContext.Session.SetString("Userpassword", check.PasswordHash);
                HttpContext.Session.SetString("Userrole", check.Role);

                if (check.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else if (check.Role == "Customer")   
                {
                    return RedirectToAction("Dashboard", "Customer");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login","User");
        }
    }
}
