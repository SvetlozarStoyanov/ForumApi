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
using Models.DTOs.CommentReplies.Output;
using Models.DTOs.Comments.Output;
using Models.DTOs.Posts.Input;
using Models.DTOs.Posts.Output;
using Models.DTOs.Subforums.Output;
using Models.DTOs.UserPermittedActions.Output;
using Models.DTOs.Users.Output;
using Models.DTOs.Votes.Output;
using Models.Enums.Votes;

namespace Services.Entity.Posts
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork unitOfWork;

        public PostService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
                    CommentCount = x.Comments.Count + x.Comments.SelectMany(c => c.Replies).Count(),
                    User = new UserMinInfoDto()
                    {
                        Id = x.UserId,
                        Username = x.User.UserName!
                    },
                    Subforum = new SubforumMinInfoDto()
                    {
                        Id = x.SubforumId,
                        Name = x.Subforum.Name
                    },
                })
                .ToListAsync();

            return posts;
        }

        public async Task<IEnumerable<PostListDto>> GetHomePagePostsAsync(string userId)
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
                    CommentCount = x.Comments.Count + x.Comments.SelectMany(c => c.Replies).Count(),
                    User = new UserMinInfoDto()
                    {
                        Id = x.UserId,
                        Username = x.User.UserName!
                    },
                    Subforum = new SubforumMinInfoDto()
                    {
                        Id = x.SubforumId,
                        Name = x.Subforum.Name
                    },
                    UserPermittedActions = new UserPermittedActionsDto()
                    {
                        CanEdit = x.UserId == userId,
                        CanDelete = x.UserId == userId
                    },
                    UserVote = new UserVoteDto()
                    {
                        VoteType = x.Votes.Any(x => x.Type == PostVotes.Up && x.UserId == userId) ? VoteTypes.Up :
                                    x.Votes.Any(x => x.Type == PostVotes.Down && x.UserId == userId) ? VoteTypes.Down
                                    : VoteTypes.None
                    }
                })
                .ToListAsync();

            return posts;
        }

        public async Task<IEnumerable<PostListDto>> GetSubforumPostsForGuestUserAsync(long subforumId)
        {
            var posts = await unitOfWork.PostRepository
                .FindByConditionAsNoTracking(x => x.SubforumId == subforumId)
                .Select(x => new PostListDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Text = x.Text,
                    VoteTally = x.Votes.Where(x => x.Type == PostVotes.Up).Count() - x.Votes.Where(x => x.Type == PostVotes.Down).Count(),
                    CommentCount = x.Comments.Count + x.Comments.SelectMany(c => c.Replies).Count(),
                    User = new UserMinInfoDto()
                    {
                        Id = x.UserId,
                        Username = x.User.UserName!
                    },
                    Subforum = new SubforumMinInfoDto()
                    {
                        Id = x.SubforumId,
                        Name = x.Subforum.Name
                    },

                })
                .ToListAsync();

            return posts;
        }

        public async Task<IEnumerable<PostListDto>> GetSubforumPostsAsync(long subforumId, string userId)
        {
            var posts = await unitOfWork.PostRepository
                .FindByConditionAsNoTracking(x => x.SubforumId == subforumId)
                .Select(x => new PostListDto()
                {
                    Id = x.Id,
                    Title = x.Title,
                    Text = x.Text,
                    VoteTally = x.Votes.Where(x => x.Type == PostVotes.Up).Count() - x.Votes.Where(x => x.Type == PostVotes.Down).Count(),
                    CommentCount = x.Comments.Count + x.Comments.SelectMany(c => c.Replies).Count(),
                    User = new UserMinInfoDto()
                    {
                        Id = x.UserId,
                        Username = x.User.UserName!
                    },
                    Subforum = new SubforumMinInfoDto()
                    {
                        Id = x.SubforumId,
                        Name = x.Subforum.Name
                    },
                    UserPermittedActions = new UserPermittedActionsDto()
                    {
                        CanEdit = x.UserId == userId,
                        CanDelete = x.UserId == userId
                    },
                    UserVote = new UserVoteDto()
                    {
                        VoteType = x.Votes.Any(x => x.Type == PostVotes.Up && x.UserId == userId) ? VoteTypes.Up :
                                    x.Votes.Any(x => x.Type == PostVotes.Down && x.UserId == userId) ? VoteTypes.Down
                                    : VoteTypes.None
                    }
                })
                .ToListAsync();

            return posts;
        }

        public async Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdForGuestUserAsync(long id)
        {
            var operationResult = new OperationResult<PostDetailsDto>();
            var postDetails = await unitOfWork.PostRepository.FindByConditionAsNoTracking(x => x.Id == id)
                .Select(x => new PostDetailsDto()
                {
                    Post = new PostListDto()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Text = x.Text,
                        VoteTally = x.Votes.Count(x => x.Type == PostVotes.Up) - x.Votes.Count(x => x.Type == PostVotes.Down),
                        CommentCount = x.Comments.Count + x.Comments.SelectMany(c => c.Replies).Count(),
                        User = new UserMinInfoDto()
                        {
                            Id = x.UserId,
                            Username = x.User.UserName!
                        },
                        Subforum = new SubforumMinInfoDto()
                        {
                            Id = x.SubforumId,
                            Name = x.Subforum.Name
                        },
                    }
                })
                .FirstOrDefaultAsync();


            if (postDetails is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Post)} with id: {id} was not found!"));
                return operationResult;
            }


            operationResult.Data = postDetails;

            return operationResult;
        }

        public async Task<OperationResult<PostDetailsDto>> GetPostDetailsByIdAsync(long id, string userId)
        {
            var operationResult = new OperationResult<PostDetailsDto>();
            var postDetails = await unitOfWork.PostRepository.FindByConditionAsNoTracking(x => x.Id == id)
                .Select(x => new PostDetailsDto()
                {
                    Post = new PostListDto()
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Text = x.Text,
                        VoteTally = x.Votes.Count(x => x.Type == PostVotes.Up) - x.Votes.Count(x => x.Type == PostVotes.Down),
                        CommentCount = x.Comments.Count + x.Comments.SelectMany(c => c.Replies).Count(),
                        User = new UserMinInfoDto()
                        {
                            Id = x.UserId,
                            Username = x.User.UserName!
                        },
                        Subforum = new SubforumMinInfoDto()
                        {
                            Id = x.SubforumId,
                            Name = x.Subforum.Name
                        },
                        UserPermittedActions = new UserPermittedActionsDto()
                        {
                            CanEdit = x.UserId == userId,
                            CanDelete = x.UserId == userId
                        },
                        UserVote = new UserVoteDto()
                        {
                            VoteType = x.Votes.Any(x => x.Type == PostVotes.Up && x.UserId == userId) ? VoteTypes.Up :
                                    x.Votes.Any(x => x.Type == PostVotes.Down && x.UserId == userId) ? VoteTypes.Down
                                    : VoteTypes.None
                        }
                    }
                })
                .FirstOrDefaultAsync();


            if (postDetails is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Post)} with id: {id} was not found!"));
                return operationResult;
            }

            operationResult.Data = postDetails;

            return operationResult;
        }

        public async Task<Post> CreatePostAsync(PostCreateDto postCreateDto,
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

            return post;
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
    }
}
