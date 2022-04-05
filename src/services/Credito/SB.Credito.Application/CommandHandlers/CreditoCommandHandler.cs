using FluentValidation.Results;
using MediatR;
using SB.Core.Messages;
using SB.Credito.Application.Commands;
using SB.Credito.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SB.Credito.Application.CommandHandlers
{
    public class CreditoCommandHandler : CommandHandler,
        IRequestHandler<CreateCreditoCommand, ValidationResult>,
        IRequestHandler<DeleteCreditoCommand, ValidationResult>,
        IRequestHandler<UpdateCreditoCommand, ValidationResult>
    {
        private readonly ICreditoRepository _creditoRepository;

        public CreditoCommandHandler(ICreditoRepository creditoRepository)
        {
            _creditoRepository = creditoRepository;
        }

        public async Task<ValidationResult> Handle(CreateCreditoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            var credito = new Domain.Entities.Credito(message.UsuarioCod,
                                                            message.UsuarioNome,
                                                            message.Cpf,
                                                            message.DataNascimento,
                                                            message.Salario,
                                                            message.Observacao);

            _creditoRepository.AddCredito(credito);

            return await SaveData(_creditoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(DeleteCreditoCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
                return message.ValidationResult;

            var credito = _creditoRepository.GetCreditoById(message.Id);

            if (credito == null)
            {
                var failures = new List<ValidationFailure> { new ValidationFailure("", "O crédito não foi encontrada") };
                var validationResult = new ValidationResult(failures);
                return await Task.FromResult(validationResult);
            }

            _creditoRepository.RemoveCredito(credito);

            return await SaveData(_creditoRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(UpdateCreditoCommand message, CancellationToken cancellationToken)
        {
            var failures = new List<ValidationFailure>();

            if (!message.IsValid())
                return message.ValidationResult;

            var credito = _creditoRepository.GetCreditoById(message.Id);

            if (credito == null)
            {
                failures.Add(new ValidationFailure("", "O crédito não foi encontrada"));
                var validationResult = new ValidationResult(failures);
                return await Task.FromResult(validationResult);
            }

            _creditoRepository.UpdateCredito(credito);

            return await SaveData(_creditoRepository.UnitOfWork);
        }
    }
}
