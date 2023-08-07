using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;
using VM.Domain.ValueObjects.Users;
using VM.Persistence.Constants;

namespace VM.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.User);

        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.Firstname)
            .HasConversion(x => x.Value, v => Name.Create(v).Value);

        builder.Property(u => u.Lastname)
            .HasConversion(x => x.Value, v => Name.Create(v).Value);

        builder.Property(u => u.Email)
            .HasConversion(x => x.Value, v => Email.Create(v).Value);

        builder.Property(u => u.Password)
            .HasConversion(x => x.Value, v => Password.Create(v).Value);

        builder
            .HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

        builder
            .HasOne(u => u.ShoppingCart)
            .WithOne(s => s.User)
            .HasForeignKey<ShoppingCart>(s => s.UserId);
    }
}
