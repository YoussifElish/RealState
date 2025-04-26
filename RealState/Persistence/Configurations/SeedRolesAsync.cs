using RealState.Abstactions.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RealState.Entities;
using Microsoft.AspNetCore.Identity;

namespace CureFusion.Persistence.EntitiesConfiguration
{
    public static class RoleSeeder // <<< لازم كلاس
    {
        public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roles = new List<ApplicationRole>
            {
                new ApplicationRole
                {
                    Id = DefaultRoles.AdminRoleId,
                    Name = DefaultRoles.Admin,
                    NormalizedName = DefaultRoles.Admin.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.AdminRoleConcurrenyStamp,
                    IsDefault = false
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.MemberRoleId,
                    Name = DefaultRoles.Member,
                    NormalizedName = DefaultRoles.Member.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.MemberRoleConcurrenyStamp,
                    IsDefault = true
                },
                new ApplicationRole
                {
                    Id = DefaultRoles.EmployeeRoleId,
                    Name = DefaultRoles.Employee,
                    NormalizedName = DefaultRoles.Employee.ToUpper(),
                    ConcurrencyStamp = DefaultRoles.EmployeeRoleConcurrenyStamp,
                    IsDefault = false
                }
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
