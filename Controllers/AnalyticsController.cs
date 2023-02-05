using MarketPlace_Orders.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Test_Series.Services;
using TrackAll_Backend.Database;

namespace TrackAll_BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class AnalyticsController : ControllerBase
    {
        private readonly IUserServices userServices;
        private readonly UserManager<IdentityModel> userManager;

        public AnalyticsController(IUserServices userServices,UserManager<IdentityModel> userManager)
        {
            this.userServices = userServices;
            this.userManager = userManager;
        }
        
        #region Analytics
        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetNumberAnalytics(Guid restaurantId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"Orders/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<Order>>(data);

                var allData = new { TotalSales = orders.Sum(x => x.Price), TotalOrders = orders.Count(), CancelledOrder = orders.Count(x => x.Status == "Cancelled"), NewCustomer = orders.Count(x => x.OrderNo == 1) };

                var TodayAnalysis = orders.Where(x => x.OrderTime.Date == DateTime.Now.Date).ToList();
                var TodayData = new { TotalSales = TodayAnalysis.Sum(x => x.Price), TotalOrders = TodayAnalysis.Count(), CancelledOrder = TodayAnalysis.Count(x => x.Status == "Cancelled"), NewCustomer = TodayAnalysis.Count(x => x.OrderNo == 1) };

                var WeeklyAnalysis = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date).ToList();
                var WeeklyData = new { TotalSales = WeeklyAnalysis.Sum(x => x.Price), TotalOrders = WeeklyAnalysis.Count(), CancelledOrder = WeeklyAnalysis.Count(x => x.Status == "Cancelled"), NewCustomer = WeeklyAnalysis.Count(x => x.OrderNo == 1) };

                var MonthlyAnalysis = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-DateTime.Now.Day).Date).ToList();
                var MonthlyData = new { TotalSales = MonthlyAnalysis.Sum(x => x.Price), TotalOrders = MonthlyAnalysis.Count(), CancelledOrder = MonthlyAnalysis.Count(x => x.Status == "Cancelled"), NewCustomer = MonthlyAnalysis.Count(x => x.OrderNo == 1) };

                var YearlyAnalysis = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-DateTime.Now.DayOfYear).Date).ToList();
                var YearlyData = new { TotalSales = YearlyAnalysis.Sum(x => x.Price), TotalOrders = YearlyAnalysis.Count(), CancelledOrder = YearlyAnalysis.Count(x => x.Status == "Cancelled"), NewCustomer = YearlyAnalysis.Count(x => x.OrderNo == 1) };

                var list = new { allData, TodayData, WeeklyData, MonthlyData, YearlyData };

                return Ok(list);
            }

            return BadRequest();
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetTotalSales(Guid restaurantId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"Orders/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<Order>>(data);

                var WeekDaysSales = orders.Where(x => x.OrderTime >= DateTime.Now.AddDays(-((int)DateTime.Now.DayOfWeek)).Date).GroupBy(x => x.OrderTime.Date).Select(x => new { Date = x.Key.DayOfWeek.ToString().Substring(0,3), TotalSales = x.Sum(y => y.Price) }).ToList();

                WeekDaysSales.Reverse();

                var yearlySaleOverMonth = orders.Where(x => x.OrderTime >= DateTime.Now.AddDays(-DateTime.Now.DayOfYear).Date).GroupBy(x => x.OrderTime.ToString("MMM")).Select(x => new { Month = x.Key, TotalSales = x.Sum(y => y.Price) }).ToList();

                yearlySaleOverMonth.Reverse();

                var list = new { WeekDaysSales, yearlySaleOverMonth };

                return Ok(list);
            }

            return BadRequest();
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetTotalSalesByMarket(Guid restaurantId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"Orders/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<Order>>(data);

                var SalesByMarketPlace = orders.GroupBy(x => x.MarketPlaceName).Select(x => new { MarketPlaceName = x.Key, TotalSales = x.Sum(y => y.Price) }).ToList();

                var todaySalesByMarktePlace = orders.Where(x => x.OrderTime.Date == DateTime.Now.Date).GroupBy(x => x.MarketPlaceName).Select(x => new { MarketPlaceName = x.Key, TotalSales = x.Sum(y => y.Price) }).ToList();

                var weeklySalesByMarktePlace = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date).GroupBy(x => x.MarketPlaceName).Select(x => new { MarketPlaceName = x.Key, TotalSales = x.Sum(y => y.Price) }).ToList();

                var monthlySalesByMarktePlace = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-DateTime.Now.Day).Date).GroupBy(x => x.MarketPlaceName).Select(x => new { MarketPlaceName = x.Key, TotalSales = x.Sum(y => y.Price) }).ToList();

                var yearlySalesByMarktePlace = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-DateTime.Now.DayOfYear).Date).GroupBy(x => x.MarketPlaceName).Select(x => new { MarketPlaceName = x.Key, TotalSales = x.Sum(y => y.Price) }).ToList();

                var list = new { SalesByMarketPlace, todaySalesByMarktePlace, weeklySalesByMarktePlace, monthlySalesByMarktePlace, yearlySalesByMarktePlace };
                return Ok(list);
            }
            return BadRequest();
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetTopProduct(Guid restaurantId)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync($"Orders/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<Order>>(data);
                int totalSale = orders.Count();
                var TopProduct = orders.GroupBy(x => x.ItemName).Select(x => new { ProductName = x.Key, Percentage = (x.Count() * 100) / totalSale }).OrderByDescending(x => x.Percentage).Take(5).ToList();

                totalSale = orders.Count(x => x.OrderTime.Date == DateTime.Now.Date);
                var todayTopProduct = orders.Where(x => x.OrderTime.Date == DateTime.Now.Date).GroupBy(x => x.ItemName).Select(x => new { ProductName = x.Key, Percentage = ((x.Count() * 100) / totalSale) }).OrderByDescending(x => x.Percentage).Take(5).ToList();

                totalSale = orders.Count(x => x.OrderTime.Date >= DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date);
                var weeklyTopProduct = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek).Date).GroupBy(x => x.ItemName).Select(x => new { ProductName = x.Key, Percentage = ((x.Count() * 100) / totalSale) }).OrderByDescending(x => x.Percentage).Take(5).ToList();

                totalSale = orders.Count(x => x.OrderTime.Date >= DateTime.Now.AddDays(-DateTime.Now.Day).Date);
                var monthlyTopProduct = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-DateTime.Now.Day).Date).GroupBy(x => x.ItemName).Select(x => new { ProductName = x.Key, Percentage = (x.Count() * 100) / totalSale }).OrderByDescending(x => x.Percentage).Take(5).ToList();

                totalSale = orders.Count(x => x.OrderTime.Date >= DateTime.Now.AddDays(-DateTime.Now.DayOfYear).Date);
                var yearlyTopProduct = orders.Where(x => x.OrderTime.Date >= DateTime.Now.AddDays(-DateTime.Now.DayOfYear).Date).GroupBy(x => x.ItemName).Select(x => new { ProductName = x.Key, Percentage = (x.Count() * 100) / totalSale }).OrderByDescending(x => x.Percentage).Take(5).ToList();
                var list = new { TopProduct, todayTopProduct, weeklyTopProduct, monthlyTopProduct, yearlyTopProduct };

                return Ok(list);
            }
            return BadRequest();
        }
        #endregion
    }
}
