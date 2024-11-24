using Contracts.Services.Managers;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Subforums;

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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateSubforum(SubforumCreateDto subforumCreateDto)
        {
            var operationResult = await subforumManager.CreateSubforumAsync(User.GetId(), subforumCreateDto);
            
            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
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
    }
}
