using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Subforums;
using Database.Entities.Identity;
using Database.Entities.Subforums;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Subforums;

namespace Services.Entity.Subforums
{
    public class SubforumService : ISubforumService
    {
        private readonly IUnitOfWork unitOfWork;

        public SubforumService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<OperationResult> CreateSubforumAsync(SubforumCreateDto subforumCreateDto, ApplicationUser admin)
        {
            var operationResult = new OperationResult();

            var subforum = new Subforum()
            {
                Name = subforumCreateDto.Name,
                CreatedOn = DateTime.UtcNow,
                Administrators = new List<ApplicationUser>() { admin },
            };

            await unitOfWork.SubForumRepository.AddAsync(subforum);

            return operationResult;
        }

        public async Task<OperationResult> JoinSubforumAsync(long subforumId, ApplicationUser user)
        {
            var operationResult = new OperationResult();

            var subforum = await unitOfWork.SubForumRepository.FindByCondition(x => x.Id == subforumId)
                .Include(x => x.Users)
                .FirstOrDefaultAsync();

            if (subforum is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Subforum)} with id: {subforumId} was not found!"));
                return operationResult;
            }

            if (subforum.Users.Any(x => x.Id == user.Id))
            {
                operationResult.AddError(new Error(ErrorTypes.BadRequest, $"User with id: {user.Id} is already in {nameof(Subforum)} with id: {subforumId}"));
                return operationResult;
            }

            subforum.Users.Add(user);

            return operationResult;
        }
    }
}
