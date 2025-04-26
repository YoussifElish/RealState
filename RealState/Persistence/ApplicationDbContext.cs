using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealState.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;
using RealState.Entities.PropertForSell;
using RealState.Entities.PropertForRent;

namespace RealState.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IHttpContextAccessor httpContextAccessor ) : IdentityDbContext<ApplicationUser,ApplicationRole,string>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        var cascadeFKs = builder.Model.GetEntityTypes().SelectMany(t => t.GetForeignKeys()).Where(x => x.DeleteBehavior == DeleteBehavior.Cascade && !x.IsOwnership);
        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        base.OnModelCreating(builder);
    }

    public DbSet<PropertForSell> propertForSells { get; set; }
    public DbSet<PropertForRent>  propertForRents { get; set; }
    public DbSet<ContactLeads>   contactLeads { get; set; }
    public DbSet<UploadedFile>   uploadedFiles { get; set; }

    //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{

    //    var entries = ChangeTracker.Entries<AuditableEntity>();
    //    foreach (var entityEntry in entries)
    //    {

    //        var currentUserId = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault().Value;
    //        if (entityEntry.State == EntityState.Added)
    //        {
    //            entityEntry.Property(x => x.CreatedByID).CurrentValue = currentUserId;
    //        }

    //        else if (entityEntry.State == EntityState.Modified)
    //        {
    //            entityEntry.Property(x => x.UpdatedById).CurrentValue = currentUserId;
    //            entityEntry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
    //        }
    //    }

    //    return base.SaveChangesAsync(cancellationToken);
    //}


}
