using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models.BindingTargets;
using RestaurantApi.Models.IdentityContext;

namespace RestaurantApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AccountController : ControllerBase
    {
        private UserManager<RestaurantUser> _userManager;
        private SignInManager<RestaurantUser> _signInManager;

        public AccountController(
            UserManager<RestaurantUser> userManager,
            SignInManager<RestaurantUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            RestaurantUser? user = await _userManager
                .FindByNameAsync(loginModel.Username!);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                if ((await _signInManager.PasswordSignInAsync(user,
                    loginModel.Password!, false, false)).Succeeded)
                {                    
                    return Ok();
                }
                
            }
            return Unauthorized("Invalid username or password");
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(UserModel userModel)
        {
            RestaurantUser? user = await
                _userManager.FindByNameAsync(userModel.Name!);
            if (user == null)
            {
                user = userModel.ToRestaurantUser();
                if ((await _userManager.CreateAsync(
                    user, userModel.Password!)).Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest($"Can not create user with username:" +
                        $" {userModel.UserName} and password: {userModel.Password}");
                }
                
            }
            return Conflict("Username is already taken");
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> UserInfo()
        {
            RestaurantUser? user = await
                _userManager.GetUserAsync(this.User) ?? new();

            UserModel userData = new()
            {
                Name = user.Name,
                Surname = user.Surname,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                City = user.City,
                Street = user.Street,
                Flat = user.Flat
            };
            return Ok(userData);
        }

        [Authorize]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}
