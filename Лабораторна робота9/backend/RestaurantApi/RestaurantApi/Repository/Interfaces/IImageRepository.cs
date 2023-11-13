using RestaurantApi.Models;

namespace RestaurantApi.Repository.Interfaces
{
    public interface IImageRepository
    {
        IQueryable<Image> Images { get; }

        Task<Image?> GetImageByIdAsync(long id);
    }
}
