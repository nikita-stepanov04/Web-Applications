using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.BindingTargets;
using RestaurantApi.Models.DataContext;
using RestaurantApi.Models.DishModels;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("api/dishes")]
    public class DishController : ControllerBase
    {
        private DataContext _context;
        private IUrlHelperFactory _urlHelperFactory;

        public DishController(DataContext context, IUrlHelperFactory urlHelperFactory)
        {
            _context = context;
            _urlHelperFactory = urlHelperFactory;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDishes()
        {
            List<Dish> dishes = await _context.Dishes.ToListAsync();
            if (dishes.Count() > 0)
            {
                List<DishBindingTarget> responseList = new();
                IUrlHelper helper = _urlHelperFactory.GetUrlHelper(ControllerContext);
                foreach (var dish in dishes)
                {
                    responseList.Add(new DishBindingTarget()
                    {
                        Name = dish.Name,
                        Description = dish.Description,
                        Price = dish.Price,
                        ImageUrl = helper.Action(
                            controller: "Dish",
                            action: "GetImage",
                            values: new { id = dish.ImageId },
                            protocol: Request.Scheme)
                    });
                }
                return Ok(responseList);
            }
            else
            {
                return NotFound("No dishes in the database");
            }
        }

        [HttpGet("image/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImage(long id)
        {
            Image? image = await _context.Images.FindAsync(id);
            if (image != null)
            {
                return File(image.BitMap!, image.ContentType!);
            }
            return NotFound($"Image with id: {id} was not found");
        }
    }
}
