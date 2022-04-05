using FluentValidation;
using SB.Core.Messages;
using System;

namespace SB.Credito.Application.Commands
{
    public class DeleteCreditoCommand : Command
    {
        public Guid Id { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new DeleteMovimentationValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DeleteMovimentationValidation : AbstractValidator<DeleteCreditoCommand>
    {
        public DeleteMovimentationValidation()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O Id deve ser preenchido");
        }
    }
}
