using Microsoft.AspNetCore.SignalR;
using ProjInventroy.Hubs;

namespace ProjInventroy.Services.Inventroy
{
    public class InventoryService
    {
        private readonly IHubContext<InventoryHub> _hub;

        public InventoryService(IHubContext<InventoryHub> hub)
        {
            this._hub = hub;
        }

        public async Task NotifyLowStockAsync(string productName, int stockQty)
        {
            await _hub.Clients.All.SendAsync(
                "LowStockAlert",
                productName,
                stockQty
            );
        }
    }
}
