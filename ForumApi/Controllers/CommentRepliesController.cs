using Contracts.Services.Managers;
using Database.Enums.Votes;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.CommentReplies.Input;
using Models.DTOs.Comments;

namespace ForumApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentRepliesController : ControllerBase
    {
        private readonly ICommentReplyManager commentReplyManager;

        public CommentRepliesController(ICommentReplyManager commentReplyManager)
        {
            this.commentReplyManager = commentReplyManager;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateComment(CommentReplyCreateDto commentReplyCreateDto)
        {
            var operationResult = await commentReplyManager.CreateCommentReplyAsync(User.GetId(), commentReplyCreateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok(operationResult.Data);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCommentReply([FromRoute] long id, CommentReplyUpdateDto commentUpdateDto)
        {
            var operationResult = await commentReplyManager.UpdateCommentReplyAsync(id, User.GetId(), commentUpdateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }
            return Ok();
        }

        [HttpPost]
        [Route("upvote/{id}")]
        public async Task<IActionResult> UpvoteCommentReply([FromRoute] long id)
        {
            var operationResult = await commentReplyManager.VoteOnCommentReplyAsync(id, User.GetId(), CommentReplyVotes.Up);
            
            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPost]
        [Route("downvote/{id}")]
        public async Task<IActionResult> DownvoteCommentReply([FromRoute] long id)
        {
            var operationResult = await commentReplyManager.VoteOnCommentReplyAsync(id, User.GetId(), CommentReplyVotes.Down);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteCommentReply([FromRoute] long id)
        {
            var operationResult = await commentReplyManager.DeleteCommentReplyAsync(id, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
