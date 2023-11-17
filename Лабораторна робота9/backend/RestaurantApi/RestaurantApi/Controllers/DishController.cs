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
            await Console.Out.WriteLineAsync($"type: {type}, page: {page}, perpage: {perPage}");
            PagingInfo pagInfo = new()
            {
                CurrentPage = page,
                ItemsPerPage = perPage
            };

            List<Dish> dishes = await _dishRepository
                .GetDishesByTypeAndPagingInfoAsync(type, pagInfo);

            List<DishBindingTarget> dishBindingTargets = new();
            IUrlHelper helper = _urlHelperFactory.GetUrlHelper(ControllerContext);
            foreach (var dish in dishes)
            {
                dishBindingTargets.Add(new DishBindingTarget()
                {
                    Id = dish.Id,
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

            pagInfo.TotalPages = await 
                _dishRepository.GetTotalPagesAsync(type, pagInfo);
            
            return Ok(new
            {
                dishes = dishBindingTargets,
                paginationInfo = pagInfo
            });
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
    }
}
