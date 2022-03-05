using Core.Models;
using BlazorServerClient.Dtos;

namespace BlazorServerClient.Services
{
    public interface IGroceriesRestService
    {
        Task<IReadOnlyList<Item>> GetItemsByStoreIdAsync(int id);
        Task<(bool, int?)> AddItemAsync(Item item);
        Task<(bool,int?)> AddStoreAsync(Store store);
        Task<Store> GetStoreByStoreIdAsync(int id);
        Task<IReadOnlyList<StoreIdAndNameDto>> GetAllStoreIdsAndNamesAsync();
        Task<IReadOnlyList<Store>> GetAllStoresAsync();
        Task<bool> DeleteStoreAsync(int storeId);
        Task<bool> DeleteItemAsync(int itemId);
    }
}