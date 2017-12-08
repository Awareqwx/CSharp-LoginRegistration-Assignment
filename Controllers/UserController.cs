using LoginRegistration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegistration.Controllers
{
    public class UserController : Controller
    {

        private readonly DbConnector _dbConnector;
        
        public UserController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult Register(User user)
        {
            if(ModelState.IsValid)
            {
                if(_dbConnector.Query($"SELECT * FROM users WHERE email='{user.email}'").Count != 0)
                {
                    ModelState.AddModelError("email", "Email address already in use");
                    ViewBag.errors = ModelState.Values;
                    return View("Index", user);
                }
                _dbConnector.Execute($"INSERT INTO users (email, firstName, lastName, password, created_at, updated_at) VALUES ('{user.email}', '{user.firstName}', '{user.lastName}', '{user.password}', NOW(), NOW());");
                return RedirectToAction("Success");
            }
            ViewBag.errors = ModelState.Values;
            return View("Index", user);
        }

        [HttpPost]
        [Route("/login")]
        public IActionResult Login(string email, string password)
        {
            if(_dbConnector.Query($"SELECT * FROM users WHERE email='{email}' AND password='{password}'").Count != 0)
            {
                return RedirectToAction("Success");
            }
            ViewBag.loginError = "Invalid username or password";
            return View("Index");
        }

        [HttpGet]
        [Route("/success")]
        public IActionResult Success()
        {
            return View("Success");
        }
    }
}