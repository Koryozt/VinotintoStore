using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.General;
using VM.Persistence.Constants;

namespace VM.Persistence.Configurations;

internal sealed class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable(TableNames.CartItem);

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Quantity)
            .HasConversion(x => x.Value, v => Quantity.Create(v).Value);

        builder.Property(ci => ci.TotalPrice)
            .HasConversion(x => x.Value, v => Amount.Create(v).Value);

        builder.HasOne(ci => ci.Product)
            .WithOne();
    }
}
