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

internal sealed class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
    public void Configure(EntityTypeBuilder<OrderDetail> builder)
    {
        builder.ToTable(TableNames.OrderDetail);

        builder.HasKey(od => od.Id);

        builder.Property(od => od.Price)
            .HasConversion(x => x.Value, v => Amount.Create(v).Value);

        builder.Property(od => od.Quantity)
            .HasConversion(x => x.Value, v => Quantity.Create(v).Value);
    }
}
