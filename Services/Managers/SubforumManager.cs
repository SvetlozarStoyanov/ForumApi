using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Subforums;
using Contracts.Services.Entity.Users;
using Contracts.Services.Managers;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Subforums.Input;

namespace Services.Managers
{
    public class SubforumManager : ISubforumManager
    {
        private readonly ISubforumService subforumService;
        private readonly IUnitOfWork unitOfWork;

        public SubforumManager(ISubforumService subforumService,
            IUnitOfWork unitOfWork)
        {
            this.subforumService = subforumService;
            this.unitOfWork = unitOfWork;
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
