
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) 
        : IBasketRepository
    {        

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
        {
            var cacheBasket = await cache.GetStringAsync(userName, cancellationToken);
            if (!string.IsNullOrEmpty(cacheBasket))
                return JsonSerializer.Deserialize<ShoppingCart>(cacheBasket)!;
            

            var dbBasket = await repository.GetBasket(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(dbBasket), cancellationToken);
            return dbBasket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
        {
            await repository.StoreBasket(basket, cancellationToken);

            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);

            return basket;
        }

        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
        {
           await repository.DeleteBasket(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);
            return true;
        }
    }
}
