using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VM.Domain.Entities;
using VM.Domain.Enums;
using VM.Domain.ValueObjects.General;
using VM.Persistence.Constants;

namespace VM.Persistence.Configurations;

internal sealed class RatingCategory : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable(TableNames.Rating);

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Score)
            .HasConversion(
            v => v.ToString(),
            v => (Score)Enum.Parse(typeof(Score), v));

        builder.Property(r => r.Comment)
            .HasConversion(x => x.Value, v => LongText.Create(v).Value);
    }
}
