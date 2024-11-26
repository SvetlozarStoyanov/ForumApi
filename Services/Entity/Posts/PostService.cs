using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Posts;
using Database.Entities.Identity;
using Database.Entities.Posts;
using Database.Entities.Subforums;
using Database.Enums.Statuses;
using Database.Enums.Votes;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Posts.Input;
using Models.DTOs.Posts.Output;
using Models.DTOs.Users.Output;

namespace Services.Entity.Posts
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task CreatePostAsync(PostCreateDto postCreateDto,
            ApplicationUser user,
            Subforum subforum)
        {
            var currDate = DateTime.UtcNow;

            var post = new Post()
            {
                Title = postCreateDto.Title,
                Text = postCreateDto.Text,
                CreatedOn = currDate,
                UpdatedOn = currDate,
                User = user,
                Subforum = subforum
            };

            await unitOfWork.PostRepository.AddAsync(post);
        }

        public async Task<OperationResult> UpdatePostAsync(long postId, string userId, PostUpdateDto postUpdateDto)
        {
            var operationResult = new OperationResult();

            var post = await unitOfWork.PostRepository.FindByCondition(x => x.Id == postId)
                .FirstOrDefaultAsync();

            if (post is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Post)} with id: {postId} was not found!"));
                return operationResult;
            }

            if (post.UserId != userId)
            {
                operationResult.AddError(new Error(ErrorTypes.BadRequest, $"{nameof(Post)} with id: {postId} does not belong to user with id: {userId}!"));
                return operationResult;
            }

            post.Text = postUpdateDto.Text;
            post.UpdatedOn = DateTime.UtcNow;

            return operationResult;
        }

        public async Task<OperationResult> DeletePostAsync(long postId, string userId)
        {
            var operationResult = new OperationResult();

            var post = await unitOfWork.PostRepository.FindByCondition(x => x.Id == postId)
                .FirstOrDefaultAsync();

            if (post is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Post)} with id: {postId} was not found!"));
                return operationResult;
            }

            if (post.UserId != userId)
            {
                operationResult.AddError(new Error(ErrorTypes.BadRequest, $"{nameof(Post)} with id: {postId} does not belong to user with id: {userId}!"));
                return operationResult;
            }

            post.Status = EntityStatus.Archived;
            post.UpdatedOn = DateTime.UtcNow;

            return operationResult;
        }

        public async Task<IEnumerable<PostListDto>> GetHomePagePostsForGuestUserAsync()
        {
            var posts = await unitOfWork.PostRepository.AllAsNoTracking()
                .OrderByDescending(x => x.CreatedOn)
                .Take(20)
                .Select(x => new PostListDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Text = x.Text,
                    VoteTally = x.Votes.Where(x => x.Type == PostVotes.Up).Count() - x.Votes.Where(x => x.Type == PostVotes.Down).Count(),
                    User = new UserPostMinInfoDto()
                    {
                        Id = x.UserId,
                        Username = x.User.UserName!
                    }
                })
                .ToListAsync();

            return posts;
        }
    }
}
