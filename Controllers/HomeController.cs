using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TrackAll_Backend.Database;
using TrackAll_Backend.HelperModels;
using TrackAll_BackEnd.Models;

namespace TrackAll_BackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityModel> userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityModel> userManager)
        {
            _logger = logger;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var user = new IdentityModel { RestId = Guid.NewGuid(), UserName = model.Email, Email = model.Email, Name = model.Name};
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return View("Index");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}