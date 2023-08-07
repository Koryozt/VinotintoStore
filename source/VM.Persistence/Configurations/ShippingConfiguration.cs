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

internal sealed class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
{
    public void Configure(EntityTypeBuilder<Shipping> builder)
    {
        builder.ToTable(TableNames.Shipping);

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Address)
            .HasConversion(x => x.Value, v => LongText.Create(v).Value);

        builder.Property(s => s.Cost)
            .HasConversion(x => x.Value, v => Amount.Create(v).Value);
    }
}
