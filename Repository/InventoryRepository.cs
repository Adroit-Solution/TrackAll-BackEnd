using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Test_Series.Services;
using TrackAll_Backend.Database;
using TrackAll_BackEnd.HelperModels;
using TrackAll_BackEnd.Models;

namespace TrackAll_BackEnd.Repository
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext appDbContext;
        private readonly IUserServices userServices;
        private readonly UserManager<IdentityModel> userManager;

        public InventoryRepository(AppDbContext appDbContext, IUserServices userServices, UserManager<IdentityModel> userManager)
        {
            this.appDbContext = appDbContext;
            this.userServices = userServices;
            this.userManager = userManager;
        }

        public async Task<IdentityResult> AddItem(ItemModel model)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            ProductModel item = new ProductModel()
            {
                Id = Guid.NewGuid(),
                Restaurant = restaurant,
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock,
                Category = model.Category
            };

            await appDbContext.Inventory.AddAsync(item);
            var result = await appDbContext.SaveChangesAsync();
            if (result > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed(new IdentityError() { Code = "Not Added", Description = "Item not Added " });
        }

        public async Task<List<ItemModel>> GetItem()
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<IdentityResult> UpdateItem(ItemModel model)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var item = await appDbContext.Inventory.FirstOrDefaultAsync(a => a.Restaurant == restaurant && a.Name == model.Name);
            if (item is not null)
            {
                item.Stock = model.Stock;
                appDbContext.Inventory.Update(item);
                var result = await appDbContext.SaveChangesAsync();
                if (result > 0)
                    return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError() { Code = "Not Updated", Description = "Item not Updated " });
        }

        public async Task<IdentityResult> DeleteItem(ItemModel model)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var item = await appDbContext.Inventory.FirstOrDefaultAsync(a => a.Restaurant == restaurant && a.Name == model.Name);
            if (item is not null)
            {
                appDbContext.Inventory.Remove(item);
                var result = await appDbContext.SaveChangesAsync();
                if (result > 0)
                    return IdentityResult.Success;
            }
            return IdentityResult.Failed(new IdentityError() { Code = "Not Deleted", Description = "Item not Deleted " });
        }

        public async Task<List<ItemModel>> GetItemByCategory(string category)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Category == category).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByName(string name)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Name == name).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByPrice(float price)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Price == price).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByStock(float stock)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Stock == stock).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByPriceRange(float min, float max)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Price >= min && a.Price <= max).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByStockRange(float min, float max)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Stock >= min && a.Stock <= max).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByPriceAndStock(float price, float stock)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Price == price && a.Stock == stock).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByPriceAndStockRange(float price, float min, float max)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Price == price && a.Stock >= min && a.Stock <= max).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByPriceRangeAndStock(float min, float max, float stock)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Price >= min && a.Price <= max && a.Stock == stock).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }

        public async Task<List<ItemModel>> GetItemByPriceRangeAndStockRange(float min, float max, float minStock, float maxStock)
        {
            var restaurant = await userManager.FindByIdAsync(userServices.GetId());
            var itemList = await appDbContext.Inventory.Where(a => a.Restaurant == restaurant && a.Price >= min && a.Price <= max && a.Stock >= minStock && a.Stock <= maxStock).Select(a => new { a.Name, a.Stock, a.Category }).ToListAsync();

            var toReturn = new List<ItemModel>();

            foreach (var item in itemList)
            {
                ItemModel itemModel = new()
                {
                    Stock = item.Stock,
                    Name = item.Name,
                    Category = item.Category
                };

                toReturn.Add(itemModel);
            }
            return toReturn;
        }


    }
}
