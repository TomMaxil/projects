namespace ProjInventroy.Services.Inventroy
{
    public interface IInventroyService
    {
        Task HandleLowStockAsync(int productId);
    }
}
