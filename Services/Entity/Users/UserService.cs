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

        public async Task<bool> IsUserNameTakenAsync(string userName)
        {
            return await unitOfWork.UserRepository.AllAsNoTracking().AnyAsync(u => u.NormalizedUserName == userName.ToUpper());
        }
    }
}
