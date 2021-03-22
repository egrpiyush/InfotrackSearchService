using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.GetStaticSearchResult
{
    public class GetStaticSearchResultQueryValidator : AbstractValidator<GetStaticSearchResultQuery>
    {
        public GetStaticSearchResultQueryValidator()
        {
            RuleFor(p => p.UrlOfInterest)
                .NotNull()
                .NotEmpty()
                .WithMessage("UrlOfInterest is a required parameter.");
        }
    }
}
