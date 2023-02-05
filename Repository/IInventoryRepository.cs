using Microsoft.AspNetCore.Identity;
using TrackAll_BackEnd.HelperModels;
using TrackAll_BackEnd.Models;

namespace TrackAll_BackEnd.Repository
{
    public interface IInventoryRepository
    {
        Task<IdentityResult> AddItem(ItemModel model);
        Task<IdentityResult> DeleteItem(ItemModel model);
        Task<List<ItemModel>> GetItem();
        Task<List<ItemModel>> GetItemByCategory(string category);
        Task<List<ItemModel>> GetItemByName(string name);
        Task<List<ItemModel>> GetItemByPrice(float price);
        Task<List<ItemModel>> GetItemByPriceAndStock(float price, float stock);
        Task<List<ItemModel>> GetItemByPriceAndStockRange(float price, float min, float max);
        Task<List<ItemModel>> GetItemByPriceRange(float min, float max);
        Task<List<ItemModel>> GetItemByPriceRangeAndStock(float min, float max, float stock);
        Task<List<ItemModel>> GetItemByPriceRangeAndStockRange(float min, float max, float minStock, float maxStock);
        Task<List<ItemModel>> GetItemByStock(float stock);
        Task<List<ItemModel>> GetItemByStockRange(float min, float max);
        Task<IdentityResult> UpdateItem(ItemModel model);
    }
}