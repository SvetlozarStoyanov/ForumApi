using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Posts;
using Contracts.Services.Entity.Subforums;
using Contracts.Services.Managers;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Subforums.Input;
using Models.DTOs.Subforums.Output;

namespace Services.Managers
{
    public class SubforumManager : ISubforumManager
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ISubforumService subforumService;
        private readonly IPostService postService;

        public SubforumManager(IUnitOfWork unitOfWork,
            ISubforumService subforumService,
            IPostService postService)
        {
            this.unitOfWork = unitOfWork;
            this.subforumService = subforumService;
            this.postService = postService;
        }

        public async Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name)
        {
            var operationResult = new OperationResult<SubforumDetailsDto>();
            var fetchSubforumByNameResult = await subforumService.GetSubforumByNameAsync(name);

            if (!fetchSubforumByNameResult.IsSuccessful)
            {
                operationResult.AppendErrors(fetchSubforumByNameResult);
                return operationResult;
            }

            var subforum = fetchSubforumByNameResult.Data;

            var subforumPosts = await postService.GetSubforumPostsAsync(subforum.Id);

            subforum.Posts = subforumPosts;

            operationResult.Data = subforum;
            
            return operationResult;
        }

        public async Task<OperationResult> CreateSubforumAsync(string userId, SubforumCreateDto subforumCreateDto)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var createSubforumResult = await subforumService.CreateSubforumAsync(subforumCreateDto, user);

            if (!createSubforumResult.IsSuccessful)
            {
                operationResult.AppendErrors(createSubforumResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }

        public async Task<OperationResult> JoinSubforumAsync(long subforumId, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var joinForumResult = await subforumService.JoinSubforumAsync(subforumId, user);

            if (!joinForumResult.IsSuccessful)
            {
                operationResult.AppendErrors(joinForumResult);
                return operationResult;
            }

            await unitOfWork.SaveChangesAsync();

            return operationResult;
        }
    }
}
