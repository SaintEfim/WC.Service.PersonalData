using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WC.Service.PersonalData.Data.Models;
using WC.Service.PersonalData.Shared.Models;

namespace WC.Service.PersonalData.Data.PostgreSql.Configuration;

public class PersonalDataEntityConfiguration : IEntityTypeConfiguration<PersonalDataEntity>
{
    public void Configure(
        EntityTypeBuilder<PersonalDataEntity> builder)
    {
        builder.Property(x => x.EmployeeId)
            .IsRequired();

        builder.HasIndex(x => x.EmployeeId)
            .IsUnique();

        builder.Property(x => x.Email)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.Password)
            .IsRequired();

        builder.Property(x => x.Role)
            .HasConversion<byte>()
            .HasDefaultValue(UserRole.User);
    }
}
