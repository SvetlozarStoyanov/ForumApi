using Contracts.Services.Seeding;
using ForumApi.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace ForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedingController : ControllerBase
    {
        private readonly ISeedingService seedingService;

        public SeedingController(ISeedingService seedingService)
        {
            this.seedingService = seedingService;
        }

        [HttpPost]
        [Route("seed")]
        public async Task<IActionResult> SeedDatabase()
        {
            var operationResult = await seedingService.SeedAsync();

            if (!operationResult.IsSuccessful)
            {
                return this.Error(operationResult);
            }

            return Ok();
        }
    }
}
