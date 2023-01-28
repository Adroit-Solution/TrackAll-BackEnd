using MarketPlace_Orders.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Newtonsoft.Json;
using NuGet.Packaging;
using System;
using System.Net.Http.Headers;
using Test_Series.Services;
using TrackAll_Backend.Database;
using TrackAll_BackEnd.Models;

namespace TrackAll_BackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly AppDbContext appDbContext;


        public OrderController(AppDbContext appDbContext, IUserServices userServices)
        {
            this.appDbContext = appDbContext;

        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetAllOrders(Guid restaurantId)
        {

            //List<OrderApi> orders = new List<OrderApi>();

            //var user = await appDbContext.Users.FindAsync(restaurantId);
            //if (user == null)
            //{
            //    return BadRequest("Restaurant not found");
            //}
            //HttpClient client = new HttpClient();

            //client.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            //client.DefaultRequestHeaders.Accept.Clear();
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //var availabeMarketPlace = await appDbContext.MarketPlaceMaps.Where(a => a.Restaurant == user).FirstOrDefaultAsync();

            //if (availabeMarketPlace == null)
            //{
            //    return BadRequest("Restaurant is not available in any marketplace");
            //}

            //if (availabeMarketPlace.Zomato != Guid.Empty)
            //{
            //    HttpResponseMessage zomato = await client.GetAsync($"GetZomatoOrders/{restaurantId}");
            //    if(zomato.IsSuccessStatusCode)
            //    {
            //        var zomatoOrders = await zomato.Content.ReadAsStringAsync();
            //        var order = JsonConvert.DeserializeObject<List<OrderApi>>(zomatoOrders);
            //        orders.AddRange(order);
            //    }
            //}

            //if (availabeMarketPlace.Swiggy != Guid.Empty)
            //{
            //    HttpResponseMessage swiggy = await client.GetAsync($"GetSwiggyOrders/{restaurantId}");
            //    if (swiggy.IsSuccessStatusCode)
            //    {
            //        var swiggyOrders = await swiggy.Content.ReadAsStringAsync();
            //        var order = JsonConvert.DeserializeObject<List<OrderApi>>(swiggyOrders);
            //        orders.AddRange(order);
            //    }
            //}

            //if (availabeMarketPlace.FoodPanda != Guid.Empty)
            //{
            //    HttpResponseMessage foodPanda = await client.GetAsync($"GetFoodPandaOrders/{restaurantId}");
            //    if (foodPanda.IsSuccessStatusCode)
            //    {
            //        var foodPandaOrders = await foodPanda.Content.ReadAsStringAsync();
            //        var order = JsonConvert.DeserializeObject<List<OrderApi>>(foodPandaOrders);
            //        orders.AddRange(order);
            //    }
            //}

            //if (availabeMarketPlace.UberEats != Guid.Empty)
            //{
            //    HttpResponseMessage uberEats = await client.GetAsync($"GetUberEatsOrders/{restaurantId}");
            //    if (uberEats.IsSuccessStatusCode)
            //    {
            //        var uberEatsOrders = await uberEats.Content.ReadAsStringAsync();
            //        var order = JsonConvert.DeserializeObject<List<OrderApi>>(uberEatsOrders);
            //        orders.AddRange(order);
            //    }
            //}
            //return Ok(orders.OrderByDescending(a => a.OrderTime));

            List<OrderApi> orderList = new List<OrderApi>();

            HttpClient httpClient = new HttpClient();
            //httpClient.BaseAddress = new Uri("http://localhost:5226/api/api/");
            httpClient.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync($"GetZomatoOrders/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var orders = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<OrderApi>>(orders);
                orderList.AddRange(order);
            }

            response = await httpClient.GetAsync($"GetSwiggyOrders/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var orders = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<OrderApi>>(orders);
                orderList.AddRange(order);
            }

            response = await httpClient.GetAsync($"GetFoodPandaOrders/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var orders = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<OrderApi>>(orders);
                orderList.AddRange(order);
            }

            response = await httpClient.GetAsync($"GetUberEatsOrders/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var orders = await response.Content.ReadAsStringAsync();
                var order = JsonConvert.DeserializeObject<List<OrderApi>>(orders);
                orderList.AddRange(order);
            }

            if (orderList is null)
            {
                return BadRequest("No orders found");
            }
            return Ok(orderList.OrderByDescending(a => a.OrderTime));
        }

        [HttpPut("{marketPlaceName}/{orderId}/{dateTime}")]
        public async Task<IActionResult> PutAcceptOrder(string marketPlaceName, Guid orderId, int dateTime)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = new HttpResponseMessage();
            if (marketPlaceName == "Zomato")
            {
                response = await httpClient.PutAsync($"PutZomatoOrderAccept/{orderId}/{dateTime}", null);
            }
            else if (marketPlaceName == "Swiggy")
            {
                 response = await httpClient.PutAsync($"PutSwiggyOrderAccept/{orderId}/{dateTime}", null);
            }
            else if (marketPlaceName == "Food Panda")
            {
                 response = await httpClient.PutAsync($"PutFoodPandaOrderAccept/{orderId}/{dateTime}", null);
            }
            else if (marketPlaceName == "Uber Eats")
            {
                 response = await httpClient.PutAsync($"PutUberEatsOrderAccept/{orderId}/{dateTime}", null);
            }
            
            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut("{marketPlaceName}/{orderId}")]
        public async Task<IActionResult> PutRejectOrder(string marketPlaceName, Guid orderId)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = new HttpResponseMessage();
            if (marketPlaceName == "Zomato")
            {
                response = await httpClient.PutAsync($"PutZomatoOrderCancel/{orderId}", null);
            }
            else if(marketPlaceName =="Swiggy")
            {
                response = await httpClient.PutAsync($"PutSwiggyOrderCancel/{orderId}", null);
            }
            else if(marketPlaceName =="Uber Eats")
            {
                response = await httpClient.PutAsync($"PutUberEatsOrderCancel/{orderId}", null);
            }
            else if(marketPlaceName == "Food Panda")
            {
                response = await httpClient.PutAsync($"PutFoodPandaOrderCancel/{orderId}", null);
            }


            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet("{restaurantId}")]
        public async Task<IActionResult> GetProduct(Guid restaurantId)
        {
            List<ProductModel> productsList = new();

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5226/api/api/");
            //httpClient.BaseAddress = new Uri("https://heyq.bsite.net/api/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpClient.GetAsync($"GetZomatoProduct/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<List<ProductModel>>(products);
                productsList.AddRange(product);
            }

            response = await httpClient.GetAsync($"GetSwiggyProduct/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<List<ProductModel>>(products);
                productsList.AddRange(product);
            }

            response = await httpClient.GetAsync($"GetFoodPandaProduct/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<List<ProductModel>>(products);
                productsList.AddRange(product);
            }

            response = await httpClient.GetAsync($"GetUberEatsProduct/{restaurantId}");
            if (response.IsSuccessStatusCode)
            {
                var products = await response.Content.ReadAsStringAsync();
                var product = JsonConvert.DeserializeObject<List<ProductModel>>(products);
                productsList.AddRange(product);
            }

            if (productsList is null)
            {
                return BadRequest("No orders found");
            }
            return Ok(productsList);

        }

        //PutZomatoProductStock
        [HttpPut("{name}/{restaurantId}/{productName}")]
        public async Task<IActionResult> PutProductStock(string name,Guid restaurantId,string productName)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:5226/api/api/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = new HttpResponseMessage();
            if (name == "Zomato")
            {
                response = await httpClient.PutAsync($"PutZomatoProductStock/{productName}/{restaurantId}", null);
            }
            else if (name == "Swiggy")
            {
                response = await httpClient.PutAsync($"PutSwiggyProductStock/{productName}/{restaurantId}", null);
            }
            else if (name == "Food Panda")
            {
                response = await httpClient.PutAsync($"PutFoodPandaProductStock/{productName}/{restaurantId}", null);
            }
            else if (name == "Uber Eats")
            {
                response = await httpClient.PutAsync($"PutUberEatsProductStock/{productName}/{restaurantId}", null);
            }

            if (response.IsSuccessStatusCode)
            {
                return Ok("Prodout Stock Updated Successfully");
            }

            return BadRequest();
        }
    }
}
