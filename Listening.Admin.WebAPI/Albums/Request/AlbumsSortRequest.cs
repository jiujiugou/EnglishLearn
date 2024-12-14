﻿using Commons.Validators;
using FluentValidation;
namespace Listening.Admin.WebAPI.Albums.Request
{
    public class AlbumsSortRequest
    {
        public Guid[] SortedAlbumIds { get; set; }
    }

    public class AlbumsSortRequestValidator : AbstractValidator<AlbumsSortRequest>
    {
        public AlbumsSortRequestValidator()
        {
            RuleFor(r => r.SortedAlbumIds).NotNull().NotEmpty().NotContains(Guid.Empty)
                .NotDuplicated();
        }
    }
}
