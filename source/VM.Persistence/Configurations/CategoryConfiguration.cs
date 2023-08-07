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

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(TableNames.Category);

        builder.HasKey(c => c.Id);

        builder.HasIndex(c => c.Name);

        builder.Property(c => c.Name)
            .HasConversion(x => x.Value, v => Name.Create(v).Value);

        builder
            .HasMany(c => c.Products)
            .WithMany(p => p.Categories)
            .UsingEntity<ProductCategory>();
    }
}
