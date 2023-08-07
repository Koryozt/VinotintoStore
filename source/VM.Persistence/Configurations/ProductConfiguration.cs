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
using VM.Persistence.Constants;

namespace VM.Persistence.Configurations;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable(TableNames.Product);

        builder.HasKey(p => p.Id);

        builder.HasIndex(p => p.Name).IsUnique();

        builder.Property(p => p.Name)
            .HasConversion(x => x.Value, v => Name.Create(v).Value);

        builder.Property(p => p.Description)
            .HasConversion(x => x.Value, v => LongText.Create(v).Value);

        builder.Property(p => p.Price)
            .HasConversion(x => x.Value, v => Amount.Create(v).Value);

        builder.Property(p => p.Stock)
            .HasConversion(x => x.Value, v => Quantity.Create(v).Value);

        builder
            .HasMany(p => p.OrderDetails)
            .WithOne(od => od.Product)
            .HasForeignKey(od => od.ProductId);

        builder
            .HasMany(p => p.Ratings)
            .WithOne(r => r.Product)
            .HasForeignKey(r => r.ProductId);
    }
}
