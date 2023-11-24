using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.BindingTargets;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Repository.Interfaces;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("api/dishes")]
    public class DishController : ControllerBase
    {
        private IDishRepository _dishRepository;
        private IDishTypesRepository _dishTypesRepository;
        private IImageRepository _imageRepository;
        private IUrlHelperFactory _urlHelperFactory;

        public DishController(IDishRepository dishRepository,
            IDishTypesRepository dishTypesRepository,
            IImageRepository imageRepository,
            IUrlHelperFactory urlHelperFactory)
        {
            _dishRepository = dishRepository;
            _dishTypesRepository = dishTypesRepository;
            _imageRepository = imageRepository;
            _urlHelperFactory = urlHelperFactory;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDishes(
            long? type = null, int page = 1, int perPage = 6)
        {
            PagingInfo pagInfo = new()
            {
                CurrentPage = page,
                ItemsPerPage = perPage
            };

            List<Dish> dishes = await _dishRepository
                .GetDishesByTypeAndPagingInfoAsync(type, pagInfo);

            var dishBindingTargets = dishes.Select(d => ToDishBindingTarget(d));
            
            pagInfo.TotalPages = await
                _dishRepository.GetTotalPagesAsync(type, pagInfo);

            return Ok(new
            {
                dishes = dishBindingTargets,
                paginationInfo = pagInfo
            });
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDishById(long id)
        {
            Dish? dish = await _dishRepository.GetDishById(id);
            return dish != null
                ? Ok(ToDishBindingTarget(dish))
                : NotFound($"Dish with id: {id} was not found");
        }

        [HttpGet("description-for/{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDishDescriptionById(long id)
        {
            string? dishDescripiton = await _dishRepository.GetDishDescriptionById(id);
            return dishDescripiton != null
                ? Ok(dishDescripiton)
                : NotFound($"Dish with id: {id} was not found");
        }        

        [HttpGet("image/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetImage(long id)
        {
            Image? image = await _imageRepository.GetImageByIdAsync(id);
            if (image != null)
            {
                Response.Headers["Cache-Control"] = "public, max-age=120";
                return File(image.BitMap!, image.ContentType!);
            }
            return NotFound($"Image with id: {id} was not found");
        }

        [HttpGet("types")]
        public IAsyncEnumerable<DishType> GetTypes() =>
            _dishTypesRepository.DishTypes.AsAsyncEnumerable();

        private DishBindingTarget ToDishBindingTarget(Dish dish)
        {
            IUrlHelper helper = _urlHelperFactory.GetUrlHelper(ControllerContext);
            DishBindingTarget target = new()
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                Description = dish.Description,
                ImageUrl = helper.Action(
                        controller: "Dish",
                        action: "GetImage",
                        values: new { id = dish.ImageId },
                        protocol: Request.Scheme)
            };
            return target;
        }
    }
}
