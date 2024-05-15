using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoemiPOS.Domain.Businesses;
using NoemiPOS.Domain.Shared;

namespace NoemiPOS.Infraestructure.Configurations;
internal sealed class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.ToTable("businesses");
        builder.HasKey(business => business.Id);

        builder.Property(business => business.Name)
           .IsRequired()
           .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(business => business.Description)
           .IsRequired()
           .HasMaxLength(2000)
           .HasConversion(description => description.Value, value => new Description(value));

        builder.Property(business => business.Rtn)
           .IsRequired()
           .HasMaxLength(14)
           .HasConversion(rtn => rtn.Value, value => new BusinessRtn(value));

        builder.Property(business => business.Email)
           .IsRequired()
           .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(business => business.Phone)
           .IsRequired()
           .HasMaxLength(8)
           .HasConversion(phone => phone.Value, value => new PhoneNumber(value));

        builder.Property(business => business.SecondaryPhone)
            .HasMaxLength(8)
            .HasConversion(secondaryPhone => secondaryPhone != null ? secondaryPhone.Value : null,
                value => value != null ? new SecondaryPhoneNumber(value) : null);

        builder.Property(business => business.Type)
           .HasConversion(type => type.ToString(),  value => (BusinessType)Enum.Parse(typeof(BusinessType), value));

        builder.Property(business => business.ManagementNote)
            .HasMaxLength(2000)
            .HasConversion(managementNote => managementNote.Value, value => new ManagementNote(value));

        builder.Property(business => business.WebSiteUrl)
            .HasMaxLength(250)
            .HasConversion(webSiteUrl => webSiteUrl != null ? webSiteUrl.Value : null,
                value => value != null ? new WebSiteUrl(value) : null);
    }
}
