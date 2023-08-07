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

internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable(TableNames.Payment);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Method)
            .HasConversion(x => x.Value, v => Name.Create(v).Value);

        builder.Property(p => p.Amount)
            .HasConversion(x => x.Value, v => Amount.Create(v).Value);
    }
}
