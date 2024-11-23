using Contracts.Services.Managers;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.CommentReplies;
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
            var operationResult = await commentReplyManager.CreateCommentReplyAsync(commentReplyCreateDto, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] long id, CommentReplyUpdateDto commentUpdateDto)
        {
            var operationResult = await commentReplyManager.UpdateCommentReplyAsync(id, commentUpdateDto, User.GetId());

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
            var operationResult = await commentReplyManager.DeleteCommentReplyAsync(id, User.GetId());

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
