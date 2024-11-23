using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Subforums;
using Database.Entities.Identity;
using Database.Entities.Subforums;
using Models.Common;
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
    }
}
