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

        public SubforumManager(IUnitOfWork unitOfWork,
            ISubforumService subforumService)
        {
            this.unitOfWork = unitOfWork;
            this.subforumService = subforumService;
        }

        public async Task<IEnumerable<SubforumListDto>> GetSubforumsForGuestUserAsync(SubforumsQueryDto subforumsQueryDto)
        {
            return await subforumService.GetSubforumsForGuestUserAsync(subforumsQueryDto);
        }

        public async Task<OperationResult<IEnumerable<SubforumListDto>>> GetUserUnjoinedSubforumsAsync(string userId, SubforumsQueryDto subforumsQueryDto)
        {
            var operationResult = new OperationResult<IEnumerable<SubforumListDto>>();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var subforums = await subforumService.GetUserUnjoinedSubforumsAsync(userId, subforumsQueryDto);

            operationResult.Data = subforums;

            return operationResult;
        }

        public async Task<OperationResult<IEnumerable<SubforumListDto>>> GetUserJoinedSubforumsAsync(string userId, SubforumsQueryDto subforumsQueryDto)
        {
            var operationResult = new OperationResult<IEnumerable<SubforumListDto>>();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var subforums = await subforumService.GetUserJoinedSubforumsAsync(userId, subforumsQueryDto);

            operationResult.Data = subforums;

            return operationResult;
        }

        public async Task<IEnumerable<string>> GetAllSubforumNamesAsync()
        {
            return await subforumService.GetAllSubforumNamesAsync();
        }

        public async Task<IEnumerable<SubforumDropdownDto>> GetSubforumsForDropdownAsync()
        {
            return await subforumService.GetSubforumsForDropdownAsync();
        }

        public async Task<IEnumerable<SubforumSearchDto>> SearchSubforumsAsync(string searchTerm)
        {
            return await subforumService.SearchSubforumsAsync(searchTerm);
        }

        public async Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name, string? userId)
        {
            var operationResult = new OperationResult<SubforumDetailsDto>();


            if (userId is not null)
            {
                var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

                if (user is null)
                {
                    operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                    return operationResult;
                }
            }

            var fetchSubforumByNameResult = userId is not null ? await subforumService.GetSubforumByNameAsync(name, userId) :
                await subforumService.GetSubforumByNameForGuestUserAsync(name);

            if (!fetchSubforumByNameResult.IsSuccessful)
            {
                operationResult.AppendErrors(fetchSubforumByNameResult);
                return operationResult;
            }

            var subforumDto = fetchSubforumByNameResult.Data;


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

        public async Task<OperationResult> LeaveSubforumAsync(long subforumId, string userId)
        {
            var operationResult = new OperationResult();

            var user = await unitOfWork.UserRepository.GetByIdAsync(userId);

            if (user is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            var joinForumResult = await subforumService.LeaveSubforumAsync(subforumId, user);

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
