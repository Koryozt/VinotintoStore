using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.Domain.Entities;
using VM.Persistence.Constants;

namespace VM.Persistence.Configurations;

internal sealed class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable(TableNames.ShoppingCart);

        builder.HasKey(sc => sc.Id);

        builder
            .HasMany(s => s.CartItems)
            .WithOne(ci => ci.ShoppingCart)
            .HasForeignKey(ci => ci.ShoppingCartId);
    }
}
