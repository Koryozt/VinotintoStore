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

internal sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(TableNames.Order);

        builder.HasKey(o => o.Id);

        builder.Property(o => o.TotalAmount)
            .HasConversion(x => x.Value, v => Amount.Create(v).Value);

        builder
            .HasMany(o => o.OrderDetails)
            .WithOne(od => od.Order)
            .HasForeignKey(od => od.OrderId);

        builder
            .HasOne(o => o.Shipping)
            .WithOne(s => s.Order)
            .HasForeignKey<Shipping>(s => s.OrderId);

        builder
            .HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId);
    }
}
