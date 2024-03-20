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

        builder.OwnsOne(tenant => tenant.Address);

        builder.Property(tenant => tenant.FullName)
            .IsRequired()
            .HasMaxLength(250)
            .HasConversion(fullName => fullName.Value, value => new FullName(value));

        builder.Property(tenant => tenant.Email)
           .IsRequired()
           .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(tenant => tenant.Dni)
            .IsRequired()
            .HasMaxLength(13)
            .HasConversion(dni => dni.Value, value => new Dni(value));

        builder.Property(tenant => tenant.Rtn)
            .IsRequired()
            .HasMaxLength(14)
            .HasConversion(rtn => rtn.Value, value => new TenantRtn(value));

        builder.Property(tenant => tenant.Phone)
            .IsRequired()
            .HasMaxLength(8)
            .HasConversion(phone => phone.Value, value => new PhoneNumber(value));

        builder.Property(tenant => tenant.SecondaryPhone)
            .HasMaxLength(8)
            .HasConversion(
            secondaryPhone => secondaryPhone != null ? secondaryPhone.Value : null,
            value => value != null ? new SecondaryPhoneNumber(value) : null);

        builder.Property(tenant => tenant.Description)
            .IsRequired()
            .HasMaxLength(2000)
            .HasConversion(description => description.Value, value => new Description(value));

        builder.Property(tenant => tenant.ManagementNote)
            .HasMaxLength(2000)
            .HasConversion(managementNote => managementNote.Value, value => new ManagementNote(value));

        builder.HasIndex(user => user.Email).IsUnique();
        builder.HasIndex(user => user.Dni).IsUnique();
        builder.HasIndex(user => user.Rtn).IsUnique();
    }
}
