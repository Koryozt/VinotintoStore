﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VM.Domain.Enums;
using VM.Domain.Primitives;
using VM.Domain.ValueObjects.General;

namespace VM.Domain.Entities;

public sealed class Rating : AggregateRoot, IAuditableEntity
{
    private Rating(
        Guid id,
        Score score,
        LongText comment) : base(id)
    {
        Score = score;
        Comment = comment;
    }

    public Score Score { get; set; }
    public LongText Comment { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public static Rating Create(
        Guid id,
        Score score,
        LongText comment,
        Product product)
    {
        var rating = new Rating(
            id,
            score,
            comment)
        {
            ProductId = product.Id,
            Product = product,
            CreatedOnUtc = DateTime.UtcNow,
            ModifiedOnUtc = DateTime.UtcNow
        };

        return rating;
    }

    public void ModifyCommentAndScoring(
        LongText comment,
        Score score)
    {
        Comment = comment;
        Score = score;
    }
}
