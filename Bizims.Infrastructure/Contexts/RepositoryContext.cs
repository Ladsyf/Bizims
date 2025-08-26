using Bizims.Application.Settings;
using Bizims.Domain.Models.Businesses;
using Bizims.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Bizims.Infrastructure.Contexts;

public class RepositoryContext : DbContext
{
    private readonly DatabaseSettings _databaseSettings;

    public DbSet<User> Users { get; set; }
    public DbSet<Business> Businesses { get; set; }
    public DbSet<BusinessSetting> BusinessSettings { get; set; }

    public RepositoryContext(IOptions<DatabaseSettings> databaseSettings)
    {
        _databaseSettings = databaseSettings.Value;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_databaseSettings.DefaultConnectionString, x => x.MigrationsAssembly("Bizims.Api"));

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Business>()
            .HasOne(b => b.Setting)
            .WithOne(bs => bs.Business)
            .HasForeignKey<BusinessSetting>(bs => bs.BusinessId);

        modelBuilder.Entity<Business>()
            .HasOne(b => b.User)
            .WithMany(u => u.Businesses)
            .HasForeignKey(b => b.UserId);

        base.OnModelCreating(modelBuilder);
    }
}