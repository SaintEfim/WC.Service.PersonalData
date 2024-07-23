using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WC.Service.PersonalData.Data.Models;

namespace WC.Service.PersonalData.Data.PostgreSql.Context;

public sealed class PersonalDataDbContext : DbContext
{
    public PersonalDataDbContext(
        DbContextOptions<PersonalDataDbContext> options,
        IHostEnvironment environment)
        : base(options)
    {
        if (environment.IsDevelopment())
        {
            Database.Migrate();
        }
    }

    public DbSet<PersonalDataEntity> PersonalData { get; set; } = null!;
}
