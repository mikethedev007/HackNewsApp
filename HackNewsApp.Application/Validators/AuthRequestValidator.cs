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
    public sealed class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(ValidationMessages.UsernameRequired)
                .MinimumLength(8)
                .WithMessage(ValidationMessages.UsernameMinimumLength)
                .MaximumLength(100)
                .WithMessage(ValidationMessages.UsernameMaximumLength);

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage(ValidationMessages.PasswordRequired)
                .MinimumLength(8)
                .WithMessage(ValidationMessages.PasswordMinimumLength)
                .MaximumLength(128)
                .WithMessage(ValidationMessages.PasswordMaximumLength);


        }
    }
}
