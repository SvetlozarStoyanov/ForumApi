using Contracts.Services.Managers;
using Database.Enums.Votes;
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

        [AllowAnonymous]
        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> Details([FromRoute] long id)
        {
            var operationResult = await postManager.GetPostDetailsByIdAsync(id);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
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
        public async Task<IActionResult> UpdatePost([FromRoute] long id, PostUpdateDto postUpdateDto)
        {
            var operationResult = await postManager.UpdatePostAsync(id, User.GetId(), postUpdateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPost]
        [Route("upvote/{id}")]
        public async Task<IActionResult> UpvotePost([FromRoute] long id)
        {
            var operationResult = await postManager.VoteOnPostAsync(id, User.GetId(), PostVotes.Up);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPost]
        [Route("downvote/{id}")]
        public async Task<IActionResult> DownvotePost([FromRoute] long id)
        {
            var operationResult = await postManager.VoteOnPostAsync(id, User.GetId(), PostVotes.Down);

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
