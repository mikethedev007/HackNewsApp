using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using HackNewsApp.Application.DTOs;
using HackNewsApp.Application.Validators.Common;

namespace HackNewsApp.Application.Validators
{
    public class HackNewsQueryRequestValidator : AbstractValidator<HackNewsQueryRequest>
    {
        private static readonly string[] AllowedSortFields =
        {
            "score", "score_desc", "publishedtime", "publishedtime_desc",
            "title", "title_desc"
        };

        public HackNewsQueryRequestValidator()
        {
            Include(new PaginationValidator());

            RuleFor(x => x.MinimumScore)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MinimumScore.HasValue)
                .WithMessage(ValidationMessages.MinimumScoreMustBeValid);

            RuleFor(x => x.MaximumScore)
                .GreaterThanOrEqualTo(0)
                .When(x => x.MaximumScore.HasValue)
                .WithMessage(ValidationMessages.MaximumScoreMustBeValid);

            RuleFor(x => x)
                .Must(BeValidScoreRange)
                .When(x => x.MinimumScore.HasValue && x.MaximumScore.HasValue)
                .WithMessage(ValidationMessages.MinimumScoreMustBeLessThanOrEqualToMaximumScore);

            RuleFor(x => x.SortByField)
                .Must(BeValidSortField)
                .When(x => !string.IsNullOrWhiteSpace(x.SortByField))
                .WithMessage(ValidationMessages.InvalidSortByField);
        }

        private static bool BeValidScoreRange(HackNewsQueryRequest request)
        {
            if (!request.MinimumScore.HasValue || !request.MaximumScore.HasValue)
            {
                return true;
            }

            return request.MinimumScore.Value <= request.MaximumScore.Value;
        }

        private static bool BeValidSortField(string? sortByField)
        {
            if (string.IsNullOrWhiteSpace(sortByField))
            {
                return true;
            }

            return AllowedSortFields.Contains(sortByField,
                StringComparer.OrdinalIgnoreCase);
        }

    }
}
