
using Contracts.Services.JWT;
using Database.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ForumApi.Extensions;
using Contracts.Services.Entity.Users;
using Models.DTOs.Users.Input;

namespace ForumApi.Controllers
{
    [ApiController]
    [Authorize]

    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IUserService userService;
        private readonly IJwtService jwtService;


        public UsersController(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUserService userService,
            IJwtService jwtService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.userService = userService;
            this.jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userId = User.GetId();

            if (userId != null)
            {
                return BadRequest("Already logged in!");
            }

            var user = await userManager.FindByNameAsync(userLoginDto.Username);

            if (user is null || !(await userManager.CheckPasswordAsync(user, userLoginDto.Password)))
            {
                return BadRequest("Incorrect username or password!");
            }

            var token = jwtService.GenerateJwtToken(user.Id, user.UserName);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Ensure this is only true in HTTPS environments
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return Ok(new { Id = user.Id, Username = user.UserName });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
        {
            var user = new ApplicationUser()
            {
                Email = userRegisterDto.Email,
                UserName = userRegisterDto.UserName,
            };

            var userNameIsTaken = await userService.IsUserNameTakenAsync(userRegisterDto.UserName);

            if (userNameIsTaken)
            {
                return BadRequest($"Username - {userRegisterDto.UserName} is already taken!");
            }

            //var operationResult = await customerService.CreateCustomerAsync(user, userRegisterDto.Customer);

            //if (!operationResult.IsSuccessful)
            //{
            //    return this.Error(operationResult);
            //}

            var createUserResult = await userManager.CreateAsync(user, userRegisterDto.Password);

            if (!createUserResult.Succeeded)
            {
                return BadRequest(createUserResult.Errors.Select(e => e.Description));
            }

            var token = jwtService.GenerateJwtToken(user.Id, user.UserName);

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Ensure this is only true in HTTPS environments
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(1)
            });

            return Ok(new { Id = user.Id, Username = user.UserName });
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            
            if (Request.Cookies["jwt"] != null)
            {
                Response.Cookies.Delete("jwt");
            }
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("all-usernames")]
        public async Task<IActionResult> GetAllUsernames()
        {
            var usernames = await userService.GetAllUsernamesAsync();

            return Ok(usernames);
        }

        [HttpPost]
        [Route("get-info")]
        public async Task<IActionResult> GetUserInfo()
        {
            var operationResult = await userService.GetUserByIdAsync(User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("search/{searchTerm}")]
        public async Task<IActionResult> SearchUsers(string searchTerm)
        {
            var foundUsers = await userService.SearchUsersAsync(searchTerm);

            return Ok(foundUsers);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-user-details/{username}")]
        public async Task<IActionResult> GetUserDetails(string username)
        {
            var operationResult = await userService.GetUserDetailsAsync(username);
            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [HttpGet]
        [Route("auth-test")]
        public IActionResult AuthTest()
        {
            return Ok("You are authenticated!");
        }
    }
}
