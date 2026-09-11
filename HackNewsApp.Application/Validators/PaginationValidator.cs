using FluentValidation;
using HackNewsApp.Application.DTOs;
using HackNewsApp.Application.Validators.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackNewsApp.Application.Validators
{
    public sealed class PaginationValidator : AbstractValidator<HackNewsQueryRequest>
    {
        public PaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThan(0)
                .WithMessage(ValidationMessages.PageNumberMustBeGreaterThanZero);

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage(ValidationMessages.PageSizeMustBeWithinRange);
        }
    }
}
