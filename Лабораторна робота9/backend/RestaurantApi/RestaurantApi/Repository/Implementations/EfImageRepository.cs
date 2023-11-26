using Microsoft.Extensions.Caching.Distributed;
using RestaurantApi.Models;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Repository.Interfaces;
using System.Text.Json;

namespace RestaurantApi.Repository.Implementations
{
    public class EfImageRepository : IImageRepository
    {
        private DataContext _context;
        private IDistributedCache _cache;

        public EfImageRepository(DataContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IQueryable<Image> Images =>
            _context.Images;

        public async Task<Image?> GetImageByIdAsync(long id)
        {
            string cacheKey = $"img-{id}";
            string? json = await _cache.GetStringAsync(cacheKey);

            if (json != null)
            {
                return JsonSerializer.Deserialize<Image>(json);
            }
            else
            {                
                Image? img = await _context.Images.FindAsync(id);
                if (img != null)
                {
                    await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(img),
                        new DistributedCacheEntryOptions
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                        });
                }
                return img;
            }
        }
    }
}
