using Contracts.Services.Managers;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Comments;

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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateComment(CommentCreateDto commentCreateDto)
        {
            var operationResult = await commentManager.CreateCommentAsync(User.GetId(), commentCreateDto);

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
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
