namespace Contracts.Services.JWT
{
    public interface IJwtService
    {
        string GenerateJwtToken(string userId, string userName);
    }
}
