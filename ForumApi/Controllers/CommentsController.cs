using Contracts.Services.Managers;
using Database.Enums.Votes;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Comments.Input;

namespace ForumApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentManager commentManager;

        public CommentsController(ICommentManager commentManager)
        {
            this.commentManager = commentManager;
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("post-comments/{postId}")]
        public async Task<IActionResult> GetPostComments([FromRoute] long postId)
        {
            var operationResult = await commentManager.GetPostCommentsAsync(postId, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateComment(CommentCreateDto commentCreateDto)
        {
            var operationResult = await commentManager.CreateCommentAsync(User.GetId(), commentCreateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] long id, CommentUpdateDto commentUpdateDto)
        {
            var operationResult = await commentManager.UpdateCommentAsync(id, User.GetId(), commentUpdateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }
            return Ok();
        }

        [HttpPost]
        [Route("upvote/{id}")]
        public async Task<IActionResult> UpvoteComment([FromRoute] long id)
        {
            var operationResult = await commentManager.VoteOnCommentAsync(id, User.GetId(), CommentVotes.Up);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPost]
        [Route("downvote/{id}")]
        public async Task<IActionResult> DownvoteComment([FromRoute] long id)
        {
            var operationResult = await commentManager.VoteOnCommentAsync(id, User.GetId(), CommentVotes.Down);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] long id)
        {
            var operationResult = await commentManager.DeleteCommentAsync(id, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
