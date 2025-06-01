using Application.Contracts;
using System.Net.Http.Json;

namespace Infrastructure.Persistence.Services;

public class InventoryApiClient (HttpClient httpClient)
    : IInventoryApiClient
{
   
    public async Task<bool> CheckProductAvailabilityAsync(Guid productId, int quantity)
    {
      
        var response = await httpClient.GetAsync($"/api/inventoryitems/check-availability/{productId}/{quantity}");

        if (!response.IsSuccessStatusCode)
        {
          
            return false;
        }

        var content = await response.Content.ReadFromJsonAsync<ResponseDto>();

        return content?.IsAvailable ?? false;
    }

    private class ResponseDto
    {
        public bool IsAvailable { get; set; }
    }
}
