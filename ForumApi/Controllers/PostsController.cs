using Contracts.Services.Managers;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Posts.Input;

namespace ForumApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostManager postManager;

        public PostsController(IPostManager postManager)
        {
            this.postManager = postManager;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("get-guest-user-posts")]
        public async Task<IActionResult> GetGuestUserHomePagePosts()
        {
            var posts = await postManager.GetHomePagePostsForGuestUserAsync();

            return Ok(posts);
        }


        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreatePost(PostCreateDto postCreateDto)
        {
            var operationResult = await postManager.CreatePostAsync(User.GetId(), postCreateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> CreatePost([FromRoute] long id, PostUpdateDto postUpdateDto)
        {
            var operationResult = await postManager.UpdatePostAsync(id, User.GetId(), postUpdateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeletePost([FromRoute] long id)
        {
            var operationResult = await postManager.DeletePostAsync(id, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
