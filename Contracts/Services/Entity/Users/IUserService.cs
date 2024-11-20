namespace Contracts.Services.Entity.Users
{
    public interface IUserService
    {
        Task<bool> IsUserNameTakenAsync(string userName);
    }
}
