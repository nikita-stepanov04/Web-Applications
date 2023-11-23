using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestaurantApi.Models.BindingTargets;
using RestaurantApi.Models.IdentityContext;
using System.Text.Json;

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
                .FindByEmailAsync(loginModel.Email!);

            if (user != null)
            {
                await _signInManager.SignOutAsync();
                if ((await _signInManager.PasswordSignInAsync(user,
                    loginModel.Password!, false, false)).Succeeded)
                {
                    string role = (await _userManager.GetRolesAsync(user))
                        .FirstOrDefault() ?? UserRoles.User;
                    return Ok(new { role = role });
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
                _userManager.FindByEmailAsync(userModel.Email!);
            if (user == null)
            {
                user = userModel.ToRestaurantUser();
                await Console.Out.WriteLineAsync(JsonSerializer.Serialize(user));
                if ((await _userManager.CreateAsync(
                    user, userModel.Password!)).Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                    return Ok();
                }
                else
                {
                    return BadRequest($"Can not create user with email:" +
                        $" {userModel.Email} and password: {userModel.Password}");
                }

            }
            else
            {
                return Conflict("Username is already taken");
            }
        }

        [HttpGet("get-role")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserRole()
        {
            var claimsPrincipal = this.User;
            if (claimsPrincipal != null)
            {
                RestaurantUser? user = await
                    _userManager.GetUserAsync(this.User) ?? new();
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
                return Ok(role);
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet("user-info")]
        public async Task<IActionResult> UserInfo()
        {
            RestaurantUser? user = await
                _userManager.GetUserAsync(this.User) ?? new();

            UserModel userData = user.ToUserModel();
            return Ok(userData);
        }

        [HttpGet("check-email-availability")]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(user == null);
        }

        [Authorize]
        [HttpPut("edit-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PatchUser(UserUpdateModel userModel)
        {
            RestaurantUser? user = await _userManager.GetUserAsync(this.User);
            if (user != null)
            { 
                user.UpdateFromUserModel(userModel); // update all fields except password

                if (userModel.NewPassword != null)
                {
                    var passwordChangeResult = await _userManager
                        .ChangePasswordAsync(user, userModel.CurrentPassword ?? "",
                            userModel.NewPassword);
                    if (!passwordChangeResult.Succeeded)
                    {
                        return BadRequest();
                    }
                }                
                
                await _userManager.UpdateAsync(user);
                return Ok();
            }
            return BadRequest();
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
