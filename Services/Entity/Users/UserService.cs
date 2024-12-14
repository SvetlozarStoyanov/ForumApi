using Contracts.DataAccess.UnitOfWork;
using Contracts.Services.Entity.Users;
using Microsoft.EntityFrameworkCore;
using Models.Common.Enums;
using Models.Common;
using Models.DTOs.Users.Output;

namespace Services.Entity.ApplicationUsers
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<string>> GetAllUsernamesAsync()
        {
            var usernames = await unitOfWork.UserRepository.AllAsNoTracking()
                .Select(x => x.UserName)
                .ToListAsync();

            return usernames;
        }

        public async Task<OperationResult<UserShortInfoDto>> GetUserByIdAsync(string userId)
        {
            var operationResult = new OperationResult<UserShortInfoDto>();
            var userDto = await unitOfWork.UserRepository.FindByConditionAsNoTracking(x => x.Id == userId)
                .Select(x => new UserShortInfoDto()
                {
                    Username = x.UserName,
                    Email = x.Email
                })
                .FirstOrDefaultAsync();

            if (userDto is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with id: {userId} was not found!"));
                return operationResult;
            }

            operationResult.Data = userDto;

            return operationResult;
        }

        public async Task<IEnumerable<UserSearchDto>> SearchUsersAsync(string searchTerm)
        {
            var users = await unitOfWork.UserRepository.FindByConditionAsNoTracking(x => x.UserName.ToLower().Contains(searchTerm.ToLower()))
                .Select(x => new UserSearchDto()
                {
                    Id = x.Id,
                    Username = x.UserName,
                    PostCount = x.Posts.Count,
                    CommentCount = x.Comments.Count
                })
                .ToListAsync();

            return users;
        }

        public async Task<OperationResult<UserDetailsDto>> GetUserDetailsAsync(string userName)
        {
            var operationResult = new OperationResult<UserDetailsDto>();

            var userDto = await unitOfWork.UserRepository.FindByConditionAsNoTracking(x => x.UserName == userName)
                .Select(x => new UserDetailsDto()
                {
                    Id = x.Id,
                    Username = x.UserName,
                    PostCount = x.Posts.Count(),
                    CommentCount = x.Comments.Count + x.CommentReplies.Count
                })
                .FirstOrDefaultAsync();

            if (userDto is null)
            {
                operationResult.AddError(new Error(ErrorTypes.NotFound, $"User with username: {userName} was not found!"));
                return operationResult;
            }

            operationResult.Data = userDto;

            return operationResult;
        }

        public async Task<bool> IsUserNameTakenAsync(string userName)
        {
            return await unitOfWork.UserRepository.AllAsNoTracking().AnyAsync(u => u.NormalizedUserName == userName.ToUpper());
        }

    }
}
