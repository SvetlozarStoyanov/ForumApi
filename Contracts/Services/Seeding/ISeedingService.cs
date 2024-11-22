using Models.Common;

namespace Contracts.Services.Seeding
{
    public interface ISeedingService
    {
        Task<OperationResult> SeedAsync();
    }
}
