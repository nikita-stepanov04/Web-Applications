using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Models;
using RestaurantApi.Models.BindingTargets;
using RestaurantApi.Models.DishModels;
using RestaurantApi.Models.IdentityContext;
using RestaurantApi.Repository.Interfaces;
using System.Text.Json;

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
            long? type = null, string? substring = null, int page = 1, int perPage = 6)
        {
            PagingInfo pagInfo = new()
            {
                CurrentPage = page,
                ItemsPerPage = perPage
            };

            List<Dish> dishes = new();
            dishes = substring == null
                ? await _dishRepository.GetDishesByTypeAsync(type, pagInfo)
                : await _dishRepository.GetDishesBySubstringAsync(substring, pagInfo);

            var dishBindingTargets = dishes.Select(d => ToDishBindingTarget(d));

            pagInfo.TotalPages = await
                _dishRepository.GetTotalPagesAsync(type, substring, pagInfo);

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
            Dish? dish = await _dishRepository.GetDishByIdAsync(id);
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
                return File(image.BitMap!, image.ContentType!);
            }
            return NotFound($"Image with id: {id} was not found");
        }

        [HttpGet("types")]
        public IAsyncEnumerable<DishType> GetTypes() =>
            _dishTypesRepository.DishTypes.AsAsyncEnumerable();

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("{id:long}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDish(long id)
        {
            Dish? dish = await _dishRepository.GetDishByIdAsync(id);
            if (dish != null)
            {
                bool result = await _dishRepository.DeleteDishAsync(dish);
                if (result)
                {
                    return Ok();
                }
            }
            return NotFound($"Dish with id: {id} was not found");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("img/{id:long}")]
        public async Task<IActionResult> PatchDishImage(
            long id, [FromForm] IFormFile image)
        {
            bool result = await _dishRepository
                .PatchDishImageByIdAsync(id, image);
            return result
                ? Ok()
                : BadRequest();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("{id:long}")]
        public async Task<IActionResult> PatchDish(
            long id, JsonPatchDocument<Dish> patchDoc)
        {
            await Console.Out.WriteLineAsync(JsonSerializer.Serialize(patchDoc));

            bool result = await _dishRepository
                .PatchDishByIdAsync(id, patchDoc);
            return result
                ? Ok()
                : BadRequest();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("add")]
        public async Task<IActionResult> AddDish(
            [FromForm] DishFormData data)
        {
            IFormFile? image = data.Image;
            Dish dish = data.ToDish();
            DishType? type = await _dishTypesRepository.DishTypes
                .FirstOrDefaultAsync(t => t.Id == data.DishTypeId);
            dish.DishType = type;

            if (image != null)
            {
                using (var stream = new MemoryStream())
                {
                    await image.CopyToAsync(stream);

                    dish.Image = new()
                    {
                        BitMap = stream.ToArray(),
                        ContentType = image.ContentType
                    };
                }
            }
            return await _dishRepository.AddDishAsync(dish)
                ? Ok()
                : BadRequest();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpDelete("delete/dish-type/{id:long}")]
        public async Task<IActionResult> DeleteDishType(long id)
        {
            return await _dishTypesRepository.DeleteDishType(id)
                ? Ok()
                : NotFound($"Dish type with id: {id} was not found");
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPost("add/dish-type")]
        public async Task<IActionResult> AddDishType([FromBody] string name)
        {
            return await _dishTypesRepository.AddDishType(name)
                ? Ok()
                : BadRequest();
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpPatch("patch/dish-types")]
        public async Task<IActionResult> PatchDishTypes(
            JsonPatchDocument<List<DishType>> patchDoc)
        {
            return await _dishTypesRepository.PatchDishTypes(patchDoc)
                ? Ok()
                : BadRequest();
        }


        private DishBindingTarget ToDishBindingTarget(Dish dish)
        {
            IUrlHelper helper = _urlHelperFactory.GetUrlHelper(ControllerContext);
            DishBindingTarget target = new()
            {
                Id = dish.Id,
                Name = dish.Name,
                Price = dish.Price,
                Description = dish.Description,
                DishTypeId = dish.DishTypeId,
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
