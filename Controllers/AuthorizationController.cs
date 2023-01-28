using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TrackAll_Backend.Database;
using TrackAll_Backend.HelperModels;
using TrackAll_BackEnd.HelperModels;
using TrackAll_BackEnd.Models;

namespace TrackAll_Backend.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly SignInManager<IdentityModel> signInManager;
        private readonly UserManager<IdentityModel> userManager;
        private readonly AppDbContext context;
        static HttpClient client = new HttpClient();

        public AuthorizationController(SignInManager<IdentityModel> signInManager, UserManager<IdentityModel> userManager, AppDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
        }

        [HttpPost("{data}")]
        [EnableCors("AllowAll")]
        public async Task<ActionResult> SignUp([FromForm]SignUpModel model)
        {
            
            var user = new IdentityModel { RestId = Guid.NewGuid(), UserName = model.Email, Email = model.Email, Name = model.Name,RestaurantName = model.RestaurantName };
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return Ok("User created successfully!");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInModel signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.IsPersistent, false);
            if (result.Succeeded)
            {
                return Ok("Signed In");
            }
            return BadRequest(result);
        }

        [HttpPost]
        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return Ok("Signed Out");
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUser()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest("User not found");
        }

        [HttpGet]
        public async Task<IActionResult> IsSignedIn()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost]
        public async Task<IActionResult> MarketPlaces([FromBody] MarketPlaceConnection marketPlace)
        {
            if (marketPlace is null)
                return BadRequest("Invalid Object");

            MarketPlaceMap marketPlaceMap = new MarketPlaceMap();

            var user = await userManager.FindByEmailAsync(marketPlace.RestaurantEmail);
            if (user is null)
            {
                return BadRequest("User Not Found");
            }
            else
            {
                marketPlaceMap.Restaurant = user;
                marketPlaceMap.Id = Guid.Parse(user.Id);
            }
            int count = 0;
            HttpResponseMessage response = await client.GetAsync($"https://heyq.bsite.net/api/api/GetZomatoAuthenticate/{marketPlace.Zomato}");
            if (response.IsSuccessStatusCode)
            {
                count++;
                marketPlaceMap.Zomato = (marketPlace.Zomato);
            }

            response = await client.GetAsync($"https://heyq.bsite.net/api/api/GetSwiggyAuthenticate/{marketPlace.Swiggy}");
            if (response.IsSuccessStatusCode)
            {
                count++;
                marketPlaceMap.Swiggy = (marketPlace.Swiggy);
            }

            response = await client.GetAsync($"https://heyq.bsite.net/api/api/GetFoodAuthenticate/{marketPlace.FoodPanda}");
            if (response.IsSuccessStatusCode)
            {
                count++;
                marketPlaceMap.FoodPanda = (marketPlace.FoodPanda);
            }

            response = await client.GetAsync($"https://heyq.bsite.net/api/api/GetUberAuthenticate/{marketPlace.UberEats}");
            if (response.IsSuccessStatusCode)
            {
                count++;
                marketPlaceMap.UberEats = (marketPlace.UberEats);
            }

            if (count == 0)
                return BadRequest("Invalid Credentials or Credentials Not Provided");

            await context.MarketPlaceMaps.AddAsync(marketPlaceMap);
            var changes = await context.SaveChangesAsync();
            if (changes > 0)
            {
                return Ok("Market Places ConnectionsEstablished");
            }
            return BadRequest("Market Places Connections Failed");
        }

    }
}
