using RestaurantApi.Models;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Repository.Implementations
{
    public class EfImageRepository : IImageRepository
    {
        private DataContext _context;

        public EfImageRepository(DataContext context) => 
            _context = context;

        public IQueryable<Image> Images =>
            _context.Images;

        public async Task<Image?> GetImageByIdAsync(long id) =>
            await _context.Images.FindAsync(id);
    }
}
