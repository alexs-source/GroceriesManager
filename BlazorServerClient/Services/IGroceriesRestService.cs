using Core.Models;
using BlazorServerClient.Dtos;

namespace BlazorServerClient.Services
{
    public interface IGroceriesRestService
    {
        Task<IReadOnlyList<Item>> GetItemsByStoreIdAsync(int id);
        Task<Store> GetStoreByStoreIdAsync(int id);
        Task<IReadOnlyList<StoreIdAndNameDto>> GetAllStoreIdsAndNamesAsync();
        Task<IReadOnlyList<Store>> GetAllStoresAsync();
    }
}