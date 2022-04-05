using FluentValidation;
using SB.Core.Messages;
using System;

namespace SB.Credito.Application.Commands
{
    public class UpdateCreditoCommand : Command
    {
        public Guid Id { get; set; }
        public int UsuarioCod { get; set; }
        public string UsuarioNome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Salario { get; set; }
        public string Observaocao { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateMovimentationValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateMovimentationValidation : AbstractValidator<UpdateCreditoCommand>
    {
        public UpdateMovimentationValidation()
        {
            RuleFor(x => x.UsuarioCod)
                .NotEqual(0)
                .WithMessage("O campo usario cod deve ser preenchido");

            RuleFor(x => x.Cpf)
                .NotEqual(string.Empty)
                .WithMessage("O campo cpf deve ser preenchido");

            RuleFor(x => x.UsuarioNome)
                .NotEqual(string.Empty)
                .WithMessage("O campo nome de usuário deve ser preenchido");
        }
    }
}
