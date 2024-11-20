
using Contracts.Services.JWT;
using Database.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ForumApi.Extensions;
using Models.DTOs.Users;
using Contracts.Services.Entity.Users;

namespace ForumApi.Controllers
{
    [ApiController]
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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            var userId = User.GetId();

            if (userId != null)
            {
                return BadRequest("Already logged in!");
            }

            var user = await userManager.FindByNameAsync(userLoginDto.UserName);

            if (user == null || !(await userManager.CheckPasswordAsync(user, userLoginDto.Password)))
            {
                return BadRequest("Incorrect username or password!");
            }

            var token = jwtService.GenerateJwtToken(user.Id, user.UserName);

            return Ok(token);
        }

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
            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        [Route("auth-test")]
        public IActionResult AuthTest()
        {
            return Ok("You are authenticated!");
        }
    }
}
