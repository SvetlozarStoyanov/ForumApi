using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Subforums;
using Database.Entities.Identity;
using Database.Entities.Subforums;
using Microsoft.EntityFrameworkCore;
using Models.Common;
using Models.Common.Enums;
using Models.DTOs.Subforums.Input;
using Models.DTOs.Subforums.Output;

namespace Services.Entity.Subforums
{
    public class SubforumService : ISubforumService
    {
        private readonly IUnitOfWork unitOfWork;

        public SubforumService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<string>> GetAllSubforumNamesAsync()
        {
            var subforumNames = await unitOfWork.SubForumRepository.AllAsNoTracking()
                .Select(x => x.Name)
                .ToListAsync();

            return subforumNames;
        }

        public async Task<IEnumerable<SubforumDropdownDto>> GetSubforumsForDropdownAsync()
        {
            var subforumDtos = await unitOfWork.SubForumRepository.AllAsNoTracking()
                .Select(x => new SubforumDropdownDto()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();

            return subforumDtos;
        }

        public async Task<OperationResult<string>> CreateSubforumAsync(SubforumCreateDto subforumCreateDto, ApplicationUser admin)
        {
            var operationResult = new OperationResult<string>();

            var subforum = new Subforum()
            {
                Name = subforumCreateDto.Name,
                Description = subforumCreateDto.Description,
                CreatedOn = DateTime.UtcNow,
                Administrators = new List<ApplicationUser>() { admin },
                Users = new List<ApplicationUser>() { admin }
            };

            await unitOfWork.SubForumRepository.AddAsync(subforum);

            operationResult.Data = subforum.Name;

            return operationResult;
        }


        public async Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameForGuestUserAsync(string name)
        {
            var operationResult = new OperationResult<SubforumDetailsDto>();

            var subforum = await unitOfWork.SubForumRepository.FindByConditionAsNoTracking(x => x.Name.ToLower().Replace(" ", "") == name.ToLower().Replace(" ", ""))
                .Select(x => new SubforumDetailsDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UserIsMember = false,
                    UserCount = x.Users.Count,
                })
                .FirstOrDefaultAsync();

            if (subforum is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Subforum)} with name: {name} was not found!"));
                return operationResult;
            }

            operationResult.Data = subforum;

            return operationResult;
        }

        public async Task<OperationResult<SubforumDetailsDto>> GetSubforumByNameAsync(string name, string userId)
        {
            var operationResult = new OperationResult<SubforumDetailsDto>();

            var subforum = await unitOfWork.SubForumRepository.FindByConditionAsNoTracking(x => x.Name.ToLower().Replace(" ", "") == name.ToLower().Replace(" ", ""))
                .Select(x => new SubforumDetailsDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    UserIsMember = x.Users.Any(x => x.Id == userId),
                    UserCount = x.Users.Count,
                })
                .FirstOrDefaultAsync();

            if (subforum is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Subforum)} with name: {name} was not found!"));
                return operationResult;
            }

            operationResult.Data = subforum;

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

        public async Task<OperationResult> LeaveSubforumAsync(long subforumId, ApplicationUser user)
        {
            var operationResult = new OperationResult();

            var subforum = await unitOfWork.SubForumRepository.FindByCondition(x => x.Id == subforumId)
                .Include(x => x.Users.Where(x => x.Id == user.Id))
                .FirstOrDefaultAsync();

            if (subforum is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"{nameof(Subforum)} with id: {subforumId} was not found!"));
                return operationResult;
            }

            if (subforum.Users.Any(x => x.Id != user.Id))
            {
                operationResult.AddError(new Error(ErrorTypes.BadRequest, $"User with id: {user.Id} is not in {nameof(Subforum)} with id: {subforumId}"));
                return operationResult;
            }

            subforum.Users.Remove(user);

            return operationResult;
        }
    }
}
