using FluentValidation;
using GenshinTrialGenerator.Application.DTOs.Boss;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Validators.Boss
{
    public class CreateBossValidator : AbstractValidator<CreateBossRequest>
    {
        public CreateBossValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches(@"^[\p{L}\s]+$").WithMessage("Name must be valid")
                .MaximumLength(100).WithMessage("Name must be under 100 symbols");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must be under 1000 symbols");

            RuleFor(x => x.BossType)
                .IsInEnum().WithMessage("Invalid BossType");

            RuleFor(x => x.DamageType)
                .IsInEnum().WithMessage("Invalid DamageType");

            RuleFor(x => x.Region)
                .IsInEnum().WithMessage("Invalid Region");

            RuleFor(x => x.Category)
                .IsInEnum().WithMessage("Invalid Category");

            RuleFor(x => x.Location)
                .MaximumLength(50).WithMessage("Location must be under 50 symbols");

            RuleFor(x => x.HasWeakPoint)
                .NotNull();
        }
    }
}
