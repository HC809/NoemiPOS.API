using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoemiPOS.Domain.Businesses;
using NoemiPOS.Domain.Shared;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Infraestructure.Configurations;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(user => new { user.Id, user.BusinessId });

        builder.Property(user => user.FirstName)
            .IsRequired()
            .HasMaxLength(125)
            .HasConversion(firstName => firstName.Value, value => new FirstName(value));

        builder.Property(user => user.LastName)
            .IsRequired()
            .HasMaxLength(125)
            .HasConversion(lastName => lastName.Value, value => new LastName(value));

        builder.Property(user => user.Email)
           .IsRequired()
           .HasConversion(email => email.Value, value => new Email(value));

        builder.Property(user => user.Username)
            .IsRequired()
            .HasMaxLength(25)
            .HasConversion(lastName => lastName.Value, value => new Username(value));

        builder.Property(user => user.Dni)
            .HasMaxLength(13)
            .HasConversion(dni => dni != null ? dni.Value : null,
                value => value != null ? new Dni(value) : null);

        builder.Property(user => user.Phone)
            .IsRequired()
            .HasMaxLength(8)
            .HasConversion(phone => phone != null ? phone.Value : null,
                value => value != null ? new PhoneNumber(value) : null);


        builder.Property(user => user.HashPassword).IsRequired();

        builder.HasOne<Business>()
            .WithMany()
            .HasForeignKey(x => x.BusinessId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(user => user.Username).IsUnique();
        builder.HasIndex(user => user.Email).IsUnique();
        builder.HasIndex(user => user.Dni).IsUnique();
    }
}
