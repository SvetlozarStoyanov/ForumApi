using Contracts.Services.Managers;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Posts.Output;
using Models.DTOs.Subforums.Input;
using Services.Managers;

namespace ForumApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubforumsController : ControllerBase
    {
        private readonly ISubforumManager subforumManager;

        public SubforumsController(ISubforumManager subforumManager)
        {
            this.subforumManager = subforumManager;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("get-guest-user-subforums")]
        public async Task<IActionResult> GetGuestUserSubforums(SubforumsQueryDto subforumsQueryDto)
        {
            var subforums = await subforumManager.GetSubforumsForGuestUserAsync(subforumsQueryDto);

            return Ok(subforums);
        }

        [HttpPost]
        [Route("get-user-unjoined-subforums")]
        public async Task<IActionResult> GetUserUnjoinedSubforums(SubforumsQueryDto subforumsQueryDto)
        {
            var operationResult = await subforumManager.GetUserUnjoinedSubforumsAsync(User.GetId(), subforumsQueryDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [HttpPost]
        [Route("get-user-joined-subforums")]
        public async Task<IActionResult> GetUserJoinedSubforums(SubforumsQueryDto subforumsQueryDto)
        {
            var operationResult = await subforumManager.GetUserJoinedSubforumsAsync(User.GetId(), subforumsQueryDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }


        [HttpGet]
        [Route("all-names")]
        public async Task<IActionResult> GetAllNames()
        {
            var names = await subforumManager.GetAllSubforumNamesAsync();

            return Ok(names);
        }

        [HttpGet]
        [Route("all-for-dropdown")]
        public async Task<IActionResult> GetAllForDropdown()
        {
            var subforums = await subforumManager.GetSubforumsForDropdownAsync();

            return Ok(subforums);
        }

        [AllowAnonymous]
        [HttpGet()]
        [Route("search/{searchTerm}")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var foundSubforums = await subforumManager.SearchSubforumsAsync(searchTerm);

            return Ok(foundSubforums);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{name}")]
        public async Task<IActionResult> GetSubforumByNameAsync([FromRoute] string name)
        {
            var operationResult = await subforumManager.GetSubforumByNameAsync(name, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSubforum(SubforumCreateDto subforumCreateDto)
        {
            var operationResult = await subforumManager.CreateSubforumAsync(User.GetId(), subforumCreateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(new { Name = operationResult.Data });
        }

        [HttpPost]
        [Route("join/{id}")]
        public async Task<IActionResult> JoinSubforum([FromRoute] long id)
        {
            var operationResult = await subforumManager.JoinSubforumAsync(id, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPost]
        [Route("leave/{id}")]
        public async Task<IActionResult> LeaveSubforum([FromRoute] long id)
        {
            var operationResult = await subforumManager.LeaveSubforumAsync(id, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
