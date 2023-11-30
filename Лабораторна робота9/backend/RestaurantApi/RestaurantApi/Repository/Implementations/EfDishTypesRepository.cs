using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Migrations;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Repository.Interfaces;
using System.Text.Json;

namespace RestaurantApi.Repository.Implementations
{
    public class EfDishTypesRepository : IDishTypesRepository
    {
        private DataContext _context;

        public EfDishTypesRepository(DataContext context) =>
            _context = context;

        public IQueryable<DishType> DishTypes =>
            _context.DishTypes;

        public async Task<bool> AddDishType(string name)
        {
            DishType type = new()
            {
                Name = name
            };
            await _context.DishTypes.AddAsync(type);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> PatchDishTypes(JsonPatchDocument<List<DishType>> patchDoc)
        {
            if (patchDoc != null)
            {
                List<DishType> dishTypes = await _context.DishTypes.ToListAsync();
                await Console.Out.WriteLineAsync($"\n\nBefore patch: {JsonSerializer.Serialize(dishTypes)}\n\n");

                patchDoc.ApplyTo(dishTypes);

                await Console.Out.WriteLineAsync($"\n\nAfter patch: {JsonSerializer.Serialize(dishTypes)}\n\n");
                
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteDishType(long id)
        {
            DishType? type = await _context.DishTypes.FindAsync(id);
            if (type != null)
            {
                _context.Remove(type);
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
