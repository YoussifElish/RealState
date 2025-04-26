using RealState.Abstactions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace CureFusion.Persistence.EntitiesConfiguration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //Default Data
            builder.HasData([
                new IdentityUserRole<String> {
                   UserId = DefaultUsers.AdminId,
                   RoleId = DefaultRoles.AdminRoleId
                },

                ]);
        }
    }
}
