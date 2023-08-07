using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.Domain.Entities;
using VM.Domain.ValueObjects.Coupons;
using VM.Domain.ValueObjects.General;
using VM.Persistence.Constants;

namespace VM.Persistence.Configurations;

internal sealed class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable(TableNames.Coupon);

        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.Code)
            .IsUnique();

        builder.Property(c => c.Code)
            .HasConversion(x => x.Value, v => Code.Create(v).Value);

        builder.Property(c => c.Discount)
            .HasConversion(x => x.Value, v => Amount.Create(v).Value);
    }
}
