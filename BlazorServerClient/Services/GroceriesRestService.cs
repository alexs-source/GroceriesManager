using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using BlazorServerClient.Dtos;

namespace BlazorServerClient.Services
{
    public class GroceriesRestService : IGroceriesRestService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GroceriesRestService> _logger;
        private readonly string _baseUrl = "http://localhost:5000/api/groceries";
        public GroceriesRestService(HttpClient httpClient, ILogger<GroceriesRestService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Item>> GetItemsByStoreIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_baseUrl+$"/items/{id}");

            if(response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Get GetItemsByStoreIdAsync for id {id} succeeded!");
                return await response.Content.ReadFromJsonAsync<IReadOnlyList<Item>>();
            }
            else
            {
                _logger.LogError($"Get GetItemsByStoreIdAsync for id {id} failed!");
                return new List<Item>();
            }
        }

        public async Task<Store> GetStoreByStoreIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_baseUrl+$"/store/{id}");

            if(response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Get GetStoreByStoreIdAsync for id {id} succeeded!");
                return await response.Content.ReadFromJsonAsync<Store>();
            }
            else
            {
                _logger.LogError($"Get GetStoreByStoreIdAsync for id {id} failed!");
                return null;
            }
        }

        public async Task<IReadOnlyList<Store>> GetAllStoresAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl+"/stores");

            if(response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Get GetAllStoresAsync succeeded!");
                return await response.Content.ReadFromJsonAsync<IReadOnlyList<Store>>();
            }
            else
            {
                _logger.LogError($"Get GetAllStoresAsync failed!");
                return new List<Store>();
            }
        }

        public async Task<IReadOnlyList<StoreIdAndNameDto>> GetAllStoreIdsAndNamesAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl+"/stores/short");

            if(response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Get GetAllStoreIdsAndNamesAsync succeeded!");
                return await response.Content.ReadFromJsonAsync<StoreIdAndNameDto[]>();
            }
            else
            {
                _logger.LogError($"Get GetAllStoreIdsAndNamesAsync failed!");
                return new List<StoreIdAndNameDto>();
            }
        }
    }
}