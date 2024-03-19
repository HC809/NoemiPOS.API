using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Tenants;

namespace NoemiPOS.Infraestructure.Configurations;
internal sealed class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable("tenants");
        builder.HasKey(tenant => tenant.Id);

        builder.OwnsOne(tenant => tenant.Owner);
        builder.OwnsOne(tenant => tenant.Address);

        builder.Property(tenant => tenant.Description)
            .HasMaxLength(2000)
            .HasConversion(description => description.Value, value => new Description(value));

        builder.Property(tenant => tenant.ManagementNote)
            .HasMaxLength(2000)
            .HasConversion(managementNote => managementNote.Value, value => new ManagementNote(value));
    }
}
