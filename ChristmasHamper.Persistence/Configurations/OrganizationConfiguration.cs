using ChristmasHamper.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChristmasHamper.Persistence.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Acronym).IsRequired().HasMaxLength(10);
    }
}

