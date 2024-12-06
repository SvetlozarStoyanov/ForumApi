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

        public async Task<IEnumerable<string>> GetAllSubforumNamesAsync()
        {
            return await subforumService.GetAllSubforumNamesAsync();
        }

        public async Task<IEnumerable<SubforumDropdownDto>> GetSubforumsForDropdownAsync()
        {
            return await subforumService.GetSubforumsForDropdownAsync();
        }

        public async Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name, string? userId)
        {
            var operationResult = new OperationResult<SubforumDetailsDto>();


            var fetchSubforumByNameResult = await subforumService.GetSubforumByNameAsync(name);


            if (!fetchSubforumByNameResult.IsSuccessful)
            {
                operationResult.AppendErrors(fetchSubforumByNameResult);
                return operationResult;
            }

            var subforumDto = fetchSubforumByNameResult.Data;

            if (userId is not null)
            {
                var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

                if (user is null)
                {
                    operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                    return operationResult;
                }
            }

            var subforumPosts = userId is not null ? await postService.GetSubforumPostsAsync(subforumDto.Id, userId)
            : await postService.GetSubforumPostsForGuestUserAsync(subforumDto.Id);

            subforumDto.Posts = subforumPosts;

            operationResult.Data = subforumDto;

            return operationResult;
        }

        public async Task<OperationResult<string>> CreateSubforumAsync(string userId, SubforumCreateDto subforumCreateDto)
        {
            var operationResult = new OperationResult<string>();

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

            operationResult.Data = createSubforumResult.Data;

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
