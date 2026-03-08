using FluentValidation;
using GenshinTrialGenerator.Application.DTOs.Hero;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Validators.Hero
{
    public class CreateHeroValidator : AbstractValidator<CreateHeroRequest>
    {
        public CreateHeroValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .Matches(@"^[\p{L}\s]+$").WithMessage("Name must be valid")
                .MaximumLength(100).WithMessage("Name must be under 100 symbols");

            RuleFor(x => x.Description)
                .MaximumLength(1000).WithMessage("Description must be under 1000 symbols");

            RuleFor(x => x.Rarity)
                .IsInEnum().WithMessage("Invalid Rarity");

            RuleFor(x => x.Weapon)
                .IsInEnum().WithMessage("Invalid Weapon");

            RuleFor(x => x.Element)
                .IsInEnum().WithMessage("Invalid Element");

            RuleFor(x => x.Model)
                .IsInEnum().WithMessage("Invalid Model");

            RuleFor(x => x.TeamBonus)
                .IsInEnum().WithMessage("Invalid eamBonus");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Invalid Role");
        }
    }
}
