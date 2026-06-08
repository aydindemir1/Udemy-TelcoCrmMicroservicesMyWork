using Core.Security.Redis.Services;
using Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace WebApi
{
    public static class SeedRedisRole
    {
        public static async Task SeedRedisRoleAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
            var roleCache = scope.ServiceProvider.GetRequiredService<IAuthorizedRoleService>();

            var adminUserId = Guid.Parse("02e915e9-82ec-43c7-a2a3-db0a2565f4db");
            var adminUser = await dbContext.Users.FirstOrDefaultAsync(x => x.Id == adminUserId);
            var roles = await dbContext.UserOperationClaims.Where(x => x.UserId == adminUserId).Include(x => x.OperationClaim).Select(x => x.OperationClaim.Name).ToListAsync();

            if (roles.Any())
            {
                await roleCache.AddRolesAsync(adminUser.Id.ToString(), roles);
            }
        }
    }
}
