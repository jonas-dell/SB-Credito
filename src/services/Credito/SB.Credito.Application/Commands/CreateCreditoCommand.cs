using FluentValidation;
using SB.Core.Messages;
using System;

namespace SB.Credito.Application.Commands
{
    public class CreateCreditoCommand : Command
    {
        public int UsuarioCod { get; set; }
        public string UsuarioNome { get; set; }
        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public decimal Salario { get; set; }
        public string Observacao { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new RegisterMovimentationValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RegisterMovimentationValidation : AbstractValidator<CreateCreditoCommand>
    {
        public RegisterMovimentationValidation()
        {
            RuleFor(x => x.UsuarioCod)
                .NotEqual(0)
                .WithMessage("O campo id deve ser preenchido");

            RuleFor(x => x.Cpf)
                .NotEqual(string.Empty)
                .WithMessage("O campo cpf deve ser preenchido");

            RuleFor(x => x.UsuarioNome)
                .NotEqual(string.Empty)
                .WithMessage("O campo nome de usuário deve ser preenchido");

            RuleFor(x => x.Cpf)
                .Must(HasValidCpf)
                .WithMessage("O cpf informado é inválido");
        }

        protected static bool HasValidCpf(string cpf)
        {
            return Core.ValueObjects.CPF.IsValid(cpf);
        }
    }
}
